using DataMapper;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceImplementation
{
    public class ProductServicesImplementation : IProductServices
    {
        public void AddProduct(Product product)
        {
            try
            {
                DAOFactoryMethod.CurrentDAOFactory.ProducDataServices.AddProduct(product);
            }
            catch
            {

            }
        }

        public void DeleteProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetListOfProducts()
        {
            return DAOFactoryMethod.CurrentDAOFactory.ProducDataServices.GetAllProducts();

        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Product> GetProductsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public void UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
