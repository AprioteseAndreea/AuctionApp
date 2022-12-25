using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IProductServices
    {
        IList<Product> GetListOfProducts();

        void DeleteProduct(Product product);

        void UpdateProduct(Product product);

        void GetProductById(int id);
        IList<Product> GetProductsByUserId(int userId);

        void AddProduct(Product product);
    }
}
