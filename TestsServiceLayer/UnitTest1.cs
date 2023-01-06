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

        private Mock<IProductDataServices> productDataServicesMock;
        private Mock<IUserAuctionDataServices> userAuctionDataServicesMock;
        private Mock<IConfigurationDataServices> configurationDataServicesMock;


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

            /*this.product_one = new Product
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
            };*/

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

            this.productDataServicesMock = new Mock<IProductDataServices>();
            this.userAuctionDataServicesMock= new Mock<IUserAuctionDataServices>();
            this.configurationDataServicesMock = new Mock<IConfigurationDataServices>();
        }

       /* [TestMethod]
        [ExpectedException(typeof(MaxAuctionsException), "")]
        public void TestAddProduct_MaxAuctionsException()
        {

            using (mocks.Record())
            {
                Expect.Call(configurationDataServices.GetConfigurationById(1)).Return(this.configuration_one);
                Expect.Call(productDataServices.GetOpenProductsByUserId(product_one.OwnerUser.Id)).Return(new List<Product>());
                Expect.Call(productDataServices.GetProductsByUserId(product_one.OwnerUser.Id)).Return(new List<Product>());

            }
            using (mocks.Playback())
            {
                var prod = new ProductServicesImplementation(productDataServices, configurationDataServices);
                prod.AddProduct(product_one);
            }

        }
        [TestMethod]
        [ExpectedException(typeof(SimilarDescriptionException), "")]
        public void TestAddProduct_SimilarDescriptionException()
        {

            using (mocks.Record())
            {
                Expect.Call(configurationDataServices.GetConfigurationById(1)).Return(this.configuration_two);
                Expect.Call(productDataServices.GetOpenProductsByUserId(product_one.OwnerUser.Id)).Return(new List<Product>());
                Expect.Call(productDataServices.GetProductsByUserId(product_one.OwnerUser.Id)).Return(this.userProducts);

            }
            using (mocks.Playback())
            {
                var prod = new ProductServicesImplementation(productDataServices, configurationDataServices);
                prod.AddProduct(product_two);

            }

        }
        [TestMethod]
        public void TestAddProduct_Successfully()
        {

            using (mocks.Record())
            {
                Expect.Call(configurationDataServices.GetConfigurationById(1)).Return(this.configuration_two);
                Expect.Call(productDataServices.GetOpenProductsByUserId(product_one.OwnerUser.Id)).Return(new List<Product>());
                Expect.Call(productDataServices.GetProductsByUserId(product_one.OwnerUser.Id)).Return(new List<Product>());
              
            }
            using (mocks.Playback())
            {
                
                var prod = new ProductServicesImplementation(productDataServices, configurationDataServices);
                Expect.Call(() => productDataServices.AddProduct(product_one));

                prod.AddProduct(product_two);

                //productDataServices.VerifyAllExpectations();

            }

        }
        [TestMethod]
        [ExpectedException(typeof(IncompatibleCurrencyException), "")]
        public void TestAddUserAuction_IncompatibleCurrencyException()
        {
            using (mocks.Record())
            {
                Expect.Call(productDataServices.GetProductById(userAuction_one.Product)).Return(product_one);
                Expect.Call(userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userAuction_one.User, userAuction_one.Product)).Return(new List<UserAuction>());

            }
            using (mocks.Playback())
            {
                var prod = new UserAuctionServicesImplementation(userAuctionDataServices, productDataServices);
                prod.AddUserAuction(userAuction_one);

            }
        }
        [TestMethod]
        [ExpectedException(typeof(MinimumBidException), "")]
        public void TestAddUserAuction_MinimumBidException()
        {
            using (mocks.Record())
            {
                Expect.Call(productDataServices.GetProductById(userAuction_three.Product)).Return(product_one);
                Expect.Call(userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userAuction_three.User, userAuction_one.Product)).Return(new List<UserAuction>());

            }
            using (mocks.Playback())
            {
                var prod = new UserAuctionServicesImplementation(userAuctionDataServices, productDataServices);
                prod.AddUserAuction(userAuction_three);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(OverbiddingException), "")]
        public void TestAddUserAuction_OverbiddingException()
        {
            using (mocks.Record())
            {
                Expect.Call(productDataServices.GetProductById(userAuction_three.Product)).Return(product_one);
                Expect.Call(userAuctionDataServices.GetUserAuctionsByUserIdandProductId(userAuction_three.User, userAuction_one.Product)).Return(userAuctions);

            }
            using (mocks.Playback())
            {
                var prod = new UserAuctionServicesImplementation(userAuctionDataServices, productDataServices);
                prod.AddUserAuction(userAuction_three);

            }
        }*/
    }
}
