// <copyright file="CategoryRelationServiceTest.cs" company="Transilvania University of Brasov">
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
    public class CategoryRelationServiceTest
    {
        private const int POSITIVEUSERID = 5;
        private const int NEGATIVEUSERID = -5;

        private const int POSITIVEPRODUCTID = 4;
        private const int NEGATIVEPRODUCTID = -4;

        private Category category;
        private Category childCategory;
        private Category parentCategory;

        private CategoryRelation categoryRelation;
        private CategoryRelation invalidCategoryRelation;

        private CategoryRelationDTO categoryRelationDTO;
        private CategoryRelationDTO invalidCategoryRelationDTO;

        private Product productFirst;

        private User user;
        private Money moneyFirst;

        private List<Product> userProducts;
        private List<UserAuction> userAuctions;

        private Mock<ICategoryDataServices> categoryDataServicesStub;
        private Mock<ICategoryRelationDataServices> categoryRelationDataServicesStub;
        private Mock<ILog> loggerMock;

        private CategoryRelationServicesImplementation categoryRelationServices;

        [TestInitialize]
        public void SetUp()
        {
            this.category = new Category
            {
                Id = 4,
                Name = "Produse alimentare pentru oameni",
            };
            this.childCategory = new Category
            {
                Id = 5,
                Name = "Produse alimentare pentru caini",
            };
            this.parentCategory = new Category
            {
                Id = 6,
                Name = "Produse alimentare pentru oameni",
            };
            this.categoryRelation = new CategoryRelation
            {
                ParentCategory = this.parentCategory,
                ChildCategory = this.childCategory,
            };
            this.invalidCategoryRelation = new CategoryRelation
            {
                ParentCategory = new Category
                {
                    Id = 0,
                    Name = "Produse alimentare pentru oameni",
                },
                ChildCategory = new Category
                {
                    Id = 0,
                    Name = "Produse alimentare pentru oameni",
                },
            };
            this.categoryRelationDTO = new CategoryRelationDTO(this.categoryRelation);
            this.invalidCategoryRelationDTO = new CategoryRelationDTO(this.invalidCategoryRelation);

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
            this.categoryRelationDataServicesStub = new Mock<ICategoryRelationDataServices>();
            this.loggerMock = new Mock<ILog>();

            this.categoryRelationServices = new CategoryRelationServicesImplementation(
                this.categoryRelationDataServicesStub.Object,
                this.categoryDataServicesStub.Object,
                this.loggerMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddCategoryRelation_InvalidObjectException()
        {
            this.categoryRelationServices.AddCategoryRelation(this.invalidCategoryRelationDTO);
        }

        [TestMethod]
        public void TestAddCategoryRelation_Successfully()
        {
            this.categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Returns(this.childCategory);

            this.categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Returns(this.parentCategory);

            this.categoryRelationServices.AddCategoryRelation(this.categoryRelationDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "")]
        public void TestAddCategoryRelation_CategoriesNotFound()
        {
            this.categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Equals(null);

            this.categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Equals(null);

            this.categoryRelationServices.AddCategoryRelation(this.categoryRelationDTO);
        }

        [TestMethod]
        public void TestDeleteCategoryRelation_Successfully()
        {
            this.categoryRelationDataServicesStub
              .Setup(x => x.GetCategoryRelationByChildAndParentId(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(this.categoryRelation);

            this.categoryRelationServices.DeleteCategoryRelation(this.categoryRelationDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "")]
        public void TestDeleteCategoryRelation_CategoryNotFound()
        {
            this.categoryRelationDataServicesStub
              .Setup(x => x.GetCategoryRelationByChildAndParentId(It.IsAny<int>(), It.IsAny<int>()))
              .Equals(null);

            this.categoryRelationServices.DeleteCategoryRelation(this.categoryRelationDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteCategoryRelation_NullProduct()
        {
            this.categoryRelationServices.DeleteCategoryRelation(null);
        }

        [TestMethod]
        public void TestGetCategoryRelationByParentId_Successfully()
        {
            this.categoryRelationDataServicesStub
            .Setup(x => x.GetCategoryRelationByParentId(It.IsAny<int>()))
            .Returns(new List<CategoryRelation>());
            this.categoryRelationServices.GetCategoryRelationByParentId(this.parentCategory.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryRelationByParentId_NegativeId()
        {
            this.categoryRelationServices.GetCategoryRelationByParentId(NEGATIVEPRODUCTID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryRelationByParentId_ZeroId()
        {
            this.categoryRelationServices.GetCategoryRelationByParentId(0);
        }

        [TestMethod]
        public void TestGetCategoryRelationByChildId_Successfully()
        {
            this.categoryRelationDataServicesStub
           .Setup(x => x.GetCategoryRelationByChildId(It.IsAny<int>()))
         .Returns(new List<CategoryRelation>());
            this.categoryRelationServices.GetCategoryRelationByChildId(this.childCategory.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryRelationByChildId_NegativeId()
        {
            this.categoryRelationServices.GetCategoryRelationByChildId(NEGATIVEPRODUCTID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryRelationByChildId_ZeroId()
        {
            this.categoryRelationServices.GetCategoryRelationByChildId(0);
        }

        [TestMethod]
        public void TestGetListOfCategoriesRelation_Successfully()
        {
            this.categoryRelationDataServicesStub
             .Setup(x => x.GetListOfCategoriesRelation())
           .Returns(new List<CategoryRelation>());

            this.categoryRelationServices.GetListOfCategoriesRelation();
        }

        [TestMethod]
        public void TestUpdateCategoryRelation_Successfully()
        {
            this.categoryRelationDataServicesStub
              .Setup(x => x.GetCategoryRelationByChildAndParentId(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(this.categoryRelation);

            this.categoryRelationServices.UpdateCategoryRelation(this.categoryRelationDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestUpdateCategoryRelation_NullProduct()
        {
            this.categoryRelationServices.UpdateCategoryRelation(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateCategoryRelation_ObjectNotFoundException()
        {
            this.categoryRelationDataServicesStub
              .Setup(x => x.GetCategoryRelationByChildAndParentId(It.IsAny<int>(), It.IsAny<int>()))
            .Equals(null);

            this.categoryRelationServices.UpdateCategoryRelation(this.categoryRelationDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryRelationById_IncorrectIdException()
        {
            this.categoryRelationServices.GetCategoryRelationByChildAndParentId(NEGATIVEUSERID, NEGATIVEPRODUCTID);
        }

        [TestMethod]
        public void TestGetCategoryRelationById_Successfully()
        {
            this.categoryRelationDataServicesStub
             .Setup(x => x.GetCategoryRelationByChildAndParentId(It.IsAny<int>(), It.IsAny<int>()))
           .Returns(this.categoryRelation);
            this.categoryRelationServices.GetCategoryRelationByChildAndParentId(POSITIVEUSERID, POSITIVEPRODUCTID);
        }
    }
}