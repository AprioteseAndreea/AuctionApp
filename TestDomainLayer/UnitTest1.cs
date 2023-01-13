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
        private Category category_two;
        private CategoryRelation relation;
        private Product product;
        private User user;
        private UserAuction userAuction;
        private Money money_one;
        private Money money_two;
        private Money money_three;

        private Configuration configuration;

        [TestInitialize]
        public void SetUp()
        {
            this.category = new Category
            {
                Id = 4,
                Name = "Electronice",
            };

            this.category_two = new Category
            {
                Id = 6,
                Name = "Laptopuri"
            };

            this.relation = new CategoryRelation
            {
                Id = 1,
                ParentCategory = this.category,
                ChildCategory = this.category_two,
            };

            this.user = new User
            {
                Id=1,
                Name = "Andreea Apriotese",
                Status = "Active",
                Email = "andreea.apriotese@gmail.com",
                Score = 4.00,
                BirthDate = "12.12.2000"
            };

            this.money_one = new Money
            {
                Amount = 100,
                Currency = "RON"
            };
            this.money_three = new Money
            {
                Amount = 1000,
                Currency = "RON"
            };

            this.money_two = new Money
            {
                Amount = 50,
                Currency = "USD"
            };

            this.product = new Product
            {
                Id=1,
                Name = "a product",
                Description = "o descriere foarte interesanta",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2023, 12, 31),
                StartingPrice = this.money_one,
                Category = this.category,
                Status = "Open",
            };
            this.userAuction = new UserAuction
            {
                Id=2,
                Product = product,
                User = user,
                Price = this.money_two,

            };
            this.configuration = new Configuration
            {
                Id = 1,
                MaxAuctions = 5,
                InitialScore = 4,
                MinScore = 3,
                Days = 30,
            };
        }

        [TestMethod]
        public void TestCorrectProduct()
        {
            Assert.IsNotNull(product);
        }

        [TestMethod]
        public void TestProductConstructor()
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
        public void TestProductNullDescription()
        {
            product.Description = null;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductShortDescription()
        {
            product.Description = "aa";
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestProductEnoughLongDescription()
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
        public void TestProductOwnerUserNotNull()
        {
            product.OwnerUser = user;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductCategoryNull()
        {
            product.Category = null;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductCategoryNotNull()
        {
            product.Category = category;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductStartingPriceNull()
        {
            product.StartingPrice = null;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductStartingPriceNotNull()
        {
            product.StartingPrice = money_one;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductNullStatus()
        {
            product.Status = null;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductWrongStatusRange()
        {
            product.Status = "opened";
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductCorrectStatusRange()
        {
            product.Status = "Open";
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestEndDateBeforeNow()
        {
            product.EndDate = new DateTime(2022, 12, 10);
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestStartDateBeforeNow()
        {
            product.StartDate = new DateTime(2022, 12, 10);
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestStartDateBeforeNowAndEndDateInFuture()
        {
            product.StartDate = new DateTime(2022, 12, 10);
            product.EndDate = new DateTime(2023, 12, 10);

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
        public void TestCorrectAmmount()
        {
            money_one.Amount = 30;
            ValidationResults validationResults = Validation.Validate(money_one);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestCorrectMoneyCurrencyDomain()
        {
            money_one.Currency = "RON";
            ValidationResults validationResults = Validation.Validate(money_one);
            Assert.AreEqual(0, validationResults.Count);
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
        public void TestCategoryNullProducts()
        {
            category.Products = null;
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
        public void TestUserNameContainsNumbers()
        {
            user.Name = "Andreea1234";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreEqual(0, validationResults.Count);
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
            Assert.AreEqual(0, validationResults.Count);


        }
        [TestMethod]
        public void TestUserNullEmail()
        {
            user.Email = null;
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);

        }
        [TestMethod]
        public void TestUserWrongFormatEmail()
        {
            user.Email = "andreea@";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);

        }
        [TestMethod]
        public void TestUserCorrectFormatEmail()
        {
            user.Email = "andreea@gmail.com";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreEqual(0, validationResults.Count);

        }
        [TestMethod]
        public void TestUserAgeUnderEighteen()
        {
            user.BirthDate = "10.10.2010";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);

        }
        [TestMethod]
        public void TestUserAgeOverEighteen()
        {
            user.BirthDate = "10.10.2000";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreEqual(0, validationResults.Count);

        }
        [TestMethod]
        public void TestUserAgeInTheFuture()
        {
            user.BirthDate = "10.10.2040";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);

        }
        [TestMethod]
        public void TestUserScoreOutOfRange()
        {
            user.Score = 6.00;
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);

        }

        [TestMethod]
        public void TestUserScoreInRange()
        {
            user.Score = 4.50;
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreEqual(0, validationResults.Count);

        }

        [TestMethod]
        public void TestUserStatusNull()
        {
            user.Status = null;
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);

        }

        [TestMethod]
        public void TestUserStatusInRange()
        {
            user.Status = "Active";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreEqual(0, validationResults.Count);

        }

        [TestMethod]
        public void TestUserStatusOutOfRange()
        {
            user.Status = "incert";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);

        }
        [TestMethod]
        public void TestUserProductsNull()
        {
            user.Products = null;
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);

        }


        [TestMethod]
        public void TestUserAuctionNullPrice()
        {
            userAuction.Price = null;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserAuctionNotNullPrice()
        {
            userAuction.Price = money_one;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestUserAuctionNullProduct()
        {
            userAuction.Product = null;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserAuctionNotNullProduct()
        {
            userAuction.Product = product;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestUserAuctionNullUser()
        {
            userAuction.User = null;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserAuctionNotNullUser()
        {
            userAuction.User = user;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestConfigurationNegativeAuctions()
        {
            configuration.MaxAuctions = -2;
            ValidationResults validationResults = Validation.Validate(configuration);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestConfigurationOutOfRangeInitialScore()
        {
            configuration.InitialScore = 7;
            ValidationResults validationResults = Validation.Validate(configuration);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestConfigurationOutOfRangeMinScore()
        {
            configuration.MinScore = 10;
            ValidationResults validationResults = Validation.Validate(configuration);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestConfigurationOutOfRangeDays()
        {
            configuration.Days = 400;
            ValidationResults validationResults = Validation.Validate(configuration);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestCategoryConfigurationNullParentCategory()
        {
            relation.ParentCategory = null;
            ValidationResults validationResults = Validation.Validate(relation);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestCategoryConfigurationNullChildCategory()
        {
            relation.ChildCategory = null;
            ValidationResults validationResults = Validation.Validate(relation);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod()]
        public void TestMoneySmallerThanAnotherMoney_DifferentCurrency()
        {
            Assert.ThrowsException<Exception>(() => money_one < money_two);
        }

        [TestMethod()]
        public void TestMoneyGreaterThanAnotherMoney_DifferentCurrency()
        {
            Assert.ThrowsException<Exception>(() => money_one > money_two);
        }

        [TestMethod()]
        public void TestMoneySmallerThanAnotherMoney_SameCurrency()
        {
            Assert.IsTrue(money_one < money_three);
        }

        [TestMethod()]
        public void TestMoneyGreaterThanAnotherMoney_SameCurrency()
        {
            Assert.IsTrue(money_three > money_one);
        }

    }
}
