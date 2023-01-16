// <copyright file="CategoryServiceTest.cs" company="Transilvania University of Brasov">
// Copyright (c) Andreea Apriotese. All rights reserved.
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
    public class CategoryServiceTest
    {
        private const int POSITIVEUSERID = 5;
        private const int NEGATIVEUSERID = -5;

        private Category category;
        private CategoryDTO categoryDTO;
        private CategoryDTO invalidCategoryDTO;

        private Product productFirst;
        private User user;
        private Money moneyFirst;

        private List<Product> userProducts;
        private List<UserAuction> userAuctions;

        private Mock<ICategoryDataServices> categoryDataServicesStub;
        private Mock<ILog> loggerMock;

        private CategoryServicesImplementation categoryServices;

        [TestInitialize]
        public void SetUp()
        {
            this.category = new Category
            {
                Id = 4,
                Name = "Produse alimentare pentru oameni",
            };
            this.categoryDTO = new CategoryDTO(this.category);
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
            this.userProducts = new List<Product>();
            this.userAuctions = new List<UserAuction>();
            this.invalidCategoryDTO = null;

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
            this.categoryDataServicesStub = new Mock<ICategoryDataServices>();
            this.loggerMock = new Mock<ILog>();

            this.categoryServices = new CategoryServicesImplementation(
                this.categoryDataServicesStub.Object,
                this.loggerMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddCategory_InvalidObjectException()
        {
            this.categoryServices.AddCategory(this.invalidCategoryDTO);
        }

        [TestMethod]
        public void TestAddCategory_Successfully()
        {
            this.categoryServices.AddCategory(this.categoryDTO);
        }

        [TestMethod]
        public void TestDeleteCategory_Successfully()
        {
            this.categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Returns(this.category);

            this.categoryServices.DeleteCategory(this.categoryDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The object can not be null.")]
        public void TestDeleteCategory_ObjectNotFoundException()
        {
            this.categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Equals(null);
            this.categoryServices.DeleteCategory(this.categoryDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteCategory_NullProduct()
        {
            this.categoryServices.DeleteCategory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryById_IncorrectIdException()
        {
            this.categoryServices.GetCategoryById(NEGATIVEUSERID);
        }

        [TestMethod]
        public void TestGetCategoryById_Successfully()
        {
            this.categoryDataServicesStub
            .Setup(x => x.GetCategoryById(It.IsAny<int>()))
            .Returns(this.category);
            this.categoryServices.GetCategoryById(POSITIVEUSERID);
        }

        [TestMethod]
        public void TestGetListOfCategories_Successfully()
        {
            this.categoryDataServicesStub
             .Setup(x => x.GetListOfCategories())
             .Returns(new List<Category>());

            this.categoryServices.GetListOfCategories();
        }

        [TestMethod]
        public void TestUpdateCategory_Successfully()
        {
            this.categoryDataServicesStub
               .Setup(x => x.GetCategoryById(It.IsAny<int>()))
               .Returns(this.category);

            this.categoryServices.UpdateCategory(this.categoryDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestUpdateCategory_NullProduct()
        {
            this.categoryServices.UpdateCategory(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateCategory_ObjectNotFoundException()
        {
            this.categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Equals(null);

            this.categoryServices.UpdateCategory(this.categoryDTO);
        }
    }
}