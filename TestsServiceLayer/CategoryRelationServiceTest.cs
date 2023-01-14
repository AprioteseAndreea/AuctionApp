using DataMapper;
using DomainModel.enums;
using DomainModel.Enums;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.ServiceImplementation;
using System;
using System.Collections.Generic;
using ServiceLayer.Utils;
using DomainModel.DTO;

namespace TestsServiceLayer
{
    [TestClass]
    public class CategoryRelationServiceTest
    {

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
        private Mock<ICategoryRelationDataServices> categoryRelationDataServicesStub;

        private ProductServicesImplementation prod;
        private UserAuctionServicesImplementation userAuction;
        private UserServicesImplementation userService;
        private CategoryServicesImplementation categoryServices;
        private CategoryRelationServicesImplementation categoryRelationServices;
        private ConfigurationServicesImplementation configurationServices;

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
                ParentCategory = parentCategory,
                ChildCategory = childCategory,
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
                }
            };
            categoryRelationDTO = new CategoryRelationDTO(categoryRelation);
            invalidCategoryRelationDTO = new CategoryRelationDTO(invalidCategoryRelation);

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
            this.moneyThird = new Money
            {
                Amount = 150,
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
                    Currency = Currency.RON,
                },
            });

            this.productDataServicesStub = new Mock<IProductDataServices>();
            this.userAuctionDataServiceStub = new Mock<IUserAuctionDataServices>();
            this.configurationDataServicesStub = new Mock<IConfigurationDataServices>();
            this.userDataServicesStub = new Mock<IUserDataServices>();
            this.categoryDataServicesStub = new Mock<ICategoryDataServices>();
            this.categoryRelationDataServicesStub = new Mock<ICategoryRelationDataServices>();

            prod = new ProductServicesImplementation(productDataServicesStub.Object, configurationDataServicesStub.Object, userDataServicesStub.Object, categoryDataServicesStub.Object);
            userAuction = new UserAuctionServicesImplementation(userAuctionDataServiceStub.Object, productDataServicesStub.Object, userDataServicesStub.Object);
            userService = new UserServicesImplementation(userDataServicesStub.Object, configurationDataServicesStub.Object);
            categoryServices = new CategoryServicesImplementation(categoryDataServicesStub.Object);
            configurationServices = new ConfigurationServicesImplementation(configurationDataServicesStub.Object);
            categoryRelationServices = new CategoryRelationServicesImplementation(categoryRelationDataServicesStub.Object, categoryDataServicesStub.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddCategoryRelation_InvalidObjectException()
        {
            categoryRelationServices.AddCategoryRelation(invalidCategoryRelationDTO);
        }

        [TestMethod]
        public void TestAddCategoryRelation_Successfully()
        {

            categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Returns(childCategory);

            categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Returns(parentCategory);

            categoryRelationServices.AddCategoryRelation(categoryRelationDTO);
        }
        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "")]
        public void TestAddCategoryRelation_CategoriesNotFound()
        {

            categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Equals(null);

            categoryDataServicesStub
              .Setup(x => x.GetCategoryById(It.IsAny<int>()))
              .Equals(null);

            categoryRelationServices.AddCategoryRelation(categoryRelationDTO);
        }

        [TestMethod]
        public void TestDeleteCategoryRelation_Successfully()
        {
            categoryRelationDataServicesStub
              .Setup(x => x.GetCategoryRelationByChildAndParentId(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(categoryRelation);

            categoryRelationServices.DeleteCategoryRelation(categoryRelationDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "")]
        public void TestDeleteCategoryRelation_CategoryNotFound()
        {
            categoryRelationDataServicesStub
              .Setup(x => x.GetCategoryRelationByChildAndParentId(It.IsAny<int>(), It.IsAny<int>()))
              .Equals(null);

            categoryRelationServices.DeleteCategoryRelation(categoryRelationDTO);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteCategoryRelation_NullProduct()
        {
            categoryRelationServices.DeleteCategoryRelation(null);
        }

        [TestMethod]
        public void TestGetCategoryRelationByParentId_Successfully()
        {
            categoryRelationDataServicesStub
            .Setup(x => x.GetCategoryRelationByParentId(It.IsAny<int>()))
            .Returns(new List<CategoryRelation>());
            categoryRelationServices.GetCategoryRelationByParentId(parentCategory.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryRelationByParentId_NegativeId()
        {
            categoryRelationServices.GetCategoryRelationByParentId(NEGATIVE_PRODUCT_ID);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryRelationByParentId_ZeroId()
        {
            categoryRelationServices.GetCategoryRelationByParentId(0);
        }

        [TestMethod]
        public void TestGetCategoryRelationByChildId_Successfully()
        {
            categoryRelationDataServicesStub
           .Setup(x => x.GetCategoryRelationByChildId(It.IsAny<int>()))
         .Returns(new List<CategoryRelation>());
            categoryRelationServices.GetCategoryRelationByChildId(childCategory.Id);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryRelationByChildId_NegativeId()
        {
            categoryRelationServices.GetCategoryRelationByChildId(NEGATIVE_PRODUCT_ID);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryRelationByChildId_ZeroId()
        {
            categoryRelationServices.GetCategoryRelationByChildId(0);
        }
        [TestMethod]
        public void TestGetListOfCategoriesRelation_Successfully()
        {
            categoryRelationDataServicesStub
             .Setup(x => x.GetListOfCategoriesRelation())
           .Returns(new List<CategoryRelation>());

            categoryRelationServices.GetListOfCategoriesRelation();
        }


        [TestMethod]
        public void TestUpdateCategoryRelation_Successfully()
        {
            categoryRelationDataServicesStub
              .Setup(x => x.GetCategoryRelationByChildAndParentId(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(categoryRelation);

            categoryRelationServices.UpdateCategoryRelation(categoryRelationDTO);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestUpdateCategoryRelation_NullProduct()
        {
            categoryRelationServices.UpdateCategoryRelation(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateCategoryRelation_ObjectNotFoundException()
        {
            categoryRelationDataServicesStub
              .Setup(x => x.GetCategoryRelationByChildAndParentId(It.IsAny<int>(), It.IsAny<int>()))
            .Equals(null);

            categoryRelationServices.UpdateCategoryRelation(categoryRelationDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetCategoryRelationById_IncorrectIdException()
        {

            categoryRelationServices.GetCategoryRelationByChildAndParentId(NEGATIVE_USER_ID, NEGATIVE_PRODUCT_ID);
        }

        [TestMethod]
        public void TestGetCategoryRelationById_Successfully()
        {
            categoryRelationDataServicesStub
             .Setup(x => x.GetCategoryRelationByChildAndParentId(It.IsAny<int>(), It.IsAny<int>()))
           .Returns(categoryRelation);
            categoryRelationServices.GetCategoryRelationByChildAndParentId(POSITIVE_USER_ID, POSITIVE_PRODUCT_ID);
        }
    }
}
