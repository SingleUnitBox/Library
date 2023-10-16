using LibraryServices.IServices;
using LibraryWeb.Models.PatronModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers
{
    public class PatronController : Controller
    {
        private readonly IPatronService _patronService;

        public PatronController(IPatronService patronService)
        {
            _patronService = patronService;
        }
        public IActionResult Index()
        {
            var patronModels = _patronService.GetAll();
            var listingResult = patronModels.Select(x =>
                new PatronIndexListingModel
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    LibraryCardId = x.LibraryCard.Id,
                    Fees = x.LibraryCard.Fees,
                    HomeLibrary = x.HomeLibraryBranch.Name
                });
            PatronIndexModel patronIndexModel = new()
            {
                Patrons = listingResult,
            };
            return View(patronIndexModel);
        }
        public IActionResult Details(int patronId)
        {
            var patronModel = _patronService.GetById(patronId);
            PatronDetailsModel patronDetailsModel = new()
            {
                Id = patronModel.Id,
                FirstName = patronModel.FirstName,
                LastName = patronModel.LastName,
                LibraryCardId = patronModel.LibraryCard.Id,
                Address = patronModel.Address,
                PhoneNumber = patronModel.PhoneNumber,
                MemberSince = patronModel.LibraryCard.Created,
                HomeLibrary = patronModel.HomeLibraryBranch.Name,
                Fees = patronModel.LibraryCard.Fees,
                Checkouts = _patronService.GetCheckouts(patronId),
                Holds = _patronService.GetHolds(patronId),
            };
            return View(patronDetailsModel);
        }
    }
}
