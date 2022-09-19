using Castle.MicroKernel.SubSystems.Conversion;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Entities
{
    public class Product
    {
        public long ? ProductId { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        [Column(TypeName ="decimal(115,2")]
        public decimal Price { get; set; }
        
        public string PhotoImage { get; set; }

        public int CategoryId { get; set; }

        //
        public Category Category { get; set; }
    }
}
