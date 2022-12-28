using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Common.Utility;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestDomainLayer
{
    [TestClass]
    public class ProductTests
    {
        private Category category;
        private Product product;
        private User user;
        private UserAuction userAuction;
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
                EndDate = new DateTime(2023, 12, 31),
                StartingPrice = this.money_one,
                Category = this.category,
                Status = "Opened",
            };
            this.userAuction = new UserAuction
            {
                Product = 1,
                User = 1,
                Price = this.money_two,
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
        public void TestProductLongEnoughName()
        {
            product.Name = "Daniela";
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestProductShortDescription()
        {
            product.Description = "aa";
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestProductEnoughDescription()
        {
            product.Description = "Masina de colectie";
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreEqual(0, validationResults.Count);
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
        public void TestSelfValidationForStartDate()
        {
            product.StartDate = new DateTime(2022, 12, 10);
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestZeroMoneyAmount()
        {
            money_one.Amount = 0;
            ValidationResults validationResults = Validation.Validate(money_one);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestNegativeAmmount()
        {
            money_one.Amount = -30;
            ValidationResults validationResults = Validation.Validate(money_one);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestMoneyCurrencyDomain()
        {
            money_one.Currency = "TL";
            ValidationResults validationResults = Validation.Validate(money_one);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestMoneyNullCurrency()
        {
            money_one.Currency = null;
            ValidationResults validationResults = Validation.Validate(money_one);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestCategoryEnoughName()
        {
            category.Name = "Electrocasnice";
            ValidationResults validationResults = Validation.Validate(category);
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestCategoryShortName()
        {
            category.Name = "a";
            ValidationResults validationResults = Validation.Validate(category);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserNullName()
        {
            user.Name = null;
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserShortName()
        {
            user.Name = "b";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestUserEnoughLongName()
        {
            user.Name = "Andreea";
            ValidationResults validationResults = Validation.Validate(user);

        }
            [TestMethod]
        public void TestUserAuctionNullPrice()
        {
            userAuction.Price = null;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
        }
    }
}
