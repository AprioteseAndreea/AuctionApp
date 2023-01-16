// <copyright file="IProductDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper
{
    using System.Collections.Generic;
    using DomainModel;

    public interface IProductDataServices
    {
        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns></returns>
        IList<Product> GetAllProducts();

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        Product GetProductById(int id);

        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        void AddProduct(Product product);

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="product">The product.</param>
        void DeleteProduct(Product product);

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        void UpdateProduct(Product product);

        /// <summary>
        /// Gets the products by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<Product> GetProductsByUserId(int userId);

        /// <summary>
        /// Gets the open products by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        IList<Product> GetOpenProductsByUserId(int userId);
    }
}