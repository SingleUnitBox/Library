using LibraryData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices.IServices
{
    public interface IBranchService
    {
        LibraryBranch GetById(int branchId);
        IEnumerable<LibraryBranch> GetAll();
        IEnumerable<Patron> GetPatronsById(int branchId);
        IEnumerable<LibraryAsset> GetAssetsById(int branchId);
        IEnumerable<string> GetBranchHours(int branchId);
        bool IsBranchOpen(int branchId);

    }
}
