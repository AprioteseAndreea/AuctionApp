// <copyright file="UserServiceTest.cs" company="Transilvania University of Brasov">
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
    public class UserServiceTest
    {
        private const int PositiveUserId = 5;
        private const int NegativeUserId = -5;

        private User user;
        private User invalidUser;
        private Category category;
        private Money moneyFirst;
        private UserDTO userDTO;
        private UserDTO invaidUserDTO;
        private Product productFirst;
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
                Name = "Genti de lux",
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

        /// <summary>
        /// Tests the add user invalid object exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddUser_InvalidObjectException()
        {
            this.configurationDataServicesStub
             .Setup(x => x.GetConfigurationById(1))
             .Returns(this.configurationSecond);
            this.userService.AddUser(this.invaidUserDTO);
        }

        /// <summary>
        /// Tests the add user null object.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddUser_NullObject()
        {
            this.configurationDataServicesStub
             .Setup(x => x.GetConfigurationById(1))
             .Returns(this.configurationSecond);
            this.userService.AddUser(null);
        }

        /// <summary>
        /// Tests the add user successfully.
        /// </summary>
        [TestMethod]
        public void TestAddUser_Successfully()
        {
            this.configurationDataServicesStub
                .Setup(x => x.GetConfigurationById(1))
                .Returns(this.configurationSecond);
            this.userService.AddUser(this.userDTO);
        }

        /// <summary>
        /// Tests the delete user successfully.
        /// </summary>
        [TestMethod]
        public void TestDeleteUser_Successfully()
        {
            this.userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Returns(this.user);

            this.userService.DeleteUser(this.userDTO);
        }

        /// <summary>
        /// Tests the delete user null product.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteUser_NullProduct()
        {
            this.userService.DeleteUser(null);
        }

        /// <summary>
        /// Tests the delete usert object not found exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestDeleteUsert_ObjectNotFoundException()
        {
            this.userDataServicesStub
               .Setup(x => x.GetUserById(It.IsAny<int>()))
               .Equals(null);

            this.userService.DeleteUser(this.userDTO);
        }

        /// <summary>
        /// Tests the get list of users successfully.
        /// </summary>
        [TestMethod]
        public void TestGetListOfUsers_Successfully()
        {
            this.userDataServicesStub
               .Setup(x => x.GetAllUsers())
               .Returns(new List<User>());

            this.userService.GetListOfUsers();
        }

        /// <summary>
        /// Tests the get user by identifier incorrect identifier exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserById_IncorrectIdException()
        {
            this.userService.GetUserById(NegativeUserId);
        }

        /// <summary>
        /// Tests the get user by identifier successfully.
        /// </summary>
        [TestMethod]
        public void TestGetUserById_Successfully()
        {
            this.userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Returns(this.user);

            this.userService.GetUserById(PositiveUserId);
        }

        /// <summary>
        /// Tests the update user successfully.
        /// </summary>
        [TestMethod]
        public void TestUpdateUser_Successfully()
        {
            this.userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Returns(this.user);

            this.userService.UpdateUser(this.userDTO);
        }

        /// <summary>
        /// Tests the update user null product.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestUpdateUser_NullProduct()
        {
            this.userService.UpdateUser(null);
        }

        /// <summary>
        /// Tests the update user object not found exception.
        /// </summary>
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
