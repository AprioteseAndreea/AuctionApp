// <copyright file="UserAuctionServiceTest.cs" company="Transilvania University of Brasov">
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
    public class UserAuctionServiceTest
    {
        private const int PositiveUserId = 5;
        private const int NegativeUserId = -5;

        private const int PositiveProductId = 4;
        private const int NegativeProductId = -4;

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
                BirthDate = "12.12.2000",
            };

            this.moneyFirst = new Money
            {
                Amount = 100,
                Currency = Currency.RON,
            };
            this.moneySecond = new Money
            {
                Amount = 50,
                Currency = Currency.USD,
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
                Id = 1,
                Product = this.productFirst,
                User = this.user,
                Price = this.moneySecond,
            };
            this.userAuctionSecond = new UserAuction
            {
                Id = 2,
                Product = this.productFirst,
                User = this.user,
                Price = this.moneyThird,
            };
            this.userAuctionThird = new UserAuction
            {
                Id = 3,
                Product = this.productFirst,
                User = this.user,
                Price = this.moneyFirst,
            };
            this.userAuctionFourth = new UserAuction
            {
                Id = 4,
                Product = this.productFirst,
                User = this.user,
                Price = new Money
                {
                    Amount = 400,
                    Currency = Currency.RON,
                },
            };
            this.invalidUserAuction = new UserAuction
            {
                Id = 0,
                Product = this.productFirst,
                User = null,
                Price = this.moneyThird,
            };

            this.userAuctionFirstDTO = new UserAuctionDTO(this.userAuctionFirst);
            this.userAuctionSecondDTO = new UserAuctionDTO(this.userAuctionSecond);
            this.userAuctionThirdDTO = new UserAuctionDTO(this.userAuctionThird);
            this.userAuctionFourthDTO = new UserAuctionDTO(this.userAuctionFourth);
            this.invalidUserAuctionDTO = new UserAuctionDTO(this.invalidUserAuction);

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

            this.productDataServicesStub = new Mock<IProductDataServices>();
            this.userAuctionDataServiceStub = new Mock<IUserAuctionDataServices>();
            this.userDataServicesStub = new Mock<IUserDataServices>();
            this.loggerMock = new Mock<ILog>();
            this.userAuction = new UserAuctionServicesImplementation(
                this.userAuctionDataServiceStub.Object,
                this.productDataServicesStub.Object,
                this.userDataServicesStub.Object,
                this.loggerMock.Object);
        }

        /// <summary>
        /// Tests the add user auction incompatible currency exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncompatibleCurrencyException), "")]
        public void TestAddUserAuction_IncompatibleCurrencyException()
        {
            this.productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(this.productFirst);
            this.userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());
            this.userAuction.AddUserAuction(this.userAuctionFirstDTO);
        }

        /// <summary>
        /// Tests the add user auction minimum bid exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(MinimumBidException), "")]
        public void TestAddUserAuction_MinimumBidException()
        {
            this.productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(this.productFirst);
            this.userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            this.userAuction.AddUserAuction(this.userAuctionThirdDTO);
        }

        /// <summary>
        /// Tests the add user auction closed auction exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ClosedAuctionException), "")]
        public void TestAddUserAuction_ClosedAuctionException()
        {
            this.productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(this.closedProduct);
            this.userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            this.userAuction.AddUserAuction(this.userAuctionThirdDTO);
        }

        /// <summary>
        /// Tests the add user auction overbidding exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(OverbiddingException), "")]
        public void TestAddUserAuction_OverbiddingException()
        {
            this.productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(this.productFirst);
            this.userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(this.userAuctions);

            this.userAuction.AddUserAuction(this.userAuctionThirdDTO);
        }

        /// <summary>
        /// Tests the add user auction overbidding exception amount is over300 percent.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(OverbiddingException), "")]
        public void TestAddUserAuction_OverbiddingException_AmountIsOver300Percent()
        {
            this.productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(this.productFirst);
            this.userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(this.userAuctions);

            this.userAuction.AddUserAuction(this.userAuctionFourthDTO);
        }

        /// <summary>
        /// Tests the add user auction successfully.
        /// </summary>
        [TestMethod]
        public void TestAddUserAuction_Successfully()
        {
            this.productDataServicesStub
              .Setup(x => x.GetProductById(It.IsAny<int>()))
              .Returns(this.productFirst);
            this.userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            this.userAuction.AddUserAuction(this.userAuctionSecondDTO);
        }

        /// <summary>
        /// Tests the delete user auction successfully.
        /// </summary>
        [TestMethod]
        public void TestDeleteUserAuction_Successfully()
        {
            this.userAuctionDataServiceStub
              .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
              .Returns(this.userAuctionFirst);

            this.userAuction.DeleteUserAuction(this.userAuctionFirstDTO);
        }

        /// <summary>
        /// Tests the delete user auction null product.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestDeleteUserAuction_NullProduct()
        {
            this.userAuction.DeleteUserAuction(null);
        }

        /// <summary>
        /// Tests the delete user auction object not found exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The product was not found!")]
        public void TestDeleteUserAuction_ObjectNotFoundException()
        {
            this.userAuctionDataServiceStub
           .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
           .Equals(null);

            this.userAuction.DeleteUserAuction(this.userAuctionFirstDTO);
        }

        /// <summary>
        /// Tests the get list of user auctions successfully.
        /// </summary>
        [TestMethod]
        public void TestGetListOfUserAuctions_Successfully()
        {
            this.userAuctionDataServiceStub
             .Setup(x => x.GetListOfUserAuctions())
             .Returns(new List<UserAuction>());

            this.userAuction.GetListOfUserAuctions();
        }

        /// <summary>
        /// Tests the get user auction by identifier incorrect identifier exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionById_IncorrectIdException()
        {
            this.userAuction.GetUserAuctionById(NegativeUserId);
        }

        /// <summary>
        /// Tests the get user auction by identifier successfully.
        /// </summary>
        [TestMethod]
        public void TestGetUserAuctionById_Successfully()
        {
            this.userAuctionDataServiceStub
            .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
            .Returns(this.userAuctionFirst);

            this.userAuction.GetUserAuctionById(PositiveUserId);
        }

        /// <summary>
        /// Tests the get user auctions by user identifier incorrect identifier exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserId_IncorrectIdException()
        {
            this.userAuction.GetUserAuctionsByUserId(NegativeUserId);
        }

        /// <summary>
        /// Tests the get user auctions by user identifier successfully.
        /// </summary>
        [TestMethod]
        public void TestGetUserAuctionsByUserId_Successfully()
        {
            this.userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Returns(this.user);
            this.userAuctionDataServiceStub
               .Setup(x => x.GetUserAuctionsByUserId(It.IsAny<int>()))
               .Returns(new List<UserAuction>());
            this.userAuction.GetUserAuctionsByUserId(PositiveUserId);
        }

        /// <summary>
        /// Tests the get user auctions by user identifier object not found exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ObjectNotFoundException), "The ObjectNotFoundException was thrown!")]
        public void TestGetUserAuctionsByUserId_ObjectNotFoundException()
        {
            this.userDataServicesStub
            .Setup(x => x.GetUserById(It.IsAny<int>()))
            .Equals(null);
            this.userAuction.GetUserAuctionsByUserId(PositiveUserId);
        }

        /// <summary>
        /// Tests the get user auctions by user idand product identifier successfully.
        /// </summary>
        [TestMethod]
        public void TestGetUserAuctionsByUserIdandProductId_Successfully()
        {
            this.userAuctionDataServiceStub
            .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
            .Returns(new List<UserAuction>());

            this.userAuction.GetUserAuctionsByUserIdandProductId(PositiveUserId, PositiveProductId);
        }

        /// <summary>
        /// Tests the get user auctions by user idand product identifier incorrect identifier exception user identifier is less than0.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_UserIdIsLessThan0()
        {
            this.userAuction.GetUserAuctionsByUserIdandProductId(NegativeUserId, PositiveProductId);
        }

        /// <summary>
        /// Tests the get user auctions by user idand product identifier incorrect identifier exception product identifier is less than0.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_ProductIdIsLessThan0()
        {
            this.userAuction.GetUserAuctionsByUserIdandProductId(PositiveUserId, NegativeProductId);
        }

        /// <summary>
        /// Tests the get user auctions by user idand product identifier incorrect identifier exception product identifier and user identifier is less than0.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_ProductIdAndUserIdIsLessThan0()
        {
            this.userAuction.GetUserAuctionsByUserIdandProductId(NegativeUserId, NegativeProductId);
        }

        /// <summary>
        /// Tests the get user auctions by user idand product identifier incorrect identifier exception user identifier is0.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_UserIdIs0()
        {
            this.userAuction.GetUserAuctionsByUserIdandProductId(0, NegativeProductId);
        }

        [TestMethod]
        [ExpectedException(typeof(IncorrectIdException), "")]
        public void TestGetUserAuctionsByUserIdandProductId_IncorrectIdException_ProductIdIs0()
        {
            this.userAuction.GetUserAuctionsByUserIdandProductId(PositiveUserId, 0);
        }

        /// <summary>
        /// Tests the update user auction successfully.
        /// </summary>
        [TestMethod]
        public void TestUpdateUserAuction_Successfully()
        {
            this.userAuctionDataServiceStub
              .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
              .Returns(this.userAuctionFirst);

            this.userAuction.UpdateUserAuction(this.userAuctionFirstDTO);
        }

        /// <summary>
        /// Tests the update user auction null product.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "The object can not be null.")]
        public void TestUpdateUserAuction_NullProduct()
        {
            this.userAuction.UpdateUserAuction(null);
        }

        /// <summary>
        /// Tests the update user auction null reference exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException), "The product was not found!")]
        public void TestUpdateUserAuction_NullReferenceException()
        {
            this.userAuctionDataServiceStub
              .Setup(x => x.GetUserAuctionById(It.IsAny<int>()))
              .Equals(null);

            this.userAuction.UpdateUserAuction(this.userAuctionFirstDTO);
        }

        /// <summary>
        /// Tests the add user auction invalid object exception.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidObjectException), "")]
        public void TestAddUserAuction_InvalidObjectException()
        {
            this.productDataServicesStub
                .Setup(x => x.GetProductById(It.IsAny<int>()))
                .Returns(this.productFirst);
            this.userAuctionDataServiceStub
                .Setup(x => x.GetUserAuctionsByUserIdandProductId(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<UserAuction>());

            this.userAuction.AddUserAuction(this.invalidUserAuctionDTO);
        }
    }
}
