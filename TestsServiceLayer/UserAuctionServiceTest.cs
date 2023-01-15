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
using log4net;

namespace TestsServiceLayer
{
    [TestClass]
    public class UserAuctionServiceTest
    {
        private Category category;

        private Product productFirst;
        private Product closedProduct;

        private User user;

        private UserAuction userAuctionFirst;
        private UserAuction userAuctionSecond;
        private UserAuction userAuctionThird;
        private UserAuction userAuctionFourth;
        private UserAuction invalidUserAuction;

        private UserAuctionDTO userAuctionFirstDTO;
        private UserAuctionDTO userAuctionSecondDTO;
        private UserAuctionDTO userAuctionThirdDTO;
        private UserAuctionDTO userAuctionFourthDTO;
        private UserAuctionDTO invalidUserAuctionDTO;


        private Money moneyFirst;
        private Money moneySecond;
        private Money moneyThird;

        private const int POSITIVE_USER_ID = 5;
        private const int NEGATIVE_USER_ID = -5;

        private const int POSITIVE_PRODUCT_ID = 4;
        private const int NEGATIVE_PRODUCT_ID = -4;

        private List<Product> userProducts;
        private List<UserAuction> userAuctions;

        private Mock<IProductDataServices> productDataServicesStub;
        private Mock<IUserAuctionDataServices> userAuctionDataServiceStub;
        private Mock<IUserDataServices> userDataServicesStub;
        private Mock<ILog> loggerMock;

