using DomainModel;
using DomainModel.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public interface IProductServices
    {
        IList<ProductDTO> GetListOfProducts();

        void DeleteProduct(ProductDTO product);

        void UpdateProduct(ProductDTO product);

        ProductDTO GetProductById(int id);
        IList<ProductDTO> GetProductsByUserId(int userId);
        IList<ProductDTO> GetOpenProductsByUserId(int userId);

        void AddProduct(ProductDTO product);
    }
}
