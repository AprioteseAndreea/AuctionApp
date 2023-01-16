namespace DataMapper.SqlServerDAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;

    public class SQLCategoryDataServices : ICategoryDataServices
    {
        public void AddCategory(Category category)
        {
            using (var context = new MyApplicationContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        public void DeleteCategory(Category category)
        {
            using (var context = new MyApplicationContext())
            {
                context.Categories.Attach(category);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        public Category GetCategoryById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Categories.Where(cat => cat.Id == id).SingleOrDefault();
            }
        }

        public IList<Category> GetListOfCategories()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Categories.Select(category => category).ToList();
            }
        }

        public void UpdateCategory(Category category)
        {
            using (var context = new MyApplicationContext())
            {
                var result = context.Categories.First(c => c.Id == category.Id);
                if (result != null)
                {
                    result = category;
                    context.SaveChanges();
                }
            }
        }
    }
}