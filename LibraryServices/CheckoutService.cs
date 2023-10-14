using LibraryData;
using LibraryData.Models;
using LibraryServices.IServices;
using LibraryUtilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class CheckoutService : ICheckoutService
    {
        private readonly LibraryDbContext _libraryDbContext;

        public CheckoutService(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public void Add(Checkout checkout)
        {
            _libraryDbContext.Add(checkout);
            _libraryDbContext.SaveChanges();
        }

        public void CheckInAsset(int assetId)
        {
            var libraryAssetFromDb = _libraryDbContext.LibraryAssets.FirstOrDefault(x => x.Id == assetId);

            RemoveExistingCheckouts(assetId);
            CloseExistingCheckoutHistory(assetId);
            // look for exisiting holds on the item
            var earliestHold = _libraryDbContext.Holds
                .Include(x => x.LibraryAsset)
                .Include(x => x.LibraryCard)
                .Where(x => x.LibraryAsset.Id == assetId)
                .OrderBy(x => x.HoldPlaced)
                .FirstOrDefault();
            // if there are holds, checkout item to the earliest hold
            if (earliestHold != null)
            {
                CheckoutToEarliestHold(assetId, earliestHold);
            }
            // otherwise, update item status to available
            else
            {
                UpdateAssetStatus(assetId, SD.Status_Available);
            }
            _libraryDbContext.SaveChanges();
        }

        private void CheckoutToEarliestHold(int assetId, Hold earliestHold)
        {
            var libraryCard = earliestHold.LibraryCard;

            _libraryDbContext.Remove(earliestHold);
            _libraryDbContext.SaveChanges();

            CheckOutAsset(assetId, libraryCard.Id);
        }

        public void CheckOutAsset(int assetId, int libraryCardId)
        {
            if (IsCheckedOut(assetId))
            {
                return;
            }
            var libraryAssetFromDb = _libraryDbContext.LibraryAssets.FirstOrDefault(x => x.Id == assetId);
            UpdateAssetStatus(assetId, SD.Status_CheckedOut);

            var libraryCard = _libraryDbContext.LibraryCards
                .Include(x => x.Checkouts)
                .FirstOrDefault(x => x.Id == libraryCardId);

            var now = DateTime.Now;

            Checkout checkout = new()
            {
                LibraryAsset = libraryAssetFromDb,
                LibraryCard = libraryCard,
                Since = now,
                Until = GetDefaultCheckoutTime(now)
            };
            _libraryDbContext.Add(checkout);

            CheckoutHistory checkoutHistory = new()
            {
                LibraryAsset = libraryAssetFromDb,
                LibraryCard = libraryCard,
                CheckedOut = now,

            };
            _libraryDbContext.Add(checkoutHistory);
            _libraryDbContext.SaveChanges();         
        }

        private DateTime GetDefaultCheckoutTime(DateTime now)
        {
            return now.AddDays(30);
        }

        public bool IsCheckedOut(int assetId)
        {
            return _libraryDbContext.Checkouts
                .Where(x => x.LibraryAsset.Id == assetId)
                .Any();
        }

        public IEnumerable<Checkout> GetAll()
        {
            return _libraryDbContext.Checkouts
                .Include(x => x.LibraryAsset)
                .Include(x => x.LibraryCard);
        }

        public Checkout GetById(int checkoutId)
        {
            return _libraryDbContext.Checkouts
                .Include(x => x.LibraryAsset)
                .Include(x => x.LibraryCard)
                .FirstOrDefault(x => x.Id == checkoutId);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int assetId)
        {
            return _libraryDbContext.CheckoutHistories
                .Include(x => x.LibraryAsset)
                .Include(x => x.LibraryCard)
                .Where(x => x.LibraryAsset.Id == assetId);
        }

        public string GetCurrentHoldPatronName(int holdId)
        {
            var hold = _libraryDbContext.Holds
                .Include(x => x.LibraryCard)
                .FirstOrDefault(x => x.Id == holdId);

            var cardId = hold?.LibraryCard.Id;

            var patron = _libraryDbContext.Patrons
                .FirstOrDefault(x => x.LibraryCard.Id == cardId);

            return patron?.FirstName + " " + patron?.LastName;
        }

        public DateTime GetCurrentHoldPlaced(int holdId)
        {
            return _libraryDbContext.Holds
                .FirstOrDefault(x => x.Id == holdId)
                .HoldPlaced;
        }

        public IEnumerable<Hold> GetCurrentHolds(int assetId)
        {
            return _libraryDbContext.Holds
                .Include(x => x.LibraryAsset)
                .Where(x => x.LibraryAsset.Id == assetId);
        }

        public Checkout GetLatestCheckout(int assetId)
        {
            return _libraryDbContext.Checkouts
                .Include(x => x.LibraryAsset)
                .Include(x => x.LibraryCard)
                .Where(x => x.LibraryAsset.Id == assetId)
                .OrderByDescending(x => x.Since)
                .FirstOrDefault();
        }

        public void MarkFound(int assetId)
        {          
            UpdateAssetStatus(assetId, SD.Status_Available);
            //remove any exisiting checkouts on the item
            RemoveExistingCheckouts(assetId);

            //close any exisiting checkout history
            CloseExistingCheckoutHistory(assetId);

            _libraryDbContext.SaveChanges();
        }
        public void MarkLost(int assetId)
        {
            UpdateAssetStatus(assetId, SD.Status_Lost);

            _libraryDbContext.SaveChanges();
        }
        public void PlaceHold(int assetId, int libraryCardId)
        {
            var now = DateTime.Now;
            var libraryAsset = _libraryDbContext.LibraryAssets
                .Include(x => x.Status)
                .FirstOrDefault(x => x.Id == assetId);
            var libraryCard = _libraryDbContext.LibraryCards.FirstOrDefault(x => x.Id == libraryCardId);

            if (libraryAsset.Status.Name == SD.Status_Available)
            {
                UpdateAssetStatus(assetId, SD.Status_OnHold);
            }

            Hold hold = new()
            {
                LibraryAsset = libraryAsset,
                LibraryCard = libraryCard,
                HoldPlaced = now,

            };
            _libraryDbContext.Add(hold);
            _libraryDbContext.SaveChanges();
        }
        private void CloseExistingCheckoutHistory(int assetId)
        {
            var exisitingCheckoutHistory = _libraryDbContext.CheckoutHistories
                .FirstOrDefault(x => x.LibraryAsset.Id == assetId && x.CheckedIn == null);
            if (exisitingCheckoutHistory != null)
            {
                _libraryDbContext.Update(exisitingCheckoutHistory);
                exisitingCheckoutHistory.CheckedIn = DateTime.Now;
            }
        }

        private void RemoveExistingCheckouts(int assetId)
        {
            var exisitingCheckout = _libraryDbContext.Checkouts
                .FirstOrDefault(x => x.LibraryAsset.Id == assetId);
            if (exisitingCheckout != null)
            {
                _libraryDbContext.Remove(exisitingCheckout);
            }
        }
        private void UpdateAssetStatus(int assetId, string status)
        {
            var libraryAssetFromDb = _libraryDbContext.LibraryAssets
                .FirstOrDefault(x => x.Id == assetId);

            _libraryDbContext.Update(libraryAssetFromDb);

            libraryAssetFromDb.Status = _libraryDbContext.Statuses
                .FirstOrDefault(x => x.Name == status);
        }

        public string GetCurrentCheckoutPatron(int assetId)
        {
            var checkout = GetCheckoutByAssetId(assetId);
            if (checkout == null)
            {
                return "";
            }
            var cardId = checkout.LibraryCard.Id;
            var patron = _libraryDbContext.Patrons
                .FirstOrDefault(x => x.LibraryCard.Id == cardId);

            return patron.FirstName + " " + patron.LastName;
        }

        private Checkout GetCheckoutByAssetId(int assetId)
        {
            return _libraryDbContext.Checkouts
                .Include(x => x.LibraryCard)
                .FirstOrDefault(x => x.LibraryAsset.Id == assetId);
        }

    }
}
