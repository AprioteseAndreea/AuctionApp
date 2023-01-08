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
using System.Linq.Expressions;

namespace TestsServiceLayer
{
    [TestClass]
    public class UnitTest1
    {
        private Category category;
        private Category invalidCategory;

        private Product productFirst;
        private Product productSecond;
        private Product invalidProduct;

        private User user;
        private User invalidUser;

        private UserAuction userAuctionFirst;
        private UserAuction userAuctionSecond;
        private UserAuction userAuctionThird;
        private UserAuction userAuctionFourth;


        private Money moneyFirst;
        private Money moneySecond;
        private Money moneyThird;

        private const int POSITIVE_USER_ID = 5;
        private const int NEGATIVE_USER_ID = -5;

        private const int POSITIVE_PRODUCT_ID = 4;
        private const int NEGATIVE_PRODUCT_ID = -4;

        private Configuration configurationFirst;
        private Configuration configurationSecond;
        private Configuration invalidConfiguration;

        private List<Product> userProducts;
        private List<UserAuction> userAuctions;

        private Mock<IProductDataServices> productDataServicesStub;
        private Mock<IUserAuctionDataServices> userAuctionDataServiceStub;
        private Mock<IConfigurationDataServices> configurationDataServicesStub;
        private Mock<IUserDataServices> userDataServicesStub;
        private Mock<ICategoryDataServices> categoryDataServicesStub;

        private ProductServicesImplementation prod;
        private UserAuctionServicesImplementation userAuction;
        private UserServicesImplementation userService;
        private CategoryServicesImplementation categoryServices;
        private ConfigurationServicesImplementation configurationServices;

