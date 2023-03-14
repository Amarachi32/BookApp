using AutoMapper;
using BussinessLogic.Interfaces;
using BussinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BussinessLogic.Implementations
{
    public class CatalogueService : ICatalogueServices
    {
        //private readonly UserManager<AppUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<AppUser> _authorRepo;
        private readonly IRepository<Book> _bookRepo;

        //UserManager<AppUser> usrMgr,
        public CatalogueService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            // _userManager = usrMgr;
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
            //  AppUser author = null;

            // Author author = LibraryDbContext.GetAuthorsWithTheirBooks().SingleOrDefault(u => u.Id == model.AuthorId);
            if (author == null)
            {
                return (false, $"user not found");
            }
            //  int? lastBookId =  author.BookList.OrderBy(t => t.Id).LastOrDefault()?.Id;
            var newBook = _mapper.Map<AddUpdateBookVM, Book>(model);
            Book bookDetails = author.BookList.SingleOrDefault(u => u.Id == newBook.Id);

                       if (bookDetails != null)
                        {

                            _mapper.Map(model, bookDetails);
                            await _unitOfWork.SaveChangesAsync();
                            return (true, $"update Successfully");

                        }

           // var newBook = _mapper.Map<AddUpdateBookVM, Book>(model);

            //var newBook = Mapper.CreateMap<Book>(model);
            author.BookList.Add(newBook);
            var rowChanges = await _unitOfWork.SaveChangesAsync();
            return rowChanges > 0 ? (true, $"Task: {model.Title} was successfully created!") : (false, "Failed To save changes!");
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
            //Book bookDetails = author.BookList.SingleOrDefault(u => u.Id == newBook.Id);

            if (bookDetails != null)
            {

                _mapper.Map(model, bookDetails);
                var rowChanges =  await _unitOfWork.SaveChangesAsync();
                return rowChanges > 0 ? (true, $"Task: {model.Title} was updated Successfully!") : (false, "Failed To save changes!");
            }
            return (false, $"user not found");
        }

        public async Task<(bool Success, string msg)> DeleteAsync(int BookId)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            AppUser user = await _authorRepo.GetSingleByAsync(u => u.Id == userId,
               include: u => u.Include(x => x.BookList.Where(u => u.AppUserId == userId)), tracking: true);
            // AppUser author = await _authorRepo.GetSingleByAsync(u => u.Id == AuthorId);
            AppUser author = null;
            // Author author = LibraryDbContext.GetAuthorsWithTheirBooks().SingleOrDefault(u => u.Id == AuthorId);
            if (author == null)
            {
                return (false, $"user not found");
            }
            _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Book pickBook = author.BookList.FirstOrDefault(b => b.Id == BookId);
            if (pickBook != null)
            {
                author.BookList.Remove(pickBook);
                return await _unitOfWork.SaveChangesAsync() > 0 ? (true, $"Task with Title:{pickBook.Title} was deleted") : (false, $"Delete Failed");
            }
            return (false, $"book with title: {BookId} not found");
        }


        public (AppUser author, string msg) GetBook(string AppUserId, int BookId)
        {
            //AppUser author = await _authorRepo.GetSingleByAsync(u => u.Id == model.AuthorId, include: u => u.Include(x => x.BookList), tracking: true);

            // var books = DbContext.Books.Include(b => b.AppUser).ToList();
            // AppUser author = LibraryDbContext.GetAuthorsWithTheirBooks().SingleOrDefault(u => u.Id == AuthorId);
            //  AppUser author = _authorRepo.GetSingleBy()
            AppUser author = null;
            if (author == null)
            {
                return (null, $"user not found");
            }
            Book pickBook = author.BookList.FirstOrDefault(b => b.Id == BookId);
            if (pickBook != null)
            {
                return (author, $"Book with Title:{pickBook.Title} was found");

            }
            return (null, $"Book with Title:{pickBook.Title} was not found");
        }

        public IEnumerable<Book> GetBookList()
        {
            List<Book> bookList = new List<Book>();


            //var books = LibraryDbContext.GetAuthorsWithTheirBooks().SelectMany(t => t.BookList).ToList();
            var books = bookList;


            return books;
        }


    }
}
