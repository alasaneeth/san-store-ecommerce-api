using SanStore.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanStore.Domain.Models
{
    public class Product:BaseModel
    {
        public int categoryId { get; set; }

        [ForeignKey("categoryId")]
        public Category category { get; set; }
        [ForeignKey("BrandId")]
        public int BrandId { get; set; }    
        public Brand Brand { get; set; }
        
        public string Name { get; set; }
        public string Sepacification { get; set; }
        public double price { get; set; }
    }
}
