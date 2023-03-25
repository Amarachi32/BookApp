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
        Task<Book> GetBookAsync(int BookId);

        public IEnumerable<Book> GetBookList();
        Task<(bool Success, string msg)> UpdateAsync(AddUpdateBookVM model, int BookId);


    }
}
