using AutoFixture;
using AutoMapper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using ZipPay.BAL;
using ZipPay.BAL.Entity;
using ZipPay.BAL.Exceptions;
using ZipPay.DAL;
using ZipPayWebApp.DAL.Entity;

namespace ZipPayWebApi.Tests.BAL
{
    public class UsersServiceTests
    {
        private IUsersService _usersService;
        private Mock<IUsersRepo> _usersRepo;
        private Mock<IMapper> _mapper;
        private Fixture _fixture;

        [SetUp]
        public void UsersServiceTestsSetUp()
        {
            _fixture = new Fixture();
            _mapper = new Mock<IMapper>();
            _usersRepo = new Mock<IUsersRepo>();
            
            _usersService = new UsersService(_usersRepo.Object, _mapper.Object);
        }



        [TestFixture]
        public class UsersServiceMethods : UsersServiceTests
        {
            [Test]
            public void should_get_all_users()
            {
                // Arrange
                var users = _fixture.Create<IEnumerable<USER>>();
                var expected = _fixture.Create<IEnumerable<UserModel>>();

                _usersRepo.Setup(s => s.GetAllUser()).Returns(users);
                _mapper.Setup(s => s.Map<IEnumerable<UserModel>>(users)).Returns(expected);

                // act
                var actual = _usersService.GetAllUser();

                // asset
                Assert.AreEqual(expected, actual);
            }


            [Test]
            public void should_get_user_deteails_based_on_id()
            {
                // Arrange
                var userId = _fixture.Create<int>();
                var users = _fixture.Create<USER>();
                var expected = _fixture.Create<UserModel>();

                _usersRepo.Setup(s => s.GetUser(userId)).Returns(users);
                _mapper.Setup(s => s.Map<UserModel>(users)).Returns(expected);

                // act
                var actual = _usersService.GetUser(userId);

                // asset
                Assert.AreEqual(expected, actual);
            }

            [Test]
            public void should_create_user()
            {
                // Arrange
                var userId = _fixture.Create<int>();
                var user = _fixture.Create<UserModel>();
                var userDto = _fixture.Create<USER>();
      
                _mapper.Setup(s => s.Map<USER>(user)).Returns(userDto);
                var expected = It.IsAny<int>();
                _usersRepo.Setup(s => s.CreateUser(userDto)).Returns(expected);
                // act
                var actual = _usersService.CreateUser(user);

                // asset
                Assert.AreEqual(actual, expected);
                _usersRepo.Verify(s => s.CreateUser(userDto), Times.Once());
            }

            [Test]
            public void should_throw_exception_when_email_Id_exists_while_create_user()
            {
                // Arrange
                var userId = _fixture.Create<int>();
                var user = _fixture.Create<UserModel>();
                var userDto = _fixture.Create<USER>();

                _mapper.Setup(s => s.Map<USER>(user)).Returns(userDto);
                var expected = It.IsAny<int>();
                _usersRepo.Setup(s => s.GetUser(user.Email_Id)).Returns(_fixture.Create<USER>());
                _usersRepo.Setup(s => s.CreateUser(userDto)).Returns(expected);

                 // act + asset
                Assert.Throws<BadRequestException>(() => _usersService.CreateUser(user));
            }


            [Test]
            public void should_throw_exception_when_month_expense_is_negative_while_creating_user()
            {
                // Arrange
                var userId = _fixture.Create<int>();
                var user = _fixture.Create<UserModel>();
                var userDto = _fixture.Create<USER>();
                user.Monthly_Expense = -1;

                _mapper.Setup(s => s.Map<USER>(user)).Returns(userDto);
                var expected = It.IsAny<int>();
                _usersRepo.Setup(s => s.CreateUser(userDto)).Returns(expected);

                // act + asset
                Assert.Throws<BadRequestException>(() => _usersService.CreateUser(user));
            }

            [Test]
            public void should_throw_exception_when_month_salary_is_negative_while_creating_user()
            {
                // Arrange
                var userId = _fixture.Create<int>();
                var user = _fixture.Create<UserModel>();
                var userDto = _fixture.Create<USER>();
                user.Monthly_Salary = -1;

                _mapper.Setup(s => s.Map<USER>(user)).Returns(userDto);
                var expected = It.IsAny<int>();
                _usersRepo.Setup(s => s.CreateUser(userDto)).Returns(expected);

                // act + asset
                Assert.Throws<BadRequestException>(() => _usersService.CreateUser(user));
            }

        }

        
    }
}
