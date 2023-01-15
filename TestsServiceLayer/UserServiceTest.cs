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
using log4net;

namespace TestsServiceLayer
{
    [TestClass]
    public class UserServiceTest
    {
        private Category category;

        private Product productFirst;
        
        private User user;
        private UserDTO userDTO;

        private User invalidUser;
        private UserDTO invaidUserDTO;

        private Money moneyFirst;
       
        private const int POSITIVE_USER_ID = 5;
        private const int NEGATIVE_USER_ID = -5;

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
                BirthDate = "12.12.2000"
            };
            userDTO = new UserDTO(user);
            this.invalidUser = new User
            {
                Id = 1,
                FirstName = "Andreea",
                LastName = "Apriotese",
                Status = UserStatus.Active,
                Email = "andreea.apriotese",
                Score = 4.00,
                BirthDate = "12.12.2000"
            };
            invaidUserDTO = new UserDTO(invalidUser);

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

          
            this.configurationDataServicesStub = new Mock<IConfigurationDataServices>();
            this.userDataServicesStub = new Mock<IUserDataServices>();
            this.loggerMock = new Mock<ILog>();

            userService = new UserServicesImplementation(
                userDataServicesStub.Object,
                configurationDataServicesStub.Object,
                loggerMock.Object
                );
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddUser_InvalidObjectException()
        {
            configurationDataServicesStub
             .Setup(x => x.GetConfigurationById(1))
             .Returns(configurationSecond);
            userService.AddUser(invaidUserDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddUser_NullObject()
        {
            configurationDataServicesStub
             .Setup(x => x.GetConfigurationById(1))
             .Returns(configurationSecond);
            userService.AddUser(null);
        }

        [TestMethod]
        public void TestAddUser_Successfully()
        {
            configurationDataServicesStub
                .Setup(x => x.GetConfigurationById(1))
                .Returns(configurationSecond);
            userService.AddUser(userDTO);
        }


        [TestMethod]
        public void TestDeleteUser_Successfully()
        {
            userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Returns(user);

            userService.DeleteUser(userDTO);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteUser_NullProduct()
        {
            userService.DeleteUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestDeleteUsert_ObjectNotFoundException()
        {
            userDataServicesStub
               .Setup(x => x.GetUserById(It.IsAny<int>()))
               .Equals(null);

            userService.DeleteUser(userDTO);
        }
        [TestMethod]
        public void TestGetListOfUsers_Successfully()
        {

            userDataServicesStub
               .Setup(x => x.GetAllUsers())
               .Returns(new List<User>());

            userService.GetListOfUsers();
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserById_IncorrectIdException()
        {

            userService.GetUserById(NEGATIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetUserById_Successfully()
        {
            userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Returns(user);

            userService.GetUserById(POSITIVE_USER_ID);
        }

        [TestMethod]
        public void TestUpdateUser_Successfully()
        {
            userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Returns(user);

            userService.UpdateUser(userDTO);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestUpdateUser_NullProduct()
        {
            userService.UpdateUser(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestUpdateUser_ObjectNotFoundException()
        {
            userDataServicesStub
              .Setup(x => x.GetUserById(It.IsAny<int>()))
              .Equals(null);

            userService.UpdateUser(userDTO);
        }
    }
}
