using SanStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Application.DTO.ProductDto
{
    public class CreateProductDto
    {
        public int categoryId { get; set; }     
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Sepacification { get; set; }
        public double price { get; set; }
    }
}
