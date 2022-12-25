using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestDomainLayer
{
    [TestClass]
    public class ProductTests
    {
        private Category category;
        private Product product;
        private User user;
        private Money money_one;
        private Money money_two;

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

            this.money_one = new Money
            {
                Amount = 100,
                Currency = "RON"
            };

            this.money_two = new Money
            {
                Amount = 50,
                Currency = "USD"
            };

            this.product = new Product
            {
                Name = "a product",
                Description = "o descriere foarte interesanta",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2022, 12, 31),
                StartingPrice = this.money_one,
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
        public void TestProductNameShortName()
        {
            product.Name = "a";
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
        public void TestProductOwnerUserNull()
        {
            product.OwnerUser = null;
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
        public void TestProductStartingPriceNull()
        {
            product.StartingPrice = null;
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

        [TestMethod]
        public void TestSelfValidationForDates()
        {
            product.EndDate = new DateTime(2022, 12, 10); 
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestMoneyAmount()
        {
            money_one.Amount = 0;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestMoneyCurrencyDomain()
        {
            money_one.Currency = "TL";
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestCategoryShortName()
        {
            product.Category.Name = "a";
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

    }
}
