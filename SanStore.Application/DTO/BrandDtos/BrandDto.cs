using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application.DTO.BrandDtos
{
    public class BrandDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int EstablishYear { get; set; }

    }
}
