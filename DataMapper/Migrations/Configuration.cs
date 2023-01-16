namespace DataMapper.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<SqlServerDAO.MyApplicationContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SqlServerDAO.MyApplicationContext context)
        {
        }
    }
}
