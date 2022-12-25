using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace TestDomainModel
{
    [TestClass]
    public class ProductTests
    {
        private Category category = new();
        private Product product = new();
        private User user = new();
        private Money money = new();

        [TestInitialize]
        public void SetUp()
        {
            this.category = new Category
            {
                Name = "Produse alimentare pentru oameni",
            };

            this.user = new User
            {
                Name = "Andreea Apriotese",
                Score = 4.50M,
                Status = "activ",
            };

            this.money = new Money
            {
                Amount = 100,
                Currency = "RON"
            };



            this.product = new Product
            {
                Name = "a product",
                Description = "o descriere foarte interesanta",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2022, 12, 31),
                StartingPrice = this.money,
                Category = this.category,
                Status = "Opened"
            };
        }

        [TestMethod]
        public void TestCorrectProduct()
        {
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestProductNameNull()
        {
            product.Name = null;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestShortDescription()
        {
            product.Description = "aa";
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestProductCategoryNull()
        {
            product.Category = null;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestProductStatusRange()
        {
            product.Status = "opened";
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }



        /* [TestMethod]
         public void TestMethodNameNotTooLong()
         {
             customer.Name = new String('a', 50);
             ValidationResults validationResults = Validation.Validate<Customer>(customer);
             Assert.AreEqual(validationResults.Count, 0);
         }

         [TestMethod]
         public void TestMethodNameTooShort()
         {
             customer.Name = new String('a', 2);
             ValidationResults validationResults = Validation.Validate<Customer>(customer);
             Assert.AreNotEqual(0, validationResults.Count);
         }

         [TestMethod]
         public void TestMethodNameTooLong()
         {
             customer.Name = new String('a', 51);
             ValidationResults validationResults = Validation.Validate<Customer>(customer);
             Assert.AreNotEqual(0, validationResults.Count);
         }

         [TestMethod]
         public void TestNullName()
         {
             customer.Name = null;
             ValidationResults validationResults = Validation.Validate<Customer>(customer);
             Assert.AreNotEqual(0, validationResults.Count);
         }*/

    }
}