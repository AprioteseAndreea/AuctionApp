using DomainModel;
using System.Data.Entity;

namespace DataMapper.SqlServerDAO
{
    class MyApplicationContext : DbContext
    {
        /// <summary>Initializes a new instance of the <see cref="MyApplicationContext" /> class.</summary>
        public MyApplicationContext() : base("myConStr")
        {

        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CategoryRelation> CategoryRelations { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<UserAuction> UserAuctions { get; set; }
        public virtual DbSet<Configuration> Configurations { get; set; }
    }
}
