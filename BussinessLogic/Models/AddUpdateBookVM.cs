using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Models
{
    public class AddUpdateBookVM
    {
       public string? AppUserId { get; set; }
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Genre type { get; set; } = Genre.Fiction;
        public string? ISBN { get; set; }
        public decimal price { get; set; }
        public DateTime PublishedDate { get; set; }
    }
}
