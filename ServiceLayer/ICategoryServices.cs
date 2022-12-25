﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
   public interface ICategoryServices
    {
        IList<Category> GetListOfCategories();

        void DeleteCategory(Category category);

        void UpdateCategory(Category category);

        Category GetCategoryById(int id);

        void AddCategory(Category category);
    }
}
