using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Models
{
    public class BookVM
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string type { get; set; } 
        public string ISBN { get; set; }
        public decimal price { get; set; }
        public int AvailableCopies { get; set; }
        public string PublishedDate { get; set; }
        public string Status { get; set; }
    }
}
