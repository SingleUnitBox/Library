namespace LibraryWeb.Models.PatronModels
{
    public class PatronIndexListingModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int LibraryCardId { get; set; }
        public decimal Fees { get; set; }
        public string HomeLibrary { get; set; }
    }
}
