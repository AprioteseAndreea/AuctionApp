using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper.SqlServerDAO
{
    internal class SQLCategoryRelationDataServicesL : ICategoryRelationDataServices
    {
        public void AddCategoryRelation(CategoryRelation category)
        {
            using (var context = new MyApplicationContext())
            {
                context.CategoryRelations.Add(category);
                context.SaveChanges();
            }
        }

        public void DeleteCategoryRelation(CategoryRelation category)
        {
            using (var context = new MyApplicationContext())
            {
                var newCategory = new CategoryRelation { Id = category.Id };
                context.CategoryRelations.Attach(newCategory);
                context.CategoryRelations.Remove(newCategory);
                context.SaveChanges();
            }
        }

        public IList<CategoryRelation> GetCategoryRelationByParentId(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.CategoryRelations.Where(category => category.ParentCategory == id).ToList();
            }
        }

        public IList<CategoryRelation> GetListOfCategoriesRelation()
        {
            using (var context = new MyApplicationContext())
            {
                return context.CategoryRelations.Select(category => category).ToList();
            }
        }

        public void UpdateCategoryRelation(CategoryRelation category)
        {
            using (var context = new MyApplicationContext())
            {
                var result = context.CategoryRelations.First(c => c.Id == category.Id);
                if (result != null)
                {
                    result = category;
                    context.SaveChanges();
                }
            }
        }
    }
}
