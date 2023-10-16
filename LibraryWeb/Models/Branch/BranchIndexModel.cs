using LibraryData.Models;

namespace LibraryWeb.Models.Branch
{
    public class BranchIndexModel
    {
        public IEnumerable<BranchIndexListingModel> Branches { get; set; }
    }
}
