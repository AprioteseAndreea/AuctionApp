using DataMapper;
using DataMapper.SqlServerDAO;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class CategoryServicesImplementation : ICategoryServices
    {
        private readonly ICategoryDataServices categoryDataServices;
        public CategoryServicesImplementation(ICategoryDataServices categoryDataServices)
        {
            this.categoryDataServices = categoryDataServices;
        }

        public void AddCategory(Category category)
        {
            categoryDataServices.AddCategory(category);
        }

        public void DeleteCategory(Category category)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Category> GetListOfCategories()
        {
            return categoryDataServices.GetListOfCategories();

        }

        public void UpdateCategory(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
