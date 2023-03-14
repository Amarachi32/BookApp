
using BussinessLogic.Interfaces;
using BussinessLogic.Models;
using DataAccess.Entities;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;

namespace BussinessLogic.Implementations
{
    public class LibrarianService : ILibrarianServices
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AppUser> _authorRepo;

        public LibrarianService( IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
            _authorRepo = _unitOfWork.GetRepository<AppUser>();
        }
        public void Create(CreateUserVM model)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppUser> GetAuthors()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuthorWithBookVM>> GetAuthorsWithBooksAsync()
        {
            // return _authorRepo.Select(u => new AuthorWithBookVM { 

           // var userId = HttpContext.Current.User.Identity.GetUserId();
           // var books = db.Books.Where(b => b.UserId == userId).ToList();

            return (await _authorRepo.GetAllAsync(include: u => u.Include(t => t.BookList))).Select(u => new AuthorWithBookVM { 
                Name = u.UserName,
                Books = u.BookList.Select(x => new BookVM
                {
                    Title = x.Title,
                    Author= x.Author,
                    Description= x.Description,
                    type= x.type.ToString(),
                    ISBN= x.ISBN,
                    price= x.price,
                    PublishedDate = x.PublishedDate.ToString("d"),
                    Status= x.AvailableCopies.ToString(),
                })
            
            }).ToList();
        }

/*        public void GetBooks()
        {
            var authorId = 1;
            var author = db.Authors
                .Include(a => a.Books)
                .FirstOrDefault(a => a.AuthorId == authorId);

            if (author != null)
            {
                var books = author.Books.ToList();
                // do something with the books
            }
        }*/
    }
}
