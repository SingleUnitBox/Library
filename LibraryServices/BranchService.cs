using LibraryData;
using LibraryData.Models;
using LibraryServices.DataHelpers;
using LibraryServices.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class BranchService : IBranchService
    {
        private readonly LibraryDbContext _libraryDbContext;

        public BranchService(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public IEnumerable<LibraryBranch> GetAll()
        {
            return _libraryDbContext.LibraryBranches
                .Include(x => x.LibraryAssets)
                .Include(x => x.Patrons);
        }

        public IEnumerable<LibraryAsset> GetAssetsById(int branchId)
        {
            return _libraryDbContext.LibraryBranches
                .Include(x => x.LibraryAssets)
                .FirstOrDefault(x => x.Id == branchId)
                .LibraryAssets;
        }

        public IEnumerable<string> GetBranchHours(int branchId)
        {
            var hours = _libraryDbContext.BranchHours.Where(x => x.Branch.Id == branchId);
            return BranchServiceHelper.HumanizeBusinessHours(hours);
        }

        public LibraryBranch GetById(int branchId)
        {
            return _libraryDbContext.LibraryBranches
                .Include(x => x.LibraryAssets)
                .Include(x => x.Patrons)
                .FirstOrDefault(x => x.Id == branchId);
        }

        public IEnumerable<Patron> GetPatronsById(int branchId)
        {
            return _libraryDbContext.Patrons
                .Where(x => x.HomeLibraryBranch.Id == branchId);
        }

        public bool IsBranchOpen(int branchId)
        {
            var currentTimeHour = DateTime.Now.Hour;
            var currentTimeDay = (int)DateTime.Now.DayOfWeek + 1;

            var hours = _libraryDbContext.BranchHours.Where(x => x.Branch.Id == branchId);
            var daysHours = hours.FirstOrDefault(x => x.DayOfWeek == currentTimeDay);

            var isOpen = currentTimeHour >= daysHours.OpenTime && currentTimeHour < daysHours.CloseTime;
            return isOpen;
        }
    }
}
