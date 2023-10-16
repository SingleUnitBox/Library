using LibraryData.Models;

namespace LibraryWeb.Models.Branch
{
    public class BranchDetailsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public IEnumerable<string> BranchHours { get; set; }
        public DateTime OpenDate { get; set; }
        public string ImageUrl { get; set; }
        public IEnumerable<Patron> Patrons { get; set; }
        public IEnumerable<LibraryAsset> LibraryAssets { get; set; }
        public decimal ValueOfAssets { get; set; }
    }
}
