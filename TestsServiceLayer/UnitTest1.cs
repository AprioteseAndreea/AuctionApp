using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Rhino.Mocks;
using ServiceLayer;
using Rhino.Mocks.Constraints;
using DomainModel;
using System.Collections.Generic;
using DataMapper;
using ServiceLayer.Utils;
using ServiceLayer.ServiceImplementation;
using Rhino.Mocks.Impl;
using Moq;

namespace TestsServiceLayer
{
    [TestClass]
    public class UnitTest1
    {
        private Category category;
        private Product product_one;
        private Product product_two;
        private User user;
        private UserAuction userAuction_one;
        private UserAuction userAuction_two;
        private UserAuction userAuction_three;

        private Money money_one;
        private Money money_two;
        private Configuration configuration_one;
        private Configuration configuration_two;
        private List<Product> userProducts;
        private List<UserAuction> userAuctions;

        private Mock<IProductDataServices> productDataServicesStub;
        private Mock<IUserAuctionDataServices> userAuctionDataServiceStub;
        private Mock<IConfigurationDataServices> configurationDataServicesStub;


        [TestInitialize]
        public void SetUp()
        {
            this.category = new Category
            {
                Name = "Produse alimentare pentru oameni",
            };
            this.user = new User
            {
                Id = 1,
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
            this.product_one = new Product
            {
                Id = 1,
                Name = "primul produs",
                Description = "o descriere foarte interesanta",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2023, 12, 31),
                StartingPrice = this.money_one,
                Category = this.category,
                Status = "Opened",
            };
            this.product_two = new Product
            {
                Id = 2,
                Name = "al doilea produs",
                Description = "o descriere foarte sugestiva",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2023, 12, 31),
                StartingPrice = this.money_two,
                Category = this.category,
                Status = "Opened",
            };
            this.userAuction_one = new UserAuction
            {
                Product = product_one,
                User = user,
                Price = this.money_two,

            };
            this.userAuction_two = new UserAuction
            {
                Product = product_one,
                User = user,
                Price = new Money
                {
                    Amount = 100,
                    Currency = "RON"
                },

            };
            this.userAuction_three = new UserAuction
            {
                Product = product_one,
                User = user,
                Price = this.money_one,

            };
            this.configuration_one = new Configuration
            {
                MaxAuctions = 0,
                InitialScore = 4,
                MinScore = 2,
                Days = 7
            };
            this.configuration_two = new Configuration
            {
                MaxAuctions = 5,
                InitialScore = 4,
                MinScore = 2,
                Days = 7
            };
            this.userProducts = new List<Product>();
            this.userAuctions = new List<UserAuction>();

            userProducts.Add(this.product_one);
            userAuctions.Add(new UserAuction
            {
                Product = product_one,
                User = user,
                Price = new Money
                {
                    Amount = 100,
                    Currency = "RON"
                },
            });

            this.productDataServicesStub = new Mock<IProductDataServices>();
            this.userAuctionDataServiceStub = new Mock<IUserAuctionDataServices>();
            this.configurationDataServicesStub = new Mock<IConfigurationDataServices>();
        }

        [TestMethod]
        [ExpectedException(typeof(MaxAuctionsException), "")]
        public void TestAddProduct_MaxAuctionsException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(new List<Product>());
            productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(configuration_one);

            var prod = new ProductServicesImplementation(productDataServicesStub.Object, configurationDataServicesStub.Object);
            prod.AddProduct(product_one);

        }

        [TestMethod]
        [ExpectedException(typeof(SimilarDescriptionException), "")]
        public void TestAddProduct_SimilarDescriptionException()
        { 
            productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(this.userProducts);
            productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(configuration_two);

            var prod = new ProductServicesImplementation(productDataServicesStub.Object, configurationDataServicesStub.Object);
            prod.AddProduct(product_one);

        }
        [TestMethod]
        [ExpectedException(typeof(IncompatibleCurrencyException), "")]
        public void TestAddUserAuction_IncompatibleCurrencyException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(product_one);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            var prod = new UserAuctionServicesImplementation(userAuctionDataServiceStub.Object, productDataServicesStub.Object);
            prod.AddUserAuction(userAuction_one);
        }

        [TestMethod]
        [ExpectedException(typeof(MinimumBidException), "")]
        public void TestAddUserAuction_MinimumBidException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(product_one);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            var prod = new UserAuctionServicesImplementation(userAuctionDataServiceStub.Object, productDataServicesStub.Object);
            prod.AddUserAuction(userAuction_three);
        }

        [TestMethod]
        [ExpectedException(typeof(OverbiddingException), "")]
        public void TestAddUserAuction_OverbiddingException()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(product_one);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(userAuctions);

            var prod = new UserAuctionServicesImplementation(userAuctionDataServiceStub.Object, productDataServicesStub.Object);
            prod.AddUserAuction(userAuction_three);
          
        }
    }
}