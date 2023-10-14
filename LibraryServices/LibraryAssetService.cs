using LibraryData;
using LibraryData.Models;
using LibraryServices.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAssetService
    {
        private readonly LibraryDbContext _libraryDbContext;

        public LibraryAssetService(LibraryDbContext libraryDbContext)
        {
            _libraryDbContext = libraryDbContext;
        }
        public void Add(LibraryAsset libraryAsset)
        {
            _libraryDbContext.Add(libraryAsset);
            _libraryDbContext.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _libraryDbContext.LibraryAssets
                .Include(x => x.Status)
                .Include(x => x.Location);
        }

        public string GetAuthorOrDirector(int id)
        {
            var isBook = _libraryDbContext.LibraryAssets
                .OfType<Book>()
                .Where(x => x.Id == id)
                .Any();

            return isBook ?
                _libraryDbContext.Books.FirstOrDefault(x => x.Id == id).Author :
                _libraryDbContext.Videos.FirstOrDefault(x => x.Id == id).Director
                ?? "Unknown";
        }

        public LibraryAsset GetById(int id)
        {
            return _libraryDbContext.LibraryAssets
                .Include(x => x.Status)
                .Include(x => x.Location)
                .FirstOrDefault(asset => asset.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return GetById(id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            if (_libraryDbContext.Books.Any(x => x.Id == id))
            {
                return _libraryDbContext.Books.FirstOrDefault(x => x.Id == id).DeweyIndex;
            }
            else
                return "";
        }

        public string GetIsbn(int id)
        {
            if (_libraryDbContext.Books.Any(x => x.Id == id))
            {
                return _libraryDbContext.Books.FirstOrDefault(x => x.Id == id).ISBN;
            }
            else
                return "";
        }

        public string GetTitle(int id)
        {
            return GetById(id).Title;
        }

        public string GetType(int id)
        {
            var isBook = _libraryDbContext.LibraryAssets
                .OfType<Book>()
                .Where(x => x.Id == id)
                .Any();

            return isBook ? "Book" : "Video";
        }
    }
}
