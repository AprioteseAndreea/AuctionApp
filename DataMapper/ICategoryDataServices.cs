using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
   public interface ICategoryDataServices
    {
        IList<Category> GetListOfCategories();

        void DeleteCategory(Category category);

        void UpdateCategory(Category category);

        Category GetCategoryById(int id);

        void AddCategory(Category category);
    }
}
