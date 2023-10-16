using LibraryData;
using LibraryData.Models;
using LibraryServices.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class PatronService : IPatronService
    {
        private readonly LibraryDbContext _libraryDbContext;

        public PatronService(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public void Add(Patron patron)
        {
            _libraryDbContext.Add(patron);
            _libraryDbContext.SaveChanges();
        }

        public IEnumerable<Patron> GetAll()
        {
            return _libraryDbContext.Patrons
                .Include(x => x.LibraryCard)
                .Include(x => x.HomeLibraryBranch);
        }

        public Patron GetById(int patronId)
        {
            return _libraryDbContext.Patrons
                .Include(x => x.LibraryCard)
                .Include(x => x.HomeLibraryBranch)
                .FirstOrDefault(x => x.Id == patronId);
        }

        public IEnumerable<CheckoutHistory> GetCheckoutHistory(int patronId)
        {
            var libraryCardId = GetById(patronId).LibraryCard.Id;
            return _libraryDbContext.CheckoutHistories
                .Include(x => x.LibraryCard)
                .Include(x => x.LibraryAsset)
                .Where(x => x.LibraryCard.Id == libraryCardId)
                .OrderByDescending(x => x.CheckedOut);
        }

        public IEnumerable<Checkout> GetCheckouts(int patronId)
        {
            var libraryCardId = GetById(patronId).LibraryCard.Id;
            return _libraryDbContext.Checkouts
                .Include(x => x.LibraryCard)
                .Include(x => x.LibraryAsset)
                .Where(x => x.LibraryCard.Id == libraryCardId);
        }

        public IEnumerable<Hold> GetHolds(int patronId)
        {
            var libraryCardId = GetById(patronId).LibraryCard.Id;
            return _libraryDbContext.Holds
                .Include(x => x.LibraryCard)
                .Include(x => x.LibraryAsset)
                .Where(x => x.LibraryCard.Id == libraryCardId)
                .OrderByDescending(x => x.HoldPlaced);
        }
    }
}