        private UserAuctionServicesImplementation userAuction;


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
                Id = 27,
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
                Id = 20,
                Name = "primul produs",
                Description = "o descriere foarte interesanta",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2023, 12, 31),
                StartingPrice = this.moneyFirst,
                Category = this.category,
                Status = AuctionStatus.Open,
            };
            this.closedProduct = new Product
            {
                Id = 20,
                Name = "primul produs",
                Description = "o descriere foarte interesanta",
                OwnerUser = this.user,
                StartDate = DateTime.Now,
                EndDate = new DateTime(2023, 12, 31),
                StartingPrice = this.moneyFirst,
                Category = this.category,
                Status = AuctionStatus.Closed,
            };
           
            this.userAuctionFirst = new UserAuction
            {
                Id=1,
                Product = productFirst,
                User = user,
                Price = this.moneySecond,

            };
            this.userAuctionSecond = new UserAuction
            {
                Id=2,
                Product = productFirst,
                User = user,
                Price = moneyThird,

            };
            this.userAuctionThird = new UserAuction
            {
                Id = 3,
                Product = productFirst,
                User = user,
                Price = moneyFirst,

            };
            this.userAuctionFourth = new UserAuction
            {
                Id = 4,
                Product = productFirst,
                User = user,
                Price = new Money
                {
                    Amount = 400,
                    Currency = Currency.RON,
                },

            };
            this.invalidUserAuction = new UserAuction
            {
                Id=0,
                Product = productFirst,
                User = null,
                Price = moneyThird,

            };

            userAuctionFirstDTO = new UserAuctionDTO(userAuctionFirst);
            userAuctionSecondDTO = new UserAuctionDTO(userAuctionSecond);
            userAuctionThirdDTO = new UserAuctionDTO(userAuctionThird);
            userAuctionFourthDTO = new UserAuctionDTO(userAuctionFourth);
            invalidUserAuctionDTO = new UserAuctionDTO(invalidUserAuction);

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
            this.userDataServicesStub = new Mock<IUserDataServices>();
            this.loggerMock = new Mock<ILog>();
            userAuction = new UserAuctionServicesImplementation(
                userAuctionDataServiceStub.Object,
                productDataServicesStub.Object,
                userDataServicesStub.Object,
                loggerMock.Object
                );

        }

        [TestMethod]
        [ExpectedException(typeof(IncompatibleCurrencyException), "")]
        public void TestAddUserAuction_IncompatibleCurrencyException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());
            userAuction.AddUserAuction(userAuctionFirstDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(MinimumBidException), "")]
        public void TestAddUserAuction_MinimumBidException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            userAuction.AddUserAuction(userAuctionThirdDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(ClosedAuctionException), "")]
        public void TestAddUserAuction_ClosedAuctionException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(closedProduct);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            userAuction.AddUserAuction(userAuctionThirdDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(OverbiddingException), "")]
        public void TestAddUserAuction_OverbiddingException()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(userAuctions);

            userAuction.AddUserAuction(userAuctionThirdDTO);
        }
        [TestMethod]
        [ExpectedException(typeof(OverbiddingException), "")]
        public void TestAddUserAuction_OverbiddingException_AmountIsOver300Percent()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(userAuctions);

            userAuction.AddUserAuction(userAuctionFourthDTO);
        }
        [TestMethod]
        public void TestAddUserAuction_Successfully()
        {
            productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            userAuction.AddUserAuction(userAuctionSecondDTO);
        }

        [TestMethod]
        public void TestDeleteUserAuction_Successfully()
        {
            userAuctionDataServiceStub
              .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
              .Returns(userAuctionFirst);

            userAuction.DeleteUserAuction(userAuctionFirstDTO);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteUserAuction_NullProduct()
        {
            userAuction.DeleteUserAuction(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestDeleteUserAuction_ObjectNotFoundException()
        {
            userAuctionDataServiceStub
           .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
           .Equals(null);

            userAuction.DeleteUserAuction(userAuctionFirstDTO);
        }

        [TestMethod]
        public void TestGetListOfUserAuctions_Successfully()
        {
            userAuctionDataServiceStub
             .Setup(x => x.GetListOfUserAuctions())
             .Returns(new List<UserAuction>());

            userAuction.GetListOfUserAuctions();
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionById_IncorrectIdException()
        {
            userAuction.GetUserAuctionById(NEGATIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetUserAuctionById_Successfully()
        {
            userAuctionDataServiceStub
            .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
            .Returns(userAuctionFirst);

            userAuction.GetUserAuctionById(POSITIVE_USER_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserId_IncorrectIdException()
        {
            userAuction.GetUserAuctionsByUserId(NEGATIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetUserAuctionsByUserId_Successfully()
        {
            userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Returns(user);
            userAuctionDataServiceStub
               .Setup(x => x.GetUserAuctionsByUserId(It.IsAny<int>()))
               .Returns(new List<UserAuction>());
            userAuction.GetUserAuctionsByUserId(POSITIVE_USER_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The ObjectNotFoundException was thrown!")]
        public void TestGetUserAuctionsByUserId_ObjectNotFoundException()
        {
            userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Equals(null);
            userAuction.GetUserAuctionsByUserId(POSITIVE_USER_ID);
        }

        [TestMethod]
        public void TestGetUserAuctionsByUserIdandProductId_Successfully()
        {
            userAuctionDataServiceStub
            .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(new List<UserAuction>());

            userAuction.GetUserAuctionsByUserIdandProductId(POSITIVE_USER_ID, POSITIVE_PRODUCT_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_UserIdIsLessThan0()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(NEGATIVE_USER_ID, POSITIVE_PRODUCT_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_ProductIdIsLessThan0()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(POSITIVE_USER_ID, NEGATIVE_PRODUCT_ID);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_ProductIdAndUserIdIsLessThan0()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(NEGATIVE_USER_ID, NEGATIVE_PRODUCT_ID);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_UserIdIs0()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(0, NEGATIVE_PRODUCT_ID);
        }
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_ProductIdIs0()
        {
            userAuction.GetUserAuctionsByUserIdandProductId(POSITIVE_USER_ID, 0);
        }
        [TestMethod]
        public void TestUpdateUserAuction_Successfully()
        {
            userAuctionDataServiceStub
              .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
              .Returns(userAuctionFirst);

            userAuction.UpdateUserAuction(userAuctionFirstDTO);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestUpdateUserAuction_NullProduct()
        {
            userAuction.UpdateUserAuction(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The product was not found!")]
        public void TestUpdateUserAuction_NullReferenceException()
        {
            userAuctionDataServiceStub
              .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
              .Equals(null);

            userAuction.UpdateUserAuction(userAuctionFirstDTO);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddUserAuction_InvalidObjectException()
        {
            productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(productFirst);
            userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            userAuction.AddUserAuction(invalidUserAuctionDTO);
        }

    }
}
