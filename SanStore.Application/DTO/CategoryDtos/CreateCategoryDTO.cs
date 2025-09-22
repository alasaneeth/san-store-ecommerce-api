using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application.DTO.CategoryDtos
{
    public class CreateCategoryDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
