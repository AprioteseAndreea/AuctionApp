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
    public class UserServiceTest
    {
        private const int POSITIVEUSERID = 5;
        private const int NEGATIVEUSERID = -5;
        private Category category;

        private Product productFirst;
        private User user;
        private UserDTO userDTO;

        private User invalidUser;
        private UserDTO invaidUserDTO;
        private Money moneyFirst;
        private Configuration configurationSecond;

        private List<Product> userProducts;
        private List<UserAuction> userAuctions;

        private Mock<ILog> loggerMock;
        private Mock<IUserDataServices> userDataServicesStub;
        private Mock<IConfigurationDataServices> configurationDataServicesStub;

        private UserServicesImplementation userService;

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
            this.userDTO = new UserDTO(this.user);
            this.invalidUser = new User
            {
                Id = 1,
                FirstName = "Andreea",
                LastName = "Apriotese",
                Status = UserStatus.Active,
                Email = "andreea.apriotese",
                Score = 4.00,
                BirthDate = "12.12.2000",
            };
            this.invaidUserDTO = new UserDTO(this.invalidUser);

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

            this.configurationDataServicesStub = new Mock<IConfigurationDataServices>();
            this.userDataServicesStub = new Mock<IUserDataServices>();
            this.loggerMock = new Mock<ILog>();

            this.userService = new UserServicesImplementation(
                this.userDataServicesStub.Object,
                this.configurationDataServicesStub.Object,
                this.loggerMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddUser_InvalidObjectException()
        {
            this.configurationDataServicesStub
             .Setup(x => x.GetConfigurationById(1))
             .Returns(this.configurationSecond);
            this.userService.AddUser(this.invaidUserDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddUser_NullObject()
        {
            this.configurationDataServicesStub
             .Setup(x => x.GetConfigurationById(1))
             .Returns(this.configurationSecond);
            this.userService.AddUser(null);
        }

        [TestMethod]
        public void TestAddUser_Successfully()
        {
            this.configurationDataServicesStub
                .Setup(x => x.GetConfigurationById(1))
                .Returns(this.configurationSecond);
            this.userService.AddUser(this.userDTO);
        }

        [TestMethod]
        public void TestDeleteUser_Successfully()
        {
            this.userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Returns(this.user);

            this.userService.DeleteUser(this.userDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteUser_NullProduct()
        {
            this.userService.DeleteUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestDeleteUsert_ObjectNotFoundException()
        {
            this.userDataServicesStub
               .Setup(x => x.GetUserById(It.IsAny<int>()))
               .Equals(null);

            this.userService.DeleteUser(this.userDTO);
        }

        [TestMethod]
        public void TestGetListOfUsers_Successfully()
        {
            this.userDataServicesStub
               .Setup(x => x.GetAllUsers())
               .Returns(new List<User>());

            this.userService.GetListOfUsers();
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserById_IncorrectIdException()
        {
            this.userService.GetUserById(NEGATIVEUSERID);
        }

        [TestMethod]
        public void TestGetUserById_Successfully()
        {
            this.userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Returns(this.user);

            this.userService.GetUserById(POSITIVEUSERID);
        }

        [TestMethod]
        public void TestUpdateUser_Successfully()
        {
            this.userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Returns(this.user);

            this.userService.UpdateUser(this.userDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestUpdateUser_NullProduct()
        {
            this.userService.UpdateUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateUser_ObjectNotFoundException()
        {
            this.userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Equals(null);

            this.userService.UpdateUser(this.userDTO);
        }
    }
}