        [TestInitialize]
        public void SetUp()
        {
            this.category = new Category
            {
                Name = "Produse alimentare pentru oameni",
            };
            this.invalidCategory = new Category
            {
                Name = "a",
            };
            this.user = new User
            {
                Id = 1,
                Name = "Andreea Apriotese",
                Status = "Active",
                Email = "andreea.apriotese@gmail.com",
                Score = 4.00,
                BirthDate = "12.12.2000"
            };
            this.invalidUser = new User
            {
                Id = 1,
                Name = "Andreea Apriotese",
                Status = "Active",
                Email = "andreea.apriotese",
                Score = 4.00,
                BirthDate = "12.12.2000"
            };
            this.moneyFirst = new Money
            {
                Amount = 100,
                Currency = "RON"
            };
            this.moneySecond = new Money
            {
                Amount = 50,
                Currency = "USD"
            };
            this.moneyThird = new Money
            {
                Amount = 150,
                Currency = "RON"
            };
            this.productFirst = new Product
            {
                Id = 1,
                Name = "primul produs",
                Description = "o descriere foarte interesanta",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2023, 12, 31),
                StartingPrice = this.moneyFirst,
                Category = this.category,
                Status = "Open",
            };
            this.productSecond = new Product
            {
                Id = 2,
                Name = "al doilea produs",
                Description = "licitez un produs rar",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2023, 12, 31),
                StartingPrice = this.moneySecond,
                Category = this.category,
                Status = "Open",
            };
            this.invalidProduct = new Product
            {
                Id = 2,
                Name = "al doilea produs",
                Description = "licitez un produs rar",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2022, 12, 31),
                StartingPrice = this.moneySecond,
                Category = this.category,
                Status = "Open",
            };
            this.userAuctionFirst = new UserAuction
            {
                Product = productFirst,
                User = user,
                Price = this.moneySecond,

            };
            this.userAuctionSecond = new UserAuction
            {
                Product = productFirst,
                User = user,
                Price = moneyThird,

            };
            this.userAuctionThird = new UserAuction
            {
                Product = productFirst,
                User = user,
                Price = this.moneyFirst,

            };
            this.userAuctionFourth = new UserAuction
            {
                Product = productFirst,
                User = user,
                Price = new Money
                {
                    Amount = 400,
                    Currency = "RON"
                },

            };
            this.configurationFirst = new Configuration
            {
                MaxAuctions = 0,
                InitialScore = 4,
                MinScore = 2,
                Days = 7
            };
            this.configurationSecond = new Configuration
            {
                MaxAuctions = 5,
                InitialScore = 4,
                MinScore = 2,
                Days = 7
            };
            this.invalidConfiguration = new Configuration
            {
                MaxAuctions = 5,
                InitialScore = 8,
                MinScore = 2,
                Days = 7
            };
            this.userProducts = new List<Product>();
            this.userAuctions = new List<UserAuction>();

            userProducts.Add(this.productFirst);
            userAuctions.Add(new UserAuction
            {
                Product = productFirst,
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
            this.userDataServicesStub = new Mock<IUserDataServices>();
            this.categoryDataServicesStub = new Mock<ICategoryDataServices>();
            prod = new ProductServicesImplementation(productDataServicesStub.Object, configurationDataServicesStub.Object, userDataServicesStub.Object);
            userAuction = new UserAuctionServicesImplementation(userAuctionDataServiceStub.Object, productDataServicesStub.Object, userDataServicesStub.Object);
            userService = new UserServicesImplementation(userDataServicesStub.Object, configurationDataServicesStub.Object);
            categoryServices = new CategoryServicesImplementation(categoryDataServicesStub.Object);
            configurationServices = new ConfigurationServicesImplementation(configurationDataServicesStub.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestAddProduct_NullObjectException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(new List<Product>());
            productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(configurationFirst);

            prod.AddProduct(null);

        }

        [TestMethod]
        [ExpectedException(typeof(MaxAuctionsException), "The maximum number of licitations has been reached!")]
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
                .Returns(configurationFirst);

            prod.AddProduct(productFirst);

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
                .Returns(configurationSecond);

            prod.AddProduct(productFirst);

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddProduct_InvalidObjectException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(this.userProducts);
            productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(configurationSecond);

            prod.AddProduct(invalidProduct);

        }

        [TestMethod]
        public void TestAddProduct_SimilarDescriptionWasNotThrown()
        {
            productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(this.userProducts);
            productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(configurationSecond);

            prod.AddProduct(productSecond);

        }

        [TestMethod]
        public void TestAddProduct_Successfully()
        {
            productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(new List<Product>());
            productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(configurationSecond);

            prod.AddProduct(productSecond);

        }

        [TestMethod]
        public void TestDeleteProduct_Successfully()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(productFirst);

            prod.DeleteProduct(productSecond);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestDeleteProduct_NullProduct()
        {
            prod.DeleteProduct(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestDeleteProduct_ObjectNotFoundException()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Equals(null);

            prod.DeleteProduct(productSecond);
        }

        [TestMethod]
        public void TestGetListOfProducts_Successfully()
        {
            prod.GetListOfProducts();
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetOpenProductsByUserId_IncorrectIdException()
        {
            prod.GetOpenProductsByUserId(NEGATIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetOpenProductsByUserId_Successfully()
        {
            userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Returns(user);
            prod.GetOpenProductsByUserId(POSITIVE_USER_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestGetOpenProductsByUserId_ObjectNotFoundException()
        {
            userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Equals(null);
            prod.GetOpenProductsByUserId(POSITIVE_USER_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetProductsByUserId_IncorrectIdException()
        {
            prod.GetProductsByUserId(NEGATIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetProductsByUserId_Successfully()
        {
            userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Returns(user);
            prod.GetProductsByUserId(POSITIVE_USER_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestGetProductsByUserId_ObjectNotFoundException()
        {
            userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Equals(null);
            prod.GetProductsByUserId(POSITIVE_USER_ID);
        }


        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetProductById_IncorrectIdException()
        {
            prod.GetProductById(NEGATIVE_PRODUCT_ID);
        }

        [TestMethod]
        public void TestGetProductById_Successfully()
        {

            prod.GetProductById(POSITIVE_PRODUCT_ID);
        }

        [TestMethod]
        public void TestUpdateProduct_Successfully()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(productFirst);

            prod.UpdateProduct(productSecond);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestUpdateProduct_NullProduct()
        {
            prod.UpdateProduct(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateProduct_ObjectNotFoundException()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Equals(null);

            prod.UpdateProduct(productSecond);
        }

        [TestMethod]
        [ExpectedException(typeof(IncompatibleCurrencyException), "")]
        public void TestAddUserAuction_IncompatibleCurrencyException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());
            userAuction.AddUserAuction(userAuctionFirst);
        }

        [TestMethod]
        [ExpectedException(typeof(MinimumBidException), "")]
        public void TestAddUserAuction_MinimumBidException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            userAuction.AddUserAuction(userAuctionThird);
        }

        [TestMethod]
        [ExpectedException(typeof(OverbiddingException), "")]
        public void TestAddUserAuction_OverbiddingException()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(userAuctions);

            userAuction.AddUserAuction(userAuctionThird);
        }
        [TestMethod]
        [ExpectedException(typeof(OverbiddingException), "")]
        public void TestAddUserAuction_OverbiddingException_AmountIsOver300Percent()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(userAuctions);

            userAuction.AddUserAuction(userAuctionFourth);
        }
        [TestMethod]
        public void TestAddUserAuction_Successfully()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            userAuction.AddUserAuction(userAuctionSecond);
        }

        [TestMethod]
        public void TestDeleteUserAuction_Successfully()
        {
            userAuctionDataServiceStub
              .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
              .Returns(userAuctionFirst);

            userAuction.DeleteUserAuction(userAuctionFirst);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestDeleteUserAuction_NullProduct()
        {
            userAuction.DeleteUserAuction(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestDeleteUserAuction_ObjectNotFoundException()
        {
            userAuctionDataServiceStub
           .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
           .Equals(null);

            userAuction.DeleteUserAuction(userAuctionFirst);
        }

        [TestMethod]
        public void TestGetListOfUserAuctions_Successfully()
        {
            userAuction.GetListOfUserAuctions();
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionById_IncorrectIdException()
        {
            userAuction.GetUserAuctionById(NEGATIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetUserAuctionById_Successfully()
        {
            userAuction.GetUserAuctionById(POSITIVE_USER_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserId_IncorrectIdException()
        {
            userAuction.GetUserAuctionsByUserId(NEGATIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetUserAuctionsByUserId_Successfully()
        {
            userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Returns(user);
            userAuction.GetUserAuctionsByUserId(POSITIVE_USER_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The ObjectNotFoundException was thrown!")]
        public void TestGetUserAuctionsByUserId_ObjectNotFoundException()
        {
            userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Equals(null);
            userAuction.GetUserAuctionsByUserId(POSITIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetUserAuctionsByUserIdandProductId_Successfully()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(POSITIVE_USER_ID, POSITIVE_PRODUCT_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_UserIdIsLessThan0()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(NEGATIVE_USER_ID, POSITIVE_PRODUCT_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_ProductIdIsLessThan0()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(POSITIVE_USER_ID, NEGATIVE_PRODUCT_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_ProductIdAndUserIdIsLessThan0()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(NEGATIVE_USER_ID, NEGATIVE_PRODUCT_ID);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_UserIdIs0()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(0, NEGATIVE_PRODUCT_ID);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_ProductIdIs0()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(POSITIVE_USER_ID, 0);
        }
        [TestMethod]
        public void TestUpdateUserAuction_Successfully()
        {
            userAuctionDataServiceStub
              .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
              .Returns(userAuctionFirst);

            userAuction.UpdateUserAuction(userAuctionFirst);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestUpdateUserAuction_NullProduct()
        {
            userAuction.UpdateUserAuction(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateUserAuction_ObjectNotFoundException()
        {
            userAuctionDataServiceStub
              .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
              .Equals(null);

            prod.UpdateProduct(productSecond);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddUser_InvalidObjectException()
        {
            configurationDataServicesStub
             .Setup(x => x.GetConfigurationById(1))
             .Returns(configurationSecond);
            userService.AddUser(invalidUser);
        }

        [TestMethod]
        public void TestAddUser_Successfully()
        {
            configurationDataServicesStub
                .Setup(x => x.GetConfigurationById(1))
                .Returns(configurationSecond);
            userService.AddUser(user);
        }


        [TestMethod]
        public void TestDeleteUser_Successfully()
        {
            userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Returns(user);

            userService.DeleteUser(user);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestDeleteUser_NullProduct()
        {
            userService.DeleteUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestDeleteUsert_ObjectNotFoundException()
        {
            userDataServicesStub
               .Setup(x => x.GetUserById(It.IsAny<int>()))
               .Equals(null);

            userService.DeleteUser(user);
        }
        [TestMethod]
        public void TestGetListOfUsers_Successfully()
        {
            userService.GetListOfUsers();
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserById_IncorrectIdException()
        {

            userService.GetUserById(NEGATIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetUserById_Successfully()
        {
            userService.GetUserById(POSITIVE_USER_ID);
        }

        [TestMethod]
        public void TestUpdateUser_Successfully()
        {
            userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Returns(user);

            userService.UpdateUser(user);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestUpdateUser_NullProduct()
        {
            userService.UpdateUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateUser_ObjectNotFoundException()
        {
            userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Equals(null);

            userService.UpdateUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddCategory_InvalidObjectException()
        {
            categoryServices.AddCategory(invalidCategory);
        }

        [TestMethod]
        public void TestAddCategory_Successfully()
        {
            categoryServices.AddCategory(category);
        }

        [TestMethod]
        public void TestDeleteCategory_Successfully()
        {
            categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Returns(category);

            categoryServices.DeleteCategory(category);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestDeleteCategory_NullProduct()
        {
            categoryServices.DeleteCategory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "")]
        public void TestDeleteConfiguration_ObjectNotFoundException()
        {
            categoryDataServicesStub
             .Setup(x => x.GetCategoryById(It.IsAny<int>()))
             .Equals(null);

            configurationServices.DeleteConfiguration(configurationFirst);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryById_IncorrectIdException()
        {

            categoryServices.GetCategoryById(NEGATIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetCategoryById_Successfully()
        {
            categoryServices.GetCategoryById(POSITIVE_USER_ID);
        }


        [TestMethod]
        public void TestGetListOfCategories_Successfully()
        {
            categoryServices.GetListOfCategories();
        }

        [TestMethod]
        public void TestUpdateCategory_Successfully()
        {
            categoryDataServicesStub
               .Setup(x => x.GetCategoryById(It.IsAny<int>()))
               .Returns(category);

            categoryServices.UpdateCategory(category);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestUpdateCategory_NullProduct()
        {
            categoryServices.UpdateCategory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateCategory_ObjectNotFoundException()
        {
            categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Equals(null);

            categoryServices.UpdateCategory(category);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddConfiguration_InvalidObjectException()
        {
            configurationServices.AddConfiguration(invalidConfiguration);
        }

        [TestMethod]
        public void TestAddConfiguration_Successfully()
        {
            configurationServices.AddConfiguration(configurationSecond);
        }

        [TestMethod]
        public void TestDeleteConfiguration_Successfully()
        {
            configurationDataServicesStub
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Returns(configurationFirst);

            configurationServices.DeleteConfiguration(configurationFirst);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "")]
        public void TestDeleteCategory_ObjectNotFoundException()
        {
            configurationDataServicesStub 
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Equals(null);

            configurationServices.DeleteConfiguration(configurationFirst);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestDeleteConfiguration_NullProduct()
        {
            configurationServices.DeleteConfiguration(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetConfigurationById_IncorrectIdException()
        {

            configurationServices.GetConfigurationById(NEGATIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetConfigurationById_Successfully()
        {
            configurationServices.GetConfigurationById(POSITIVE_USER_ID);
        }


        [TestMethod]
        public void TestGetListOfConfiguration_Successfully()
        {
            configurationServices.GetListOfConfiguration();
        }

        [TestMethod]
        public void TestUpdateConfiguration_Successfully()
        {
            configurationDataServicesStub
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Returns(configurationFirst);

          configurationServices.UpdateConfiguration(configurationFirst);
        }


        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The object can not be null.")]
        public void TestUpdateConfiguration_NullProduct()
        {
            configurationServices.UpdateConfiguration(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateConfiguration_ObjectNotFoundException()
        {
            configurationDataServicesStub
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Equals(null);

            configurationServices.UpdateConfiguration(configurationFirst);
        }

    }
}