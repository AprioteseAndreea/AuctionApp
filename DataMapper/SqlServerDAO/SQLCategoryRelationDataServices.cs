// <copyright file="SQLCategoryRelationDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;

    internal class SQLCategoryRelationDataServices : ICategoryRelationDataServices
    {
        /// <summary>
        /// Adds the category relation.
        /// </summary>
        /// <param name="category">The category.</param>
        public void AddCategoryRelation(CategoryRelation category)
        {
            using (var context = new MyApplicationContext())
            {
                context.CategoryRelations.Add(category);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the category relation.
        /// </summary>
        /// <param name="category">The category.</param>
        public void DeleteCategoryRelation(CategoryRelation category)
        {
            using (var context = new MyApplicationContext())
            {
                context.CategoryRelations.Attach(category);
                context.CategoryRelations.Remove(category);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets the category relation by child identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IList<CategoryRelation> GetCategoryRelationByChildId(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.CategoryRelations.Where(category => category.ChildCategory.Id == id).ToList();
            }
        }

        /// <summary>
        /// Gets the category relation by child and parent identifier.
        /// </summary>
        /// <param name="parentId">The parent identifier.</param>
        /// <param name="childId">The child identifier.</param>
        /// <returns></returns>
        public CategoryRelation GetCategoryRelationByChildAndParentId(int parentId, int childId)
        {
            using (var context = new MyApplicationContext())
            {
                return context.CategoryRelations.Where(cat => cat.ChildCategory.Id == childId && cat.ParentCategory.Id == parentId).SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets the category relation by parent identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public IList<CategoryRelation> GetCategoryRelationByParentId(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.CategoryRelations.Where(category => category.ParentCategory.Id == id).ToList();
            }
        }

        /// <summary>
        /// Gets the list of categories relation.
        /// </summary>
        /// <returns></returns>
        public IList<CategoryRelation> GetListOfCategoriesRelation()
        {
            using (var context = new MyApplicationContext())
            {
                return context.CategoryRelations.Select(category => category).ToList();
            }
        }

        /// <summary>
        /// Updates the category relation.
        /// </summary>
        /// <param name="category">The category.</param>
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