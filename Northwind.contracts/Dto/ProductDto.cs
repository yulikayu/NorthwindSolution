using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Contracts.Dto
{
    public class ProductDto
    {
        public long? ProductId { get; set; }
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Description Cannot be longer than 50 Characters.")]
        public string Description { get; set; }
        
        public decimal Price { get; set; }
        [Required(ErrorMessage ="Please select image")]
        public IFormFile PhotoImage { get; set; }

        public int CategoryId { get; set; }

        //
        public CategoryDto Category { get; set; }
        public virtual ICollection<CategoryDto> Categories { get; set; }
 
    }
}
