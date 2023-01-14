using DomainModel.enums;
using DomainModel.Enums;
using DomainModel;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel.DTO;

namespace TestDomainLayer
{
    [TestClass]
    public class DtoTests
    {
        private CategoryDTO category;
        private CategoryDTO category_two;
        private CategoryRelationDTO relation;
        private ProductDTO product;
        private UserDTO user;
        private UserAuctionDTO userAuction;
        private Money money_one;
        private Money money_two;
        private Money money_three;

        private ConfigurationDTO configuration;

        [TestInitialize]
        public void SetUp()
        {
            this.category = new CategoryDTO
            {
                Id = 4,
                Name = "Electronice",
            };

            this.category_two = new CategoryDTO
            {
                Id = 6,
                Name = "Laptopuri"
            };

            this.relation = new CategoryRelationDTO
            {
                ParentCategoryId = this.category.Id,
                ChildCategoryId = this.category_two.Id,
            };

            this.user = new UserDTO
            {
                Id = 1,
                FirstName = "Andreea",
                LastName = "Apriotese",
                Status = UserStatus.Active,
                Email = "andreea.apriotese@gmail.com",
                Score = 4.00,
                BirthDate = "12.12.2000"
            };

            this.money_one = new Money
            {
                Amount = 100,
                Currency = Currency.RON
            };
            this.money_three = new Money
            {
                Amount = 1000,
                Currency = Currency.RON
            };

            this.money_two = new Money
            {
                Amount = 50,
                Currency = Currency.USD
            };

            this.product = new ProductDTO
            {
                Id = 1,
                Name = "a product",
                Description = "o descriere foarte interesanta",
                OwnerUserId = this.user.Id,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2023, 12, 31),
                StartingPrice = this.money_one,
                CategoryId = this.category.Id,
                Status = AuctionStatus.Open,
            };
            this.userAuction = new UserAuctionDTO
            {
                Id = 2,
                ProductId = product.Id,
                UserId = user.Id,
                Price = this.money_two,

            };
            this.configuration = new ConfigurationDTO
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
        public void TestProductOwnerUserIdInRange()
        {
            product.OwnerUserId = 3;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductOwnerIdUserZero()
        {
            product.OwnerUserId = 0;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductOwnerUserIdNegative()
        {
            product.OwnerUserId = -473;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductCategoryIdInRange()
        {
            product.CategoryId = 3;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductCategoryZero()
        {
            product.CategoryId = 0;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductCategoryIdNegative()
        {
            product.CategoryId = -7;
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
        public void TestProductStartingPriceNotNull()
        {
            product.StartingPrice = money_one;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestProductWrongStatusRange()
        {
            product.Status = (AuctionStatus)23;
            ValidationResults validationResults = Validation.Validate(product);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestProductCorrectStatusRange()
        {
            product.Status = AuctionStatus.Open;
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
            money_one.Currency = Currency.RON;
            ValidationResults validationResults = Validation.Validate(money_one);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestCorrectMoneyCurrencyDomain()
        {
            money_one.Currency = Currency.RON;
            ValidationResults validationResults = Validation.Validate(money_one);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestMoneyCurrencyDomain()
        {
            money_one.Currency = (Currency)34;
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
        public void TestUserNullFirstName()
        {
            user.FirstName = null;
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserFirstNameContainsNumbers()
        {
            user.FirstName = "Andreea1234";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserShortFirstName()
        {
            user.FirstName = "b";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestUserEnoughLongFirstName()
        {
            user.FirstName = "Andreea";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreEqual(0, validationResults.Count);


        }
        [TestMethod]
        public void TestUserNullLastName()
        {
            user.LastName = null;
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserLastNameContainsNumbers()
        {
            user.LastName = "Andreea1234";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserShortLastName()
        {
            user.LastName = "b";
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestUserEnoughLongLastName()
        {
            user.LastName = "Andreea";
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
        public void TestUserWrongFormatEmail2()
        {
            user.Email = "andreea";
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
        public void TestUserStatusInRange()
        {
            user.Status = UserStatus.Active;
            ValidationResults validationResults = Validation.Validate(user);
            Assert.AreEqual(0, validationResults.Count);

        }

        [TestMethod]
        public void TestUserStatusOutOfRange()
        {
            user.Status = (UserStatus)3234;
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
        public void TestUserAuctionProductIdInRange()
        {
            userAuction.ProductId = 3;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserAuctionProductIdZero()
        {
            userAuction.ProductId = 0;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserAuctionProductIdNegative()
        {
            userAuction.ProductId = -4;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
        }

        [TestMethod]
        public void TestUserAuctionUserIdInRange()
        {
            userAuction.UserId = 2;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserAuctionUserIdZero()
        {
            userAuction.UserId = 0;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestUserAuctionUserIdNegative()
        {
            userAuction.UserId = -4;
            ValidationResults validationResults = Validation.Validate(userAuction);
            Assert.AreNotEqual(0, validationResults.Count);
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
        public void TestCategoryRelationParentCategoryInRange()
        {
            relation.ParentCategoryId = 1;
            ValidationResults validationResults = Validation.Validate(relation);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestCategoryRelationParentCategoryZero()
        {
            relation.ParentCategoryId = 0;
            ValidationResults validationResults = Validation.Validate(relation);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestCategoryRelationParentCategoryNegative()
        {
            relation.ParentCategoryId = -4;
            ValidationResults validationResults = Validation.Validate(relation);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestCategoryRelationChildCategoryInRange()
        {
            relation.ChildCategoryId = 3;
            ValidationResults validationResults = Validation.Validate(relation);
            Assert.AreEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestCategoryRelationChildCategoryIdZero()
        {
            relation.ChildCategoryId = 0;
            ValidationResults validationResults = Validation.Validate(relation);
            Assert.AreNotEqual(0, validationResults.Count);
        }
        [TestMethod]
        public void TestCategoryRelationChildCategoryIdNegative()
        {
            relation.ChildCategoryId = -5;
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
