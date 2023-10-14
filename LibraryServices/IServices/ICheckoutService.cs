using LibraryData.Models;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices.IServices
{
    public interface ICheckoutService
    {
        IEnumerable<Checkout> GetAll();
        Checkout GetById(int checkoutId);
        Checkout GetLatestCheckout(int assetId);
        void Add(Checkout checkout);
        void CheckOutAsset(int assetId, int libraryCardId);
        void CheckInAsset(int assetId);
        IEnumerable<CheckoutHistory> GetCheckoutHistory(int assetId);
        string GetCurrentCheckoutPatron(int assetId);
        void PlaceHold(int assetId, int libraryCardId);
        string GetCurrentHoldPatronName(int holdId);
        DateTime GetCurrentHoldPlaced(int holdId);
        IEnumerable<Hold> GetCurrentHolds(int id);
        void MarkLost(int assetId);
        void MarkFound(int assetId);
        bool IsCheckedOut(int assetId);
    }
}
