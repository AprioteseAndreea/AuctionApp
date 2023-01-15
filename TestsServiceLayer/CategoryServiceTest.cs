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
    public class CategoryServiceTest
    {
        private Category category;

        private CategoryDTO categoryDTO;
        private CategoryDTO invalidCategoryDTO;

        private Product productFirst;     
        private User user;      
        private Money moneyFirst;
      
        private const int POSITIVE_USER_ID = 5;
        private const int NEGATIVE_USER_ID = -5;

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
            categoryDTO = new CategoryDTO(category);
            
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
         
            this.categoryDataServicesStub = new Mock<ICategoryDataServices>();
            this.loggerMock = new Mock<ILog>();

            categoryServices = new CategoryServicesImplementation(
                categoryDataServicesStub.Object,
                loggerMock.Object);           
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddCategory_InvalidObjectException()
        {
            categoryServices.AddCategory(invalidCategoryDTO);
        }

        [TestMethod]
        public void TestAddCategory_Successfully()
        {
            categoryServices.AddCategory(categoryDTO);
        }

        [TestMethod]
        public void TestDeleteCategory_Successfully()
        {
            categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Returns(category);

            categoryServices.DeleteCategory(categoryDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The object can not be null.")]
        public void TestDeleteCategory_ObjectNotFoundException()
        {
            categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Equals(null);
            categoryServices.DeleteCategory(categoryDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteCategory_NullProduct()
        {
            categoryServices.DeleteCategory(null);
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
            categoryDataServicesStub
            .Setup(x => x.GetCategoryById(It.IsAny<int>()))
            .Returns(category);
            categoryServices.GetCategoryById(POSITIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetListOfCategories_Successfully()
        {
            categoryDataServicesStub
             .Setup(x => x.GetListOfCategories())
             .Returns(new List<Category>());

            categoryServices.GetListOfCategories();
        }

        [TestMethod]
        public void TestUpdateCategory_Successfully()
        {
            categoryDataServicesStub
               .Setup(x => x.GetCategoryById(It.IsAny<int>()))
               .Returns(category);

            categoryServices.UpdateCategory(categoryDTO);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
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

            categoryServices.UpdateCategory(categoryDTO);
        }
    }
}