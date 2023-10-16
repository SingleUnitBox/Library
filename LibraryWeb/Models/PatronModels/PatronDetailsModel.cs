using LibraryData.Models;

namespace LibraryWeb.Models.PatronModels
{
    public class PatronDetailsModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName 
        {
            get { return FirstName + " " + LastName; }          
        }

        public int LibraryCardId { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime MemberSince { get; set; }
        public string HomeLibrary { get; set; }
        public decimal Fees { get; set; }
        public IEnumerable<Checkout> Checkouts { get; set; }
        public IEnumerable<Hold> Holds { get; set; }
    }
}
