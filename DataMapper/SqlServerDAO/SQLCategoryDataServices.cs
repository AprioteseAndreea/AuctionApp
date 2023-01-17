// <copyright file="SQLCategoryDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;

    public class SQLCategoryDataServices : ICategoryDataServices
    {
        /// <summary>
        /// Adds the category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void AddCategory(Category category)
        {
            using (var context = new MyApplicationContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the category.
        /// </summary>
        /// <param name="category">The category.</param>
        public void DeleteCategory(Category category)
        {
            using (var context = new MyApplicationContext())
            {
                context.Categories.Attach(category);
                context.Categories.Remove(category);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the category by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Category GetCategoryById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Categories.Where(cat => cat.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets the list of categories.
        /// </summary>
        /// <returns></returns>
        public IList<Category> GetListOfCategories()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Categories.Select(category => category).ToList();
            }
        }

        /// <summary>
        /// Updates the category.
        /// </summary>
        /// <param name="category">The category.</param>
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