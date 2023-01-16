namespace DataMapper.SqlServerDAO
{
    using System.Data.Entity;
    using DomainModel;

    public class MyApplicationContext : DbContext
    {
        public MyApplicationContext()
            : base("myConStr")
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
