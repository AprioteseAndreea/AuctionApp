using DataMapper;
using DomainModel.enums;
using DomainModel.Enums;
using DomainModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ServiceLayer.ServiceImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.Utils;
using DomainModel.DTO;

namespace TestsServiceLayer
{

    [TestClass]
    public class ConfigurationServiceTest
    {
        private Category category;      
        private Product productFirst;       
        private User user;
        private Money moneyFirst;
    

        private const int POSITIVE_USER_ID = 5;
        private const int NEGATIVE_USER_ID = -5;  

        private Configuration configurationFirst;
        private Configuration configurationSecond;
        private Configuration invalidConfiguration;

        private ConfigurationDTO configurationFirstDTO;
        private ConfigurationDTO configurationSecondDTO;
        private ConfigurationDTO invalidConfigurationDTO;

        private List<Product> userProducts;
        private List<UserAuction> userAuctions;

   
        private Mock<IConfigurationDataServices> configurationDataServicesStub;
        private Mock<ICategoryDataServices> categoryDataServicesStub;

        private ConfigurationServicesImplementation configurationServices;

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
            configurationFirstDTO = new ConfigurationDTO(configurationFirst);
            configurationSecondDTO = new ConfigurationDTO(configurationSecond);
            invalidConfigurationDTO = new ConfigurationDTO(invalidConfiguration);

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
          
            this.configurationDataServicesStub = new Mock<IConfigurationDataServices>();
            this.categoryDataServicesStub = new Mock<ICategoryDataServices>();
         
            configurationServices = new ConfigurationServicesImplementation(configurationDataServicesStub.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "")]
        public void TestDeleteConfiguration_InvalidObjectException()
        {
            categoryDataServicesStub
             .Setup(x => x.GetCategoryById(It.IsAny<int>()))
             .Equals(null);

            configurationServices.DeleteConfiguration(configurationFirstDTO);
        }



        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddConfiguration_InvalidObjectException()
        {
            configurationServices.AddConfiguration(invalidConfigurationDTO);
        }
        [TestMethod]
        public void TestAddConfiguration_Successfully()
        {
            configurationServices.AddConfiguration(configurationSecondDTO);
        }

        [TestMethod]
        public void TestDeleteConfiguration_Successfully()
        {
            configurationDataServicesStub
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Returns(configurationFirst);

            configurationServices.DeleteConfiguration(configurationFirstDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
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
            configurationDataServicesStub
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Returns(configurationFirst);

            configurationServices.GetConfigurationById(POSITIVE_USER_ID);
        }


        [TestMethod]
        public void TestGetListOfConfiguration_Successfully()
        {
            configurationDataServicesStub
            .Setup(x => x.GetListOfConfiguration())
            .Returns(new List<Configuration>());

            configurationServices.GetListOfConfiguration();
        }

        [TestMethod]
        public void TestUpdateConfiguration_Successfully()
        {
            configurationDataServicesStub
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Returns(configurationFirst);

            configurationServices.UpdateConfiguration(configurationFirstDTO);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
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

            configurationServices.UpdateConfiguration(configurationFirstDTO);
        }
    }
}
