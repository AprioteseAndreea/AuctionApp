// <copyright file="ConfigurationServiceTest.cs" company="Transilvania University of Brasov">
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
    public class ConfigurationServiceTest
    {
        private const int POSITIVEUSERID = 5;
        private const int NEGATIVEUSERID = -5;

        private Category category;
        private Product productFirst;
        private User user;
        private Money moneyFirst;

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
        private Mock<ILog> loggerMock;

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
            this.invalidConfiguration = new Configuration
            {
                MaxAuctions = 5,
                InitialScore = 8,
                MinScore = 2,
                Days = 7,
            };
            this.configurationFirstDTO = new ConfigurationDTO(this.configurationFirst);
            this.configurationSecondDTO = new ConfigurationDTO(this.configurationSecond);
            this.invalidConfigurationDTO = new ConfigurationDTO(this.invalidConfiguration);

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

            this.configurationDataServicesStub = new Mock<IConfigurationDataServices>();
            this.categoryDataServicesStub = new Mock<ICategoryDataServices>();
            this.loggerMock = new Mock<ILog>();

            this.configurationServices = new ConfigurationServicesImplementation(
                this.configurationDataServicesStub.Object,
                this.loggerMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "")]
        public void TestDeleteConfiguration_InvalidObjectException()
        {
            this.categoryDataServicesStub
             .Setup(x => x.GetCategoryById(It.IsAny<int>()))
             .Equals(null);

            this.configurationServices.DeleteConfiguration(this.configurationFirstDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddConfiguration_InvalidObjectException()
        {
            this.configurationServices.AddConfiguration(this.invalidConfigurationDTO);
        }

        [TestMethod]
        public void TestAddConfiguration_Successfully()
        {
            this.configurationServices.AddConfiguration(this.configurationSecondDTO);
        }

        [TestMethod]
        public void TestDeleteConfiguration_Successfully()
        {
            this.configurationDataServicesStub
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Returns(this.configurationFirst);

            this.configurationServices.DeleteConfiguration(this.configurationFirstDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteConfiguration_NullProduct()
        {
            this.configurationServices.DeleteConfiguration(null);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetConfigurationById_IncorrectIdException()
        {
            this.configurationServices.GetConfigurationById(NEGATIVEUSERID);
        }

        [TestMethod]
        public void TestGetConfigurationById_Successfully()
        {
            this.configurationDataServicesStub
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Returns(this.configurationFirst);

            this.configurationServices.GetConfigurationById(POSITIVEUSERID);
        }

        [TestMethod]
        public void TestGetListOfConfiguration_Successfully()
        {
            this.configurationDataServicesStub
            .Setup(x => x.GetListOfConfiguration())
            .Returns(new List<Configuration>());

            this.configurationServices.GetListOfConfiguration();
        }

        [TestMethod]
        public void TestUpdateConfiguration_Successfully()
        {
            this.configurationDataServicesStub
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Returns(this.configurationFirst);

            this.configurationServices.UpdateConfiguration(this.configurationFirstDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestUpdateConfiguration_NullProduct()
        {
            this.configurationServices.UpdateConfiguration(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateConfiguration_ObjectNotFoundException()
        {
            this.configurationDataServicesStub
              .Setup(x => x.GetConfigurationById(It.IsAny<int>()))
              .Equals(null);

            this.configurationServices.UpdateConfiguration(this.configurationFirstDTO);
        }
    }
}
