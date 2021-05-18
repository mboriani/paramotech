using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Data;
using Sat.Recruitment.Api.Model;
using Sat.Recruitment.Api.Services;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UserServiceTest
    {
        private Mock<IUserDao> dao;
        private UserMapper mapper;
        private UserService service;

        public UserServiceTest()
        {
            dao = new Mock<IUserDao>(MockBehavior.Strict);
            mapper = new UserMapper();
            service = new UserService(dao.Object, mapper);
        }

        [Fact]
        public async Task GetAllUser_WhenUserEmptyInDataBase()
        {
            // Arrange
            dao.Setup(x => x.Get(It.IsAny<Expression<Func<User, object>>>())).ReturnsAsync(new List<User>());

            // Act
            var result = await service.GetAll();

            //// Assert
            result.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllUser_WhenUserExistInDataBase()
        {
            // Arrange
            dao.Setup(x => x.Get(It.IsAny<Expression<Func<User, object>>>())).ReturnsAsync(new List<User>()
            {
                new User()
                {
                    Address = "38 n 16",
                    Role = new Premium(),
                    Email = "mike@gmail.com",
                    Name = "miguel",
                    Money = 255m,
                    Phone = "45656565"
                }
            });

            // Act
            var result = await service.GetAll();

            //// Assert
            result.Should().NotBeEmpty();
            result.Count.Should().Be(1);
        }

        [Fact]
        public async Task AddAnUser_WhenUserNotExistInDataBase()
        {
            // Arrange
            UserDto user = new UserDto()
            {
                Address = "38 n 16",
                UserType = "Premium",
                Email = "mike@gmail.com",
                Name = "miguel",
                Money = 255m,
                Phone = "45656565"
            };
            dao.Setup(d => d.FindBy(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User>());
            dao.Setup(d => d.Add(It.IsAny<User>())).ReturnsAsync(new User());

            // Act
            var result = await service.AddUser(user);

            //// Assert
            result.Should().NotBeNull();
            result.Errors.Count.Should().Be(0);
        }

        [Fact]
        public async Task AddAnUser_WhenUserExistInDataBase()
        {
            // Arrange
            UserDto user = new UserDto()
            {
                Address = "38 n 16",
                UserType = "Premium",
                Email = "mike@gmail.com",
                Name = "miguel",
                Money = 255m,
                Phone = "45656565"
            };
            dao.Setup(d => d.FindBy(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(new List<User>(){new User()});

            // Act
            var result = await service.AddUser(user);

            //// Assert
            result.Should().NotBeNull();
            result.Errors.Count.Should().Be(1);
        }
    }
}