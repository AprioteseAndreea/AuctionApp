// <copyright file="MyApplicationContext.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Data.Entity;
    using DomainModel;

    public class MyApplicationContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyApplicationContext"/> class.
        /// </summary>
        public MyApplicationContext()
            : base("myConStr")
        {
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the categories.
        /// </summary>
        /// <value>
        /// The categories.
        /// </value>
        public virtual DbSet<Category> Categories { get; set; }

        /// <summary>
        /// Gets or sets the category relations.
        /// </summary>
        /// <value>
        /// The category relations.
        /// </value>
        public virtual DbSet<CategoryRelation> CategoryRelations { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the user auctions.
        /// </summary>
        /// <value>
        /// The user auctions.
        /// </value>
        public virtual DbSet<UserAuction> UserAuctions { get; set; }

        /// <summary>
        /// Gets or sets the configurations.
        /// </summary>
        /// <value>
        /// The configurations.
        /// </value>
        public virtual DbSet<Configuration> Configurations { get; set; }
    }
}
