using LegacyApp;
using System;
using Xunit;

namespace LegacyAppTests
{
    public class UserServiceFullCoverageTests
    {
        [Fact]
        public void AddUser_ReturnsFalse_WhenEmailDomainIsInvalid()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "john@doe", new DateTime(1980, 1, 1), 1);
            Assert.False(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_WhenAllConditionsAreMet()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "johndoe@example.com", new DateTime(1980, 1, 1), 1);
            Assert.True(result);
        }

        [Fact]
        public void AddUser_ReturnsFalse_WhenFirstNameIsEmpty()
        {
            var userService = new UserService();
            var result = userService.AddUser("", "Doe", "johndoe@example.com", new DateTime(1980, 1, 1), 1);
            Assert.False(result);
        }

        [Fact]
        public void AddUser_ReturnsFalse_WhenLastNameIsEmpty()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "", "johndoe@example.com", new DateTime(1980, 1, 1), 1);
            Assert.False(result);
        }

        [Fact]
        public void AddUser_ReturnsFalse_WhenEmailIsInvalid()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "notanemail", new DateTime(1980, 1, 1), 1);
            Assert.False(result);
        }

        [Fact]
        public void AddUser_ReturnsFalse_WhenUserIsUnderage()
        {
            var userService = new UserService();
            var underageBirthday = DateTime.Now.AddYears(-20);
            var result = userService.AddUser("John", "Doe", "johndoe@example.com", underageBirthday, 1);
            Assert.False(result);
        }

        [Fact]
        public void AddUser_ReturnsFalse_WhenClientDoesNotExist()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "johndoe@example.com", new DateTime(1980, 1, 1), 999);
            Assert.False(result);
        }

        [Fact]
        public void AddUser_ReturnsFalse_WhenCreditLimitIsLessThan500()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Kowalski", "johndoe@example.com", new DateTime(1980, 1, 1), 1);
            Assert.False(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_ForVeryImportantClient_WithoutCreditCheck()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Malewski", "johnmalewski@example.com", new DateTime(1980, 1, 1), 2);
            Assert.True(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_WhenImportantClientCreditLimitDoubled_IsAbove500()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Smith", "johnsmith@example.com", new DateTime(1980, 1, 1), 3);
            Assert.True(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_WhenNormalClientCreditLimitIsExactly500()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Kwiatkowski", "johnk@example.com", new DateTime(1980, 1, 1), 5);
            Assert.True(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_WhenUserIsExactly21()
        {
            var userService = new UserService();
            var birthday = DateTime.Now.AddYears(-21);
            var result = userService.AddUser("John", "Doe", "johndoe@example.com", birthday, 1);
            Assert.True(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_WhenEmailContainsPlusSign()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "john+doe@example.com", new DateTime(1980, 1, 1), 1);
            Assert.True(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_ForNormalClientWithHighCreditLimit()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "johndoe@example.com", new DateTime(1980, 1, 1), 4);
            Assert.True(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_WhenEmailIsValidAndHasMultipleSubdomains()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "john.doe@sub.example.com", new DateTime(1980, 1, 1), 1);
            Assert.True(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_WhenEmailIsLongButValid()
        {
            var userService = new UserService();
            var longEmail = "john.doe.very.long.email.but.still.valid@example.com";
            var result = userService.AddUser("John", "Doe", longEmail, new DateTime(1980, 1, 1), 1);
            Assert.True(result);
        }

        [Fact]
        public void AddUser_ReturnsFalse_WhenClientRepositoryThrowsException()
        {
            var userService = new UserService();
            var result = userService.AddUser("John", "Doe", "johndoe@example.com", new DateTime(1980, 1, 1), -1); // Invalid client ID
            Assert.False(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_ForNormalClient_WithCreditLimitAbove500()
        {
            var userService = new UserService();
            var result = userService.AddUser("Jane", "Doe", "janedoe@example.com", new DateTime(1970, 1, 1), 4); // ID 4 for NormalClient
            Assert.True(result);
        }

        [Fact]
        public void AddUser_ReturnsTrue_ForNormalClient_WithCreditLimitBelow500()
        {
            var userService = new UserService();
            // Assuming 'Doe' has a credit limit < 500 for NormalClient
            var result = userService.AddUser("John", "Smith", "johnsmith@example.com", new DateTime(1980, 1, 1), 4); // ID 4 for NormalClient
            Assert.True(result);
        }
        [Fact]
        public void Client_Properties_AreSetCorrectly()
        {
            // Arrange
            string name = "John Doe";
            int clientId = 123;
            string email = "john.doe@example.com";
            string address = "123 Main St, City, Country";
            string type = "Normal";

            // Act
            var client = new Client
            {
                Name = name,
                ClientId = clientId,
                Email = email,
                Address = address,
                Type = type
            };

            // Assert
            Assert.Equal(name, client.Name);
            Assert.Equal(clientId, client.ClientId);
            Assert.Equal(email, client.Email);
            Assert.Equal(address, client.Address);
            Assert.Equal(type, client.Type);
        }
        [Fact]
        public void GetCreditLimit_ReturnsCreditLimit_WhenClientExists()
        {
            // Arrange
            var userCreditService = new UserCreditService();
            var lastName = "Kowalski"; // Переконайтесь, що прізвище існує в словнику _database

            // Act
            var creditLimit = userCreditService.GetCreditLimit(lastName, DateTime.Now);

            // Assert
            Assert.Equal(200, creditLimit); // Перевірте, що повертається очікуване значення
        }

        [Fact]
        public void GetCreditLimit_ThrowsArgumentException_WhenClientDoesNotExist()
        {
            // Arrange
            var userCreditService = new UserCreditService();
            var lastName = "NonExistent"; // Використовуйте прізвище, якого немає в словнику _database

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => userCreditService.GetCreditLimit(lastName, DateTime.Now));
            Assert.Equal($"Client {lastName} does not exist", exception.Message);
        }
        [Fact]
        public void UserProperties_ShouldSetAndGetCorrectly()
        {
            // Arrange
            var user = new User
            {
                Client = new { Name = "TestClient" }, // Зазвичай Client буде типом, що підтримується вашою системою
                DateOfBirth = new DateTime(1980, 1, 1),
                EmailAddress = "test@example.com",
                FirstName = "Test",
                LastName = "User",
                HasCreditLimit = true,
                CreditLimit = 500
            };

            // Assert
            Assert.Equal("TestClient", user.Client.GetType().GetProperty("Name").GetValue(user.Client, null));
            Assert.Equal(new DateTime(1980, 1, 1), user.DateOfBirth);
            Assert.Equal("test@example.com", user.EmailAddress);
            Assert.Equal("Test", user.FirstName);
            Assert.Equal("User", user.LastName);
            Assert.True(user.HasCreditLimit);
            Assert.Equal(500, user.CreditLimit);
        }

    }
}
