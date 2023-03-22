using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic.Models
{
    public class CreateRoleVM
    {
        [Required]
        public string RoleName { get; set; }
    }
}
