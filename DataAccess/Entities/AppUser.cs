﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class AppUser : IdentityUser
    {
       [Required]
       // public string Name { get; set; }
        /* 
                [Required]
                [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
                public string Email { get; set; }

                [Required]
                public string Password { get; set; }*/
        public IList<Book> BookList { get; set; }
    }
}
