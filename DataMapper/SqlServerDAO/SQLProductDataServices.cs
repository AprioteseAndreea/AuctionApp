namespace DataMapper.SqlServerDAO
{
    using System.Collections.Generic;
    using System.Linq;
    using DomainModel;
    using DomainModel.Enums;

    public class SQLProductDataServices : IProductDataServices
    {
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

        public IList<Product> GetAllProducts()
        {
            using (var context = new MyApplicationContext())
            {
                return context.Products.Select(product => product).ToList();
            }
        }

        public IList<Product> GetOpenProductsByUserId(int userId)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Products.Where(product => product.OwnerUser.Id == userId && product.Status == AuctionStatus.Open).ToList();
            }
        }

        public Product GetProductById(int id)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Products.Where(product => product.Id == id).SingleOrDefault();
            }
        }

        public IList<Product> GetProductsByUserId(int userId)
        {
            using (var context = new MyApplicationContext())
            {
                return context.Products.Where(product => product.OwnerUser.Id == userId).ToList();
            }
        }

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