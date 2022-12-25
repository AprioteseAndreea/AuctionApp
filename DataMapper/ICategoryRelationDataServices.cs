﻿using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapper
{
    public interface ICategoryRelationDataServices
    {
        IList<CategoryRelation> GetListOfCategoriesRelation();

        void DeleteCategoryRelation(CategoryRelation category);

        void UpdateCategoryRelation(CategoryRelation category);

        IList<CategoryRelation> GetCategoryRelationByParentId(int id);

        void AddCategoryRelation(CategoryRelation category);
    }
}
