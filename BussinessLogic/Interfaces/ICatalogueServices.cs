using BussinessLogic.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface ICatalogueServices
    {
         Task <(bool Success, string msg)> AddorUpdateAsync(AddUpdateBookVM model);
        //(AppUser author, string msg) GetBook(string AppUserId, int BookId);
        Task<AddUpdateBookVM> GetBook(AddUpdateBookVM model, int id);
        Task<(bool Success, string msg)> DeleteAsync( int BookId);
        public IEnumerable<Book> GetBookList();
        Task<(bool Success, string msg)> UpdateAsync(AddUpdateBookVM model, int BookId);


    }
}
