using DataMapper;
using DomainModel;
using DomainModel.DTO;
using DomainModel.enums;
using DomainModel.Enums;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.ServiceImplementation;
using ServiceLayer.Utils;
using System;
using System.Collections.Generic;

namespace TestsServiceLayer
{
    [TestClass]
    public class ProductServiceTest
    {
        private Category category;

        private Product productFirst;
        private Product productSecond;
        private Product invalidProduct;

        private ProductDTO productFirstDTO;
        private ProductDTO productSecondDTO;
        private ProductDTO invalidProductDTO;

        private User user;

        private Money moneyFirst;
        private Money moneySecond;

        private const int POSITIVE_USER_ID = 5;
        private const int NEGATIVE_USER_ID = -5;

        private const int POSITIVE_PRODUCT_ID = 4;
        private const int NEGATIVE_PRODUCT_ID = -4;

        private Configuration configurationFirst;
        private Configuration configurationSecond;

        private List<Product> userProducts;
        private List<UserAuction> userAuctions;

        private Mock<ILog> loggerMock;
        private Mock<IProductDataServices> productDataServicesStub;
        private Mock<IConfigurationDataServices> configurationDataServicesStub;
        private Mock<IUserDataServices> userDataServicesStub;
        private Mock<ICategoryDataServices> categoryDataServicesStub;

        private ProductServicesImplementation prod;

        [TestInitialize]
        public void SetUp()
        {
            this.category = new Category
            {
                Id = 4,
                Name = "Produse alimentare pentru oameni",
            };

            this.user = new User
            {
                Id = 1,
                FirstName = "Andreea",
                LastName = "Apriotese",
                Status = UserStatus.Active,
                Email = "andreea.apriotese@gmail.com",
                Score = 4.00,
                BirthDate = "12.12.2000"
            };

            this.moneyFirst = new Money
            {
                Amount = 100,
                Currency = Currency.RON
            };
            this.moneySecond = new Money
            {
                Amount = 50,
                Currency = Currency.USD
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
                Status = AuctionStatus.Open,
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
                Status = AuctionStatus.Open,
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
                Status = AuctionStatus.Closed,
            };

            productFirstDTO = new ProductDTO(productFirst);
            productSecondDTO = new ProductDTO(productSecond);
            invalidProductDTO = new ProductDTO(invalidProduct);

            this.configurationFirst = new Configuration
            {
                MaxAuctions = 1,
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
                    Currency = Currency.RON,
                },
            });

            this.productDataServicesStub = new Mock<IProductDataServices>();
            this.configurationDataServicesStub = new Mock<IConfigurationDataServices>();
            this.userDataServicesStub = new Mock<IUserDataServices>();
            this.categoryDataServicesStub = new Mock<ICategoryDataServices>();
            this.loggerMock = new Mock<ILog>();

            prod = new ProductServicesImplementation(
                productDataServicesStub.Object,
                configurationDataServicesStub.Object,
                userDataServicesStub.Object,
                categoryDataServicesStub.Object,
                loggerMock.Object
                );

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
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
               .Returns(userProducts);

            configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(configurationFirst);

            prod.AddProduct(productFirstDTO);

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

            prod.AddProduct(productFirstDTO);

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

            prod.AddProduct(invalidProductDTO);

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

            prod.AddProduct(productSecondDTO);

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

            prod.AddProduct(productSecondDTO);

        }

        [TestMethod]
        public void TestDeleteProduct_Successfully()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(productFirst);

            prod.DeleteProduct(productSecondDTO);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
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

            prod.DeleteProduct(productSecondDTO);
        }

        [TestMethod]
        public void TestGetListOfProducts_Successfully()
        {
            productDataServicesStub
            .Setup(x => x.GetAllProducts())
            .Returns(new List<Product>());
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

            productDataServicesStub
              .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
              .Returns(new List<Product>());

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

            productDataServicesStub
           .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
           .Returns(new List<Product>());

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
            productDataServicesStub
           .Setup(x => x.GetProductById(It.IsAny<int>()))
           .Returns(productFirst);

            prod.GetProductById(POSITIVE_PRODUCT_ID);
        }

        [TestMethod]
        public void TestUpdateProduct_Successfully()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(productFirst);

            prod.UpdateProduct(productSecondDTO);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
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

            prod.UpdateProduct(productSecondDTO);
        }

    }
}
