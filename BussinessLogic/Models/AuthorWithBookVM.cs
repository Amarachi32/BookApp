using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Models
{
    public class AuthorWithBookVM
    {
        public string Name { get; set; }
        public IEnumerable<BookVM> Books { get; set; }

    }
}
