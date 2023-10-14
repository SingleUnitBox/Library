using LibraryData;
using LibraryServices.IServices;
using LibraryWeb.Models.Catalog;
using LibraryWeb.Models.CheckoutModels;
using Microsoft.AspNetCore.Mvc;

namespace LibraryWeb.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ILibraryAssetService _libraryAssetService;
        private readonly ICheckoutService _checkoutService;

        public CatalogController(ILibraryAssetService libraryAssetService,
            ICheckoutService checkoutService)
        {
            _libraryAssetService = libraryAssetService;
            _checkoutService = checkoutService;
        }
        public IActionResult Index()
        {
            var libraryAssetModels = _libraryAssetService.GetAll();

            var listingResult = libraryAssetModels.Select(x =>
                new AssetIndexListingModel
                {
                    Id = x.Id,
                    ImageUrl = x.ImageUrl,
                    Title = x.Title,
                    AuthorOrDirector = _libraryAssetService.GetAuthorOrDirector(x.Id),
                    Type = _libraryAssetService.GetType(x.Id),
                    DeweyCallNumber = _libraryAssetService.GetDeweyIndex(x.Id),
                    NumberOffCopies = x.NumberOfCopies.ToString(),
                });
            AssetIndexModel assetIndexModel = new()
            {
                Assets = listingResult,
            };

            return View(assetIndexModel);
        }
        public IActionResult Details(int id)
        {
            var libraryAsset = _libraryAssetService.GetById(id);

            AssetDetailsModel assetDetailsModel = new()
            {
                Id = libraryAsset.Id,
                Title = libraryAsset.Title,
                AuthorOrDirector = _libraryAssetService.GetAuthorOrDirector(libraryAsset.Id),
                Type = _libraryAssetService.GetType(libraryAsset.Id), 
                Year = libraryAsset.Year,
                ISBN = _libraryAssetService.GetIsbn(libraryAsset.Id),
                DeweyCallNumber = _libraryAssetService.GetDeweyIndex(libraryAsset.Id),
                Status = libraryAsset.Status.Name,
                Cost = libraryAsset.Cost,
                CurrentLocation = _libraryAssetService.GetCurrentLocation(libraryAsset.Id).Name,
                ImageUrl = libraryAsset.ImageUrl,
                PatronName = _checkoutService.GetCurrentCheckoutPatron(libraryAsset.Id),
                LastCheckout = _checkoutService.GetLatestCheckout(libraryAsset.Id),
                CheckoutHistory = _checkoutService.GetCheckoutHistory(libraryAsset.Id),
                CurrentHolds = _checkoutService.GetCurrentHolds(libraryAsset.Id)
                    .Select(x => new AssetHoldModel
                    {
                        PatronName = _checkoutService.GetCurrentHoldPatronName(x.Id),
                        HoldPlaced = _checkoutService.GetCurrentHoldPlaced(x.Id).ToString("d")
                    }),
            };

            return View(assetDetailsModel);
        }
        public IActionResult MarkLost(int id)
        {
            _checkoutService.MarkLost(id);
            return RedirectToAction(nameof(Details), new { id = id });
        }
        public IActionResult MarkFound(int id)
        {
            _checkoutService.MarkFound(id);
            return RedirectToAction(nameof(Details), new { id = id });
        }
        public IActionResult CheckOut(int id)
        {
            var libraryAsset = _libraryAssetService.GetById(id);
            CheckoutModel checkoutModel = new()
            {
                LibraryAssetId = id,
                Title = libraryAsset.Title,
                LibraryCardId = "",
                ImageUrl = libraryAsset.ImageUrl,
                IsCheckedOut = _checkoutService.IsCheckedOut(id)

            };
            return View(checkoutModel);
        }
        public IActionResult CheckIn(int id)
        {
            _checkoutService.CheckInAsset(id);
            return RedirectToAction(nameof(Details), new { id = id });
        }
        public IActionResult Hold(int id)
        {
            var libraryAsset = _libraryAssetService.GetById(id);
            CheckoutModel checkoutModel = new()
            {
                LibraryAssetId = id,
                Title = libraryAsset.Title,
                LibraryCardId = "",
                ImageUrl = libraryAsset.ImageUrl,
                IsCheckedOut = _checkoutService.IsCheckedOut(id),
                HoldCount = _checkoutService.GetCurrentHolds(id).Count()
            };
            return View(checkoutModel);
        }
        
        [HttpPost]
        public IActionResult PlaceCheckout(int libraryAssetId, int libraryCardId)
        {
            _checkoutService.CheckOutAsset(libraryAssetId, libraryCardId);
            return RedirectToAction(nameof(Details), new { id = libraryAssetId });
        }
        [HttpPost]
        public IActionResult PlaceHold(int libraryAssetId, int libraryCardId)
        {
            _checkoutService.PlaceHold(libraryAssetId, libraryCardId);
            return RedirectToAction(nameof(Details), new { id = libraryAssetId });
        }
    }
}
