namespace DataMapper
{
    using System.Collections.Generic;
    using DomainModel;

    public interface IProductDataServices
    {
        IList<Product> GetAllProducts();

        Product GetProductById(int id);

        void AddProduct(Product product);

        void DeleteProduct(Product product);

        void UpdateProduct(Product product);

        IList<Product> GetProductsByUserId(int userId);

        IList<Product> GetOpenProductsByUserId(int userId);
    }
}