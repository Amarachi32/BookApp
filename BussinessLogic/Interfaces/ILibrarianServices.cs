using BussinessLogic.Models;
using DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Interfaces
{
    public interface ILibrarianServices
    {
        void Create(CreateUserVM model);
        IEnumerable<AppUser> GetAuthors();
       Task<IEnumerable<AuthorWithBookVM>> GetAuthorsWithBooksAsync();
    }
}
