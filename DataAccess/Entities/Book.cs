using DataAccess.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class Book: BaseEntity
    {

        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public Genre type { get; set; } = Genre.Fiction;
        public string ISBN { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal price { get; set; }
        public int AvailableCopies { get; set; }
        public DateTime PublishedDate { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }  

    }

}
