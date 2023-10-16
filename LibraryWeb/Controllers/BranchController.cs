using LibraryServices.IServices;
using LibraryWeb.Models.Branch;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }
        public IActionResult Index()
        {
            var branchModels = _branchService.GetAll();
            var listingResult = branchModels.Select(x =>
            new BranchIndexListingModel
            {
                Id = x.Id,
                Name = x.Name,
                LibraryAssets = _branchService.GetAssetsById(x.Id),
                Patrons = _branchService.GetPatronsById(x.Id),
                IsOpenedNow = _branchService.IsBranchOpen(x.Id)
            });
            BranchIndexModel branchIndexModel = new()
            {
                Branches = listingResult,
            };
            return View(branchIndexModel);
        }
        public IActionResult Details(int branchId)
        {
            var branchModel = _branchService.GetById(branchId);
            BranchDetailsModel branchDetailsModel = new()
            {
                Id = branchModel.Id,
                Name = branchModel.Name,
                Address = branchModel.Address,
                PhoneNumber = branchModel.PhoneNumber,
                Description = branchModel.Description,
                ImageUrl = branchModel.ImageUrl,
                OpenDate = branchModel.OpenDate,
                BranchHours = _branchService.GetBranchHours(branchId),
                Patrons = _branchService.GetPatronsById(branchId),
                LibraryAssets = _branchService.GetAssetsById(branchId),
                ValueOfAssets = _branchService.GetAssetsById(branchId).Sum(x => x.Cost)
            };
            return View(branchDetailsModel);
        }
    }
}
