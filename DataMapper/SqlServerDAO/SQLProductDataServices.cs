﻿// <copyright file="SQLProductDataServices.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;
    using DomainModel.Enums;

    public class SQLProductDataServices : IProductDataServices
    {
        /// <summary>
        /// Adds the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void AddProduct(Product product)
        {
            using (var context = new MyApplicationContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Id == product.OwnerUser.Id);
                Category category = context.Categories.FirstOrDefault(c => c.Id == product.Category.Id);

                product.OwnerUser = null;
                product.Category = null;

                user.Products.Add(product);
                category.Products.Add(product);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void DeleteProduct(Product product)
        {
            using (var context = new MyApplicationContext())
            {
                var newProd = product;
                context.Products.Attach(newProd);
                context.Products.Remove(newProd);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all products.
        /// </summary>
        /// <returns></returns>
        public IList<Product> GetAllProducts()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Products.Select(product => product).ToList();
            }
        }

        /// <summary>
        /// Gets the open products by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IList<Product> GetOpenProductsByUserId(int userId)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Products.Where(product => product.OwnerUser.Id == userId && product.Status == AuctionStatus.Open).ToList();
            }
        }

        /// <summary>
        /// Gets the product by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Product GetProductById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Products.Where(product => product.Id == id).SingleOrDefault();
            }
        }

        /// <summary>
        /// Gets the products by user identifier.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns></returns>
        public IList<Product> GetProductsByUserId(int userId)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Products.Where(product => product.OwnerUser.Id == userId).ToList();
            }
        }

        /// <summary>
        /// Updates the product.
        /// </summary>
        /// <param name="product">The product.</param>
        public void UpdateProduct(Product product)
        {
            using (var context = new MyApplicationContext())
            {
                User user = context.Users.FirstOrDefault(u => u.Id == product.OwnerUser.Id);
                Category category = context.Categories.FirstOrDefault(c => c.Id == product.Category.Id);

                var result = user.Products.First(p => p.Id == product.Id);
                var result_two = category.Products.First(p => p.Id == product.Id);

                result.Name = product.Name;
                result_two.Name = product.Name;

                result.Description = product.Description;
                result_two.Description = product.Description;

                context.SaveChanges();
            }
        }
    }
}