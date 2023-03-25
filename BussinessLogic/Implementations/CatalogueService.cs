using AutoMapper;
using BussinessLogic.Interfaces;
using BussinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BussinessLogic.Implementations
{
    public class CatalogueService : ICatalogueServices
    {
    
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<AppUser> _authorRepo;
        private readonly IRepository<Book> _bookRepo;

        public CatalogueService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
     
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _bookRepo = _unitOfWork.GetRepository<Book>();
            _authorRepo = _unitOfWork.GetRepository<AppUser>();
        }

        public async Task<(bool Success, string msg)> AddorUpdateAsync(AddUpdateBookVM model)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser author = await _authorRepo.GetSingleByAsync(u => u.Id == userId, include: u => u.Include(x => x.BookList), tracking: true);
            if (author == null)
            {
                return (false, $"user not found");
            }
            var newBook = _mapper.Map<AddUpdateBookVM, Book>(model);
            Book bookDetails = author.BookList.SingleOrDefault(u => u.Id == newBook.Id);

            if (bookDetails != null)
            {

                _mapper.Map(model, bookDetails);
                await _unitOfWork.SaveChangesAsync();
                return (true, $"update Successfully");

            }
            author.BookList.Add(newBook);
            var rowChanges = await _unitOfWork.SaveChangesAsync();
            return rowChanges > 0 ? (true, $"Task: {model.Title} was successfully created!") : (false, "Failed To save changes!");
        }
        public async Task<Book> GetBookAsync(int BookId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            AppUser author = await _authorRepo.GetSingleByAsync(u => u.Id == userId, include: u => u.Include(x => x.BookList.Where(u => u.AppUserId == userId)), tracking: true);

            if (author == null)
            {
                return null;
            }

            Book book = author.BookList.FirstOrDefault(t => t.Id == BookId);

            if (book == null)
            {
                return null;
            }

            return book;

        }
        public async Task<(bool Success, string msg)> UpdateAsync(AddUpdateBookVM model, int BookId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            AppUser author = await _authorRepo.GetSingleByAsync(u => u.Id == userId, include: u => u.Include(x => x.BookList), tracking: true);

            if (author == null)
            {
                return (false, $"user not found");
            }
            Book bookDetails = author.BookList.SingleOrDefault(u => u.Id == BookId);

            if (bookDetails != null)
            {

                var updateModel = _mapper.Map(model, bookDetails);
                var rowChanges = await _bookRepo.SaveAsync();


                if (rowChanges > 0)
                {
                    return  (true, $"Task: {model.Title} was updated Successfully!") ;
                }
                }
            return (false, $"user not found");
        }

    public IEnumerable<Book> GetBookList()
    {
        List<Book> bookList = new List<Book>();
        var books = bookList;
        return books;
    }


}
}
