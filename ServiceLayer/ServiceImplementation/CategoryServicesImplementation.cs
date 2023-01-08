using DataMapper;
using DomainModel;
using log4net;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using ServiceLayer.Utils;
using System;
using System.Collections.Generic;


namespace ServiceLayer.ServiceImplementation
{
    public class CategoryServicesImplementation : ICategoryServices
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(CategoryServicesImplementation));
        private readonly ICategoryDataServices categoryDataServices;
        public CategoryServicesImplementation(ICategoryDataServices categoryDataServices)
        {
            this.categoryDataServices = categoryDataServices;
        }

        public void AddCategory(Category category)
        {
            log.Info("In AddCategory method.");
            ValidationResults validationResults = Validation.Validate(category);
            if (validationResults.Count == 0)
            {
                categoryDataServices.AddCategory(category);
            }
            else
            {
                throw new InvalidObjectException();

            }
        }

        public void DeleteCategory(Category category)
        {
            log.Info("In DeleteCategory method");

            if (category != null)
            {
                var currentCategory = categoryDataServices.GetCategoryById(category.Id);
                if (currentCategory != null)
                {
                    log.Info("The user have been deleted!");
                    categoryDataServices.DeleteCategory(category);
                }
                else
                {
                    log.Warn("The user that you want to delete can not be found!");
                    throw new ObjectNotFoundException(category.Name);
                }
            }
            else
            {
                log.Warn("The object passed by parameter is null.");
                throw new NullReferenceException("The object can not be null.");
            }
        }

        public Category GetCategoryById(int id)
        {
            log.Info("In GetCategoryById method");

            if (id < 0 || id == 0)
            {
                log.Warn("The IncorrectIdException was thrown!");
                throw new IncorrectIdException();

            }
            else
            {
                log.Info("The function GetCategoryById was successfully called.");
                return categoryDataServices.GetCategoryById(id);

            }
        }

        public IList<Category> GetListOfCategories()
        {
            log.Info("In GetListOfCategories method");
            return categoryDataServices.GetListOfCategories();

        }

        public void UpdateCategory(Category category)
        {
            log.Info("In UpdateCategory method");

            if (category != null)
            {
                var currentCategory = categoryDataServices.GetCategoryById(category.Id);
                if (currentCategory != null)
                {
                    log.Info("The function UpdateCategory was successfully called.");
                    categoryDataServices.UpdateCategory(category);

                }
                else
                {
                    log.Warn("The ObjectNotFoundException was thrown!");
                    throw new ObjectNotFoundException(category.Name);
                }
            }
            else 
            {
                log.Warn("The NullReferenceException was thrown!");
                throw new NullReferenceException("The object can not be null.");
            }
        }
    }
}
