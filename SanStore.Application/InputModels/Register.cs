using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application.InputModels
{
    public class Register
    {
        [Required]
        public string FirstnaName { get; set; } 
        public string LastnaName { get; set; }
        
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }    
    }
}
