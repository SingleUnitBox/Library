using LibraryData.Models;

namespace LibraryWeb.Models.Branch
{
    public class BranchIndexListingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOpenedNow { get; set; }
        public IEnumerable<LibraryAsset> LibraryAssets { get; set; }
        public IEnumerable<Patron> Patrons { get; set; }
    }
}
