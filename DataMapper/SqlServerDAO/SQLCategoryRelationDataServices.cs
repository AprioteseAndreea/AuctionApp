using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
                context.CategoryRelations.Attach(category);
                context.CategoryRelations.Remove(category);
                context.SaveChanges();
            }
        }

        public IList<CategoryRelation> GetCategoryRelationByChildId(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.CategoryRelations.Where(category => category.ChildCategory.Id == id).ToList();
            }
        }

        public CategoryRelation GetCategoryRelationByChildAndParentId(int parentId, int childId)
        {
            using (var context = new MyApplicationContext())
            {
                return context.CategoryRelations.Where(cat => cat.ChildCategory.Id == childId && cat.ParentCategory.Id == parentId).SingleOrDefault();
            }
        }

        public IList<CategoryRelation> GetCategoryRelationByParentId(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.CategoryRelations.Where(category => category.ParentCategory.Id == id).ToList();
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
                var result = context.CategoryRelations.First(c => c.ChildCategory.Id == category.ChildCategory.Id && c.ParentCategory.Id == category.ParentCategory.Id);
                if (result != null)
                {
                    result = category;
                    context.SaveChanges();
                }
            }
        }
    }
}
