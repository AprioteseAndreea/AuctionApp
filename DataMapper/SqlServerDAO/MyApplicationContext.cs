using DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

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
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<UserAuction> UserAuctions { get; set; }
    }
}
