using AutoFixture;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZipPayWebApp.BAL;
using ZipPayWebApp.BAL.Entity;
using ZipPayWebApp.BAL.Exceptions;
using ZipPayWebApp.DAL;
using ZipPayWebApp.DAL.Entity;

namespace ZipPayWebApi.Tests.BAL
{
    [TestFixture]
    public class AccountsServiceTests
    {
        private IAccountsService _accountsService;
        private Mock<IAccountsRepo> _accountRepo;
        private Mock<IMapper> _mapper;
        private Mock<IUsersService> _userService;
        private Fixture _fixture;

        [SetUp] 
        public void AccountsServiceTestsSetUp()
        {
            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();
            _accountRepo = new Mock<IAccountsRepo>();
            _userService = new Mock<IUsersService>();
            _accountsService = new AccountsService(_accountRepo.Object, _mapper.Object, _userService.Object);
        }

        [TestFixture]
        public class AccountsServiceMethods : AccountsServiceTests
        {
            [Test]
            public void should_get_account_details_by_userid()
            {
                // Arrange
                var userId = _fixture.Create<int>();
                var account = _fixture.Create<ACCOUNT>();
                var expected = _fixture.Create<AccountModel>();

                _accountRepo.Setup(s => s.GetAccount(userId)).Returns(account);
                _mapper.Setup(s => s.Map<AccountModel>(account)).Returns(expected);

                // act
                var actual = _accountsService.GetAccount(userId);

                // asset
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void should_create_account_for_user()
            {
                // Arrange
                var userId = _fixture.Create<int>();
                var account = _fixture.Create<AccountModel>();
                var accountDto = _fixture.Create<ACCOUNT>();
                account.User_Id = userId;
               

                var user = new UserModel { Id = userId, Monthly_Expense= 500, Monthly_Salary=2000 };
                _userService.Setup(user => user.GetUser(userId)).Returns(user);
                _mapper.Setup(s => s.Map<ACCOUNT>(account)).Returns(accountDto);
                var expected = It.IsAny<int>();
                _accountRepo.Setup(s => s.CreateAccount(accountDto)).Returns(expected);
                // act
                var actual = _accountsService.CreateAccount(account);

                // asset
                Assert.AreEqual(actual, expected);
                _accountRepo.Verify(s => s.CreateAccount(accountDto), Times.Once());
            }

            [Test]
            public void should_throw_exception_when_user_is_have_less_credit_limit()
            {
                // Arrange
                var userId = _fixture.Create<int>();
                var account = _fixture.Create<AccountModel>();
                var accountDto = _fixture.Create<ACCOUNT>();
                account.User_Id = userId;

                var user = new UserModel { Id = userId, Monthly_Expense = 500, Monthly_Salary = 1000 };
                _userService.Setup(user => user.GetUser(userId)).Returns(user);
                _mapper.Setup(s => s.Map<ACCOUNT>(account)).Returns(accountDto);
                var expected = It.IsAny<int>();
                _accountRepo.Setup(s => s.CreateAccount(accountDto)).Returns(expected);

                // act + asset
                Assert.Throws<BadRequestException>(() => _accountsService.CreateAccount(account));

            }
        }



    }
}
