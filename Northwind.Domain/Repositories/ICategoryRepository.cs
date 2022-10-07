using Northwind.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Northwind.Domain.Repositories
{
    public interface ICategoryRepository
    {
        //trackChanges => feature untuk mendekteksi peribahan data diobject category
        Task<IEnumerable<Category>> GetAllCategory(bool trackChanges);

        //craete 1 record with this code
        Task<Category> GetCategoryById(int categoryId,bool trackChanges);
        void insert(Category category);
        void edit(Category category);
        void remove(Category category);
    }
}
