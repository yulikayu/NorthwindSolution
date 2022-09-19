using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Entities
{
    //using union 
    //tadinya internal class category di rubah jadi public
    public class Category
    {
        public int CategoryId { get; set; } 
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Product>Products { get; set; }

        public string PhotoImage { get; set; }
    }
}
