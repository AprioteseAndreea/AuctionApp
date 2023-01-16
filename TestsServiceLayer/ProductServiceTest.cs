﻿// <copyright file="ProductServiceTest.cs" company="Transilvania University of Brasov">
// Copyright (c) Apriotese Andreea. All rights reserved.
// </copyright>

namespace TestsServiceLayer
{
    using System;
    using System.Collections.Generic;
    using DataMapper;
    using DomainModel;
    using DomainModel.DTO;
    using DomainModel.Enums;
    using log4net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using ServiceLayer.ServiceImplementation;
    using ServiceLayer.Utils;

    [TestClass]
    public class ProductServiceTest
    {
        private const int POSITIVEUSERID = 5;
        private const int NEGATIVEUSERID = -5;

        private const int POSITIVEPRODUCTID = 4;
        private const int NEGATIVEPRODUCTID = -4;

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
                BirthDate = "12.12.2000",
            };

            this.moneyFirst = new Money
            {
                Amount = 100,
                Currency = Currency.RON,
            };
            this.moneySecond = new Money
            {
                Amount = 50,
                Currency = Currency.USD,
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

            this.productFirstDTO = new ProductDTO(this.productFirst);
            this.productSecondDTO = new ProductDTO(this.productSecond);
            this.invalidProductDTO = new ProductDTO(this.invalidProduct);

            this.configurationFirst = new Configuration
            {
                MaxAuctions = 1,
                InitialScore = 4,
                MinScore = 2,
                Days = 7,
            };
            this.configurationSecond = new Configuration
            {
                MaxAuctions = 5,
                InitialScore = 4,
                MinScore = 2,
                Days = 7,
            };

            this.userProducts = new List<Product>();
            this.userAuctions = new List<UserAuction>();

            this.userProducts.Add(this.productFirst);
            this.userAuctions.Add(new UserAuction
            {
                Product = this.productFirst,
                User = this.user,
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

            this.prod = new ProductServicesImplementation(
                this.productDataServicesStub.Object,
                this.configurationDataServicesStub.Object,
                this.userDataServicesStub.Object,
                this.categoryDataServicesStub.Object,
                this.loggerMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestAddProduct_NullObjectException()
        {
            this.productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(new List<Product>());
            this.productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            this.configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(this.configurationFirst);

            this.prod.AddProduct(null);
        }

        [TestMethod]
        [ExpectedException(typeof(MaxAuctionsException), "The maximum number of licitations has been reached!")]
        public void TestAddProduct_MaxAuctionsException()
        {
            this.productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(new List<Product>());
            this.productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(this.userProducts);

            this.configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(this.configurationFirst);

            this.prod.AddProduct(this.productFirstDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(SimilarDescriptionException), "")]
        public void TestAddProduct_SimilarDescriptionException()
        {
            this.productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(this.userProducts);
            this.productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            this.configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(this.configurationSecond);

            this.prod.AddProduct(this.productFirstDTO);

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddProduct_InvalidObjectException()
        {
            this.productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(this.userProducts);
            this.productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            this.configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(this.configurationSecond);

            this.prod.AddProduct(this.invalidProductDTO);
        }

        [TestMethod]
        public void TestAddProduct_SimilarDescriptionWasNotThrown()
        {
            this.productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(this.userProducts);
            this.productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            this.configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(this.configurationSecond);

            this.prod.AddProduct(this.productSecondDTO);
        }

        [TestMethod]
        public void TestAddProduct_Successfully()
        {
            this.productDataServicesStub
                .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
                .Returns(new List<Product>());
            this.productDataServicesStub
               .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
               .Returns(new List<Product>());

            this.configurationDataServicesStub
                .Setup(c => c.GetConfigurationById(It.IsAny<int>()))
                .Returns(this.configurationSecond);

            this.prod.AddProduct(this.productSecondDTO);
        }

        [TestMethod]
        public void TestDeleteProduct_Successfully()
        {
            this.productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(this.productFirst);

            this.prod.DeleteProduct(this.productSecondDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteProduct_NullProduct()
        {
            this.prod.DeleteProduct(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestDeleteProduct_ObjectNotFoundException()
        {
            this.productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Equals(null);

            this.prod.DeleteProduct(this.productSecondDTO);
        }

        [TestMethod]
        public void TestGetListOfProducts_Successfully()
        {
            this.productDataServicesStub
            .Setup(x => x.GetAllProducts())
            .Returns(new List<Product>());
            this.prod.GetListOfProducts();
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetOpenProductsByUserId_IncorrectIdException()
        {
            this.prod.GetOpenProductsByUserId(NEGATIVEUSERID);
        }

        [TestMethod]
        public void TestGetOpenProductsByUserId_Successfully()
        {
            this.userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Returns(this.user);

            this.productDataServicesStub
              .Setup(x => x.GetOpenProductsByUserId(It.IsAny<int>()))
              .Returns(new List<Product>());

            this.prod.GetOpenProductsByUserId(POSITIVEUSERID);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestGetOpenProductsByUserId_ObjectNotFoundException()
        {
            this.userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Equals(null);
            this.prod.GetOpenProductsByUserId(POSITIVEUSERID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetProductsByUserId_IncorrectIdException()
        {
            this.prod.GetProductsByUserId(NEGATIVEUSERID);
        }

        [TestMethod]
        public void TestGetProductsByUserId_Successfully()
        {
            this.userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Returns(this.user);

            this.productDataServicesStub
           .Setup(x => x.GetProductsByUserId(It.IsAny<int>()))
           .Returns(new List<Product>());

            this.prod.GetProductsByUserId(POSITIVEUSERID);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestGetProductsByUserId_ObjectNotFoundException()
        {
            this.userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Equals(null);
            this.prod.GetProductsByUserId(POSITIVEUSERID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetProductById_IncorrectIdException()
        {
            this.prod.GetProductById(NEGATIVEPRODUCTID);
        }

        [TestMethod]
        public void TestGetProductById_Successfully()
        {
            this.productDataServicesStub
           .Setup(x => x.GetProductById(It.IsAny<int>()))
           .Returns(this.productFirst);

            this.prod.GetProductById(POSITIVEPRODUCTID);
        }

        [TestMethod]
        public void TestUpdateProduct_Successfully()
        {
            this.productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(this.productFirst);

            this.prod.UpdateProduct(this.productSecondDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestUpdateProduct_NullProduct()
        {
            this.prod.UpdateProduct(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateProduct_ObjectNotFoundException()
        {
            this.productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Equals(null);

            this.prod.UpdateProduct(this.productSecondDTO);
        }
    }
}
