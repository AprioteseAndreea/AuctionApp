// <copyright file="EntitiesTests.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace TestDomainLayer
{
    using System;
    using DomainModel;
    using DomainModel.Enums;
    using Microsoft.Practices.EnterpriseLibrary.Validation;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EntitiesTests
    {
        private Category category;
        private Category categoryTwo;
        private CategoryRelation relation;
        private Product product;
        private User user;
        private UserAuction userAuction;
        private Money moneyOne;
        private Money moneyTwo;
        private Money moneyThree;

        private Configuration configuration;

        [TestInitialize]
        public void SetUp()
        {
            this.category = new Category
            {
                Id = 4,
                Name = "Electronice",
            };

            this.categoryTwo = new Category
            {
                Id = 6,
                Name = "Laptopuri",
            };

            this.relation = new CategoryRelation
            {
                ParentCategory = this.category,
                ChildCategory = this.categoryTwo,
            };

            this.user = new User
            {
                Id = 1,
                FirstName = "Andreea",
                LastName = "Apriotese",
                Status = UserStatus.Active,
                Email = "andreea.apriotese@gmail.com",
                Score = 4.00,
                BirthDate = "12.12.2000",
            };

            this.moneyOne = new Money
            {
                Amount = 100,
                Currency = Currency.RON,
            };
            this.moneyThree = new Money
            {
                Amount = 1000,
                Currency = Currency.RON,
            };

            this.moneyTwo = new Money
            {
                Amount = 50,
                Currency = Currency.USD,
            };

            this.product = new Product
            {
                Id = 1,
                Name = "a product",
                Description = "o descriere foarte interesanta",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2023, 12, 31),
                StartingPrice = this.moneyOne,
                Category = this.category,
                Status = AuctionStatus.Open,
            };
            this.userAuction = new UserAuction
            {
                Id = 2,
                Product = this.product,
                User = this.user,
                Price = this.moneyTwo,
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

        /// <summary>
        /// Tests the correct product.
        /// </summary>
        [TestMethod]
        public void TestCorrectProduct()
        {
            Assert.IsNotNull(this.product);
        }

        /// <summary>
        /// Tests the product constructor.
        /// </summary>
        [TestMethod]
        public void TestProductConstructor()
        {
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product name null.
        /// </summary>
        [TestMethod]
        public void TestProductNameNull()
        {
            this.product.Name = null;
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the short name of the product name.
        /// </summary>
        [TestMethod]
        public void TestProductNameShortName()
        {
            this.product.Name = "a";
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the name of the product long enough.
        /// </summary>
        [TestMethod]
        public void TestProductLongEnoughName()
        {
            this.product.Name = "Daniela";
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product null description.
        /// </summary>
        [TestMethod]
        public void TestProductNullDescription()
        {
            this.product.Description = null;
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product short description.
        /// </summary>
        [TestMethod]
        public void TestProductShortDescription()
        {
            this.product.Description = "aa";
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product enough long description.
        /// </summary>
        [TestMethod]
        public void TestProductEnoughLongDescription()
        {
            this.product.Description = "Masina de colectie";
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product owner user null.
        /// </summary>
        [TestMethod]
        public void TestProductOwnerUserNull()
        {
            this.product.OwnerUser = null;
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product owner user not null.
        /// </summary>
        [TestMethod]
        public void TestProductOwnerUserNotNull()
        {
            this.product.OwnerUser = this.user;
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product category null.
        /// </summary>
        [TestMethod]
        public void TestProductCategoryNull()
        {
            this.product.Category = null;
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product category not null.
        /// </summary>
        [TestMethod]
        public void TestProductCategoryNotNull()
        {
            this.product.Category = this.category;
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product starting price null.
        /// </summary>
        [TestMethod]
        public void TestProductStartingPriceNull()
        {
            this.product.StartingPrice = null;
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product starting price not null.
        /// </summary>
        [TestMethod]
        public void TestProductStartingPriceNotNull()
        {
            this.product.StartingPrice = this.moneyOne;
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product wrong status range.
        /// </summary>
        [TestMethod]
        public void TestProductWrongStatusRange()
        {
            this.product.Status = (AuctionStatus)23;
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the product correct status range.
        /// </summary>
        [TestMethod]
        public void TestProductCorrectStatusRange()
        {
            this.product.Status = AuctionStatus.Open;
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the end date before now.
        /// </summary>
        [TestMethod]
        public void TestEndDateBeforeNow()
        {
            this.product.EndDate = new DateTime(2022, 12, 10);
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the start date before now.
        /// </summary>
        [TestMethod]
        public void TestStartDateBeforeNow()
        {
            this.product.StartDate = new DateTime(2022, 12, 10);
            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the start date before now and end date in future.
        /// </summary>
        [TestMethod]
        public void TestStartDateBeforeNowAndEndDateInFuture()
        {
            this.product.StartDate = new DateTime(2022, 12, 10);
            this.product.EndDate = new DateTime(2023, 12, 10);

            ValidationResults validationResults = Validation.Validate(this.product);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the zero money amount.
        /// </summary>
        [TestMethod]
        public void TestZeroMoneyAmount()
        {
            this.moneyOne.Amount = 0;
            ValidationResults validationResults = Validation.Validate(this.moneyOne);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the negative ammount.
        /// </summary>
        [TestMethod]
        public void TestNegativeAmmount()
        {
            this.moneyOne.Amount = -30;
            ValidationResults validationResults = Validation.Validate(this.moneyOne);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the correct ammount.
        /// </summary>
        [TestMethod]
        public void TestCorrectAmmount()
        {
            this.moneyOne.Amount = 30;
            this.moneyOne.Currency = Currency.RON;
            ValidationResults validationResults = Validation.Validate(this.moneyOne);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the correct money currency domain.
        /// </summary>
        [TestMethod]
        public void TestCorrectMoneyCurrencyDomain()
        {
            this.moneyOne.Currency = Currency.RON;
            ValidationResults validationResults = Validation.Validate(this.moneyOne);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the money currency domain.
        /// </summary>
        [TestMethod]
        public void TestMoneyCurrencyDomain()
        {
            this.moneyOne.Currency = (Currency)34;
            ValidationResults validationResults = Validation.Validate(this.moneyOne);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the name of the category enough.
        /// </summary>
        [TestMethod]
        public void TestCategoryEnoughName()
        {
            this.category.Name = "Electrocasnice";
            ValidationResults validationResults = Validation.Validate(this.category);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the short name of the category.
        /// </summary>
        [TestMethod]
        public void TestCategoryShortName()
        {
            this.category.Name = "a";
            ValidationResults validationResults = Validation.Validate(this.category);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the category null products.
        /// </summary>
        [TestMethod]
        public void TestCategoryNullProducts()
        {
            this.category.Products = null;
            ValidationResults validationResults = Validation.Validate(this.category);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the first name of the user null.
        /// </summary>
        [TestMethod]
        public void TestUserNullFirstName()
        {
            this.user.FirstName = null;
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user first name contains numbers.
        /// </summary>
        [TestMethod]
        public void TestUserFirstNameContainsNumbers()
        {
            this.user.FirstName = "Andreea1234";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the first name of the user short.
        /// </summary>
        [TestMethod]
        public void TestUserShortFirstName()
        {
            this.user.FirstName = "b";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the first name of the user enough long.
        /// </summary>
        [TestMethod]
        public void TestUserEnoughLongFirstName()
        {
            this.user.FirstName = "Andreea";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the last name of the user null.
        /// </summary>
        [TestMethod]
        public void TestUserNullLastName()
        {
            this.user.LastName = null;
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user last name contains numbers.
        /// </summary>
        [TestMethod]
        public void TestUserLastNameContainsNumbers()
        {
            this.user.LastName = "Andreea1234";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the last name of the user short.
        /// </summary>
        [TestMethod]
        public void TestUserShortLastName()
        {
            this.user.LastName = "b";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the last name of the user enough long.
        /// </summary>
        [TestMethod]
        public void TestUserEnoughLongLastName()
        {
            this.user.LastName = "Andreea";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user null email.
        /// </summary>
        [TestMethod]
        public void TestUserNullEmail()
        {
            this.user.Email = null;
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user wrong format email.
        /// </summary>
        [TestMethod]
        public void TestUserWrongFormatEmail()
        {
            this.user.Email = "andreea@";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user wrong format email2.
        /// </summary>
        [TestMethod]
        public void TestUserWrongFormatEmail2()
        {
            this.user.Email = "andreea";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user correct format email.
        /// </summary>
        [TestMethod]
        public void TestUserCorrectFormatEmail()
        {
            this.user.Email = "andreea@gmail.com";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user age under eighteen.
        /// </summary>
        [TestMethod]
        public void TestUserAgeUnderEighteen()
        {
            this.user.BirthDate = "10.10.2010";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user age over eighteen.
        /// </summary>
        [TestMethod]
        public void TestUserAgeOverEighteen()
        {
            this.user.BirthDate = "10.10.2000";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user age in the future.
        /// </summary>
        [TestMethod]
        public void TestUserAgeInTheFuture()
        {
            this.user.BirthDate = "10.10.2040";
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user score out of range.
        /// </summary>
        [TestMethod]
        public void TestUserScoreOutOfRange()
        {
            this.user.Score = 6.00;
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user score in range.
        /// </summary>
        [TestMethod]
        public void TestUserScoreInRange()
        {
            this.user.Score = 4.50;
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user status in range.
        /// </summary>
        [TestMethod]
        public void TestUserStatusInRange()
        {
            this.user.Status = UserStatus.Active;
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user status out of range.
        /// </summary>
        [TestMethod]
        public void TestUserStatusOutOfRange()
        {
            this.user.Status = (UserStatus)3234;
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user products null.
        /// </summary>
        [TestMethod]
        public void TestUserProductsNull()
        {
            this.user.Products = null;
            ValidationResults validationResults = Validation.Validate(this.user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user auction null price.
        /// </summary>
        [TestMethod]
        public void TestUserAuctionNullPrice()
        {
            this.userAuction.Price = null;
            ValidationResults validationResults = Validation.Validate(this.userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user auction not null price.
        /// </summary>
        [TestMethod]
        public void TestUserAuctionNotNullPrice()
        {
            this.userAuction.Price = this.moneyOne;
            ValidationResults validationResults = Validation.Validate(this.userAuction);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user auction null product.
        /// </summary>
        [TestMethod]
        public void TestUserAuctionNullProduct()
        {
            this.userAuction.Product = null;
            ValidationResults validationResults = Validation.Validate(this.userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user auction not null product.
        /// </summary>
        [TestMethod]
        public void TestUserAuctionNotNullProduct()
        {
            this.userAuction.Product = this.product;
            ValidationResults validationResults = Validation.Validate(this.userAuction);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user auction null user.
        /// </summary>
        [TestMethod]
        public void TestUserAuctionNullUser()
        {
            this.userAuction.User = null;
            ValidationResults validationResults = Validation.Validate(this.userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the user auction not null user.
        /// </summary>
        [TestMethod]
        public void TestUserAuctionNotNullUser()
        {
            this.userAuction.User = this.user;
            ValidationResults validationResults = Validation.Validate(this.userAuction);
            Assert.AreEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the configuration negative auctions.
        /// </summary>
        [TestMethod]
        public void TestConfigurationNegativeAuctions()
        {
            this.configuration.MaxAuctions = -2;
            ValidationResults validationResults = Validation.Validate(this.configuration);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the configuration out of range initial score.
        /// </summary>
        [TestMethod]
        public void TestConfigurationOutOfRangeInitialScore()
        {
            this.configuration.InitialScore = 7;
            ValidationResults validationResults = Validation.Validate(this.configuration);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the configuration out of range minimum score.
        /// </summary>
        [TestMethod]
        public void TestConfigurationOutOfRangeMinScore()
        {
            this.configuration.MinScore = 10;
            ValidationResults validationResults = Validation.Validate(this.configuration);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the configuration out of range days.
        /// </summary>
        [TestMethod]
        public void TestConfigurationOutOfRangeDays()
        {
            this.configuration.Days = 400;
            ValidationResults validationResults = Validation.Validate(this.configuration);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the category configuration null parent category.
        /// </summary>
        [TestMethod]
        public void TestCategoryConfigurationNullParentCategory()
        {
            this.relation.ParentCategory = null;
            ValidationResults validationResults = Validation.Validate(this.relation);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the category configuration null child category.
        /// </summary>
        [TestMethod]
        public void TestCategoryConfigurationNullChildCategory()
        {
            this.relation.ChildCategory = null;
            ValidationResults validationResults = Validation.Validate(this.relation);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        /// <summary>
        /// Tests the money smaller than another money different currency.
        /// </summary>
        [TestMethod]
        public void TestMoneySmallerThanAnotherMoney_DifferentCurrency()
        {
            Assert.ThrowsException<Exception>(() => this.moneyOne < this.moneyTwo);
        }

        /// <summary>
        /// Tests the money greater than another money different currency.
        /// </summary>
        [TestMethod]
        public void TestMoneyGreaterThanAnotherMoney_DifferentCurrency()
        {
            Assert.ThrowsException<Exception>(() => this.moneyOne > this.moneyTwo);
        }

        /// <summary>
        /// Tests the money smaller than another money same currency.
        /// </summary>
        [TestMethod]
        public void TestMoneySmallerThanAnotherMoney_SameCurrency()
        {
            Assert.IsTrue(this.moneyOne < this.moneyThree);
        }

        /// <summary>
        /// Tests the money greater than another money same currency.
        /// </summary>
        [TestMethod]
        public void TestMoneyGreaterThanAnotherMoney_SameCurrency()
        {
            Assert.IsTrue(this.moneyThree > this.moneyOne);
        }
    }
}
