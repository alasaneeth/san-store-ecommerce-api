using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application.DTO.BrandDtos
{
    public class CreateBrandDto
    {
        
        [Required]
        public string Name { get; set; }
        [Required]
        public int EstablishYear { get; set; }
    }
}
