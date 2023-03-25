using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BussinessLogic.Models
{
    public class CreateUserVM
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required] 
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage ="Password and Confirmation Password do not match")]
        public string ConfirmedPassword { get; set; }
        

    }
}


