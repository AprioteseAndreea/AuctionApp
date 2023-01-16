namespace DataMapper
{
    using System.Collections.Generic;
    using DomainModel;

    public interface ICategoryDataServices
    {
        IList<Category> GetListOfCategories();

        void DeleteCategory(Category category);

        void UpdateCategory(Category category);

        Category GetCategoryById(int id);

        void AddCategory(Category category);
    }
}
