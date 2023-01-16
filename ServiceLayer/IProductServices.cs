// <copyright file="IProductServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace ServiceLayer
{
    using System.Collections.Generic;
    using DomainModel.DTO;

    public interface IProductServices
    {
        /// <summary>
        /// Gets the list of products.
        /// </summary>
        /// <returns></returns>
        IList<ProductDTO> GetListOfProducts();

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="product">The product.</param>
        void DeleteProduct(ProductDTO product);

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        void UpdateProduct(ProductDTO product);

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        ProductDTO GetProductById(int id);

        /// <summary>
        /// Gets the products by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<ProductDTO> GetProductsByUserId(int userId);

        /// <summary>
        /// Gets the open products by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<ProductDTO> GetOpenProductsByUserId(int userId);

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        void AddProduct(ProductDTO product);
    }
}
