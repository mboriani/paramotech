using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Newtonsoft.Json;
using Sat.Recruitment.Api.Controllers;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("IntegrationTestPost", DisableParallelization = true)]
    public class UserControllerTest : IntegrationTest
    {
        [Fact]
        public async Task Get_ReturnsUser_WhenUserExistsInDataBase()
        {
            // Arrange
            var userDto = new UserDto()
            {
                Name = "Miguel",
                Money = 100m,
                Address = "22 y 5",
                Email = "mike@gmail.com",
                Phone = "4970295",
                UserType = "Normal"
            };
            await this.Post("/Users", userDto);

            // Act
            var response = await testClient.GetAsync("/Users");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var respGet = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<List<UserDto>>(respGet);
            result.Should().NotBeEmpty();
        }
        [Fact]
        public async Task Post_ANewUser_WhenUserNotExistInDataBase()
        {
            // Arrange
            var userDto = new UserDto()
            {
                Name = "Liliana",
                Money = 110m,
                Address = "22 esq 7",
                Email = "liliana@gmail.com",
                Phone = "753159",
                UserType = "Normal"
            };
            

            // Act
            var response = await this.Post("/Users", userDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Post_ANewUser_WhenThisUserExistInDataBase()
        {
            // Arrange
            var userDto = new UserDto()
            {
                Name = "Cristian U",
                Money = 110m,
                Address = "22 y 2",
                Email = "cristianu@gmail.com",
                Phone = "4794369",
                UserType = "Normal"
            };
            await this.Post("/Users", userDto);

            // Act
            var response = await this.Post("/Users", userDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respPost = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ValidationError>(respPost);
            result.Errors.Should().NotBeEmpty();
            result.Title.Should().Be("One or more validation errors occurred.");
            result.Errors.First().Should().Be("The user is duplicated");
        }

        [Fact]
        public async Task Post_ANewUser_WhenASameEmailExistInDataBase()
        {
            // Arrange
            var previusUser = new UserDto()
            {
                Name = "Cristian Rodriguez",
                Money = 110m,
                Address = "22 y 1",
                Email = "cristianrodriguez@gmail.com",
                Phone = "4794366",
                UserType = "Normal"
            };
            var userDto = new UserDto()
            {
                Name = "Fer Aramburu",
                Money = 110m,
                Address = "22 y 10",
                Email = "cristianrodriguez@gmail.com",
                Phone = "4794367",
                UserType = "Normal"
            };
            await this.Post("/Users", previusUser);

            // Act
            var response = await this.Post("/Users", userDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respPost = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ValidationError>(respPost);
            result.Errors.Should().NotBeEmpty();
            result.Title.Should().Be("One or more validation errors occurred.");
            result.Errors.First().Should().Be("The user is duplicated");
        }

        [Fact]
        public async Task Post_ANewUser_WhenASamePhoneExistInDataBase()
        {
            // Arrange
            var previusUser = new UserDto()
            {
                Name = "Juan",
                Money = 110m,
                Address = "22 y 23",
                Email = "juan@gmail.com",
                Phone = "4794365",
                UserType = "Normal"
            };
            var userDto = new UserDto()
            {
                Name = "Raul",
                Money = 110m,
                Address = "22 y 24",
                Email = "raul@gmail.com",
                Phone = "4794365",
                UserType = "Normal"
            };
            await this.Post("/Users", previusUser);

            // Act
            var response = await this.Post("/Users", userDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respPost = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ValidationError>(respPost);
            result.Errors.Should().NotBeEmpty();
            result.Title.Should().Be("One or more validation errors occurred.");
            result.Errors.First().Should().Be("The user is duplicated");
        }

        [Fact]
        public async Task Post_ANewUser_WhenASameNameAndAdressExistInDataBase()
        {
            // Arrange
            var previusUser = new UserDto()
            {
                Name = "Alfonso",
                Money = 110m,
                Address = "4794371",
                Email = "alfonso@gmail.com",
                Phone = "4794372",
                UserType = "Normal"
            };
            var userDto = new UserDto()
            {
                Name = "Alfonso",
                Money = 110m,
                Address = "4794371",
                Email = "firulete@gmail.com",
                Phone = "4794376",
                UserType = "Normal"
            };
            await this.Post("/Users", previusUser);

            // Act
            var response = await this.Post("/Users", userDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respPost = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ValidationError>(respPost);
            result.Errors.Should().NotBeEmpty();
            result.Title.Should().Be("One or more validation errors occurred.");
            result.Errors.First().Should().Be("The user is duplicated");
        }

        [Fact]
        public async Task Post_ANewUserWihOutName()
        {
            // Arrange
            var userDto = new UserDto()
            {
                Money = 110m,
                Address = "1234",
                Email = "jj@gmail.com",
                Phone = "123456",
                UserType = "Normal"
            };
            
            // Act
            var response = await this.Post("/Users", userDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respPost = await response.Content.ReadAsStringAsync();
            respPost.Should().Contain("One or more validation errors occurred.");
            respPost.Should().Contain("The Name field is required.");
        }

        [Fact]
        public async Task Post_ANewUserWihOutAdress()
        {
            // Arrange
            var userDto = new UserDto()
            {
                Name = "Pablo",
                Money = 110m,
                Email = "jj@gmail.com",
                Phone = "123456",
                UserType = "Normal"
            };

            // Act
            var response = await this.Post("/Users", userDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respPost = await response.Content.ReadAsStringAsync();
            respPost.Should().Contain("One or more validation errors occurred.");
            respPost.Should().Contain("The Address field is required.");
        }

        [Fact]
        public async Task Post_ANewUserWihOutEmail()
        {
            // Arrange
            var userDto = new UserDto()
            {
                Name = "Pablo",
                Address = "4563",
                Money = 110m,
                Phone = "123456",
                UserType = "Normal"
            };

            // Act
            var response = await this.Post("/Users", userDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respPost = await response.Content.ReadAsStringAsync();
            respPost.Should().Contain("One or more validation errors occurred.");
            respPost.Should().Contain("The Email field is required.");
        }

        [Fact]
        public async Task Post_ANewUserWihOutPhone()
        {
            // Arrange
            var userDto = new UserDto()
            {
                Name = "Pablo",
                Address = "4563",
                Money = 110m,
                Email = "jj@gmail.com",
                UserType = "Normal"
            };

            // Act
            var response = await this.Post("/Users", userDto);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
            var respPost = await response.Content.ReadAsStringAsync();
            respPost.Should().Contain("One or more validation errors occurred.");
            respPost.Should().Contain("The Phone field is required.");
        }
    }
}