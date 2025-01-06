using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.Services.SenderService.EncryptionSender;
using MacroTrackerUI.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace MacroTrackerUITest.ViewModels
{
    [TestClass]
    public class LoginViewModelTests
    {
        private Mock<IDaoSender> _mockDaoSender;
        private Mock<IPasswordEncryptionSender> _mockEncryptionSender;
        private Mock<IServiceProvider> _serviceProvider;
        private LoginViewModel _viewModel;

        [TestInitialize]
        public void Setup()
        {
            _mockDaoSender = new Mock<IDaoSender>();
            _mockEncryptionSender = new Mock<IPasswordEncryptionSender>();

            _serviceProvider = new Mock<IServiceProvider>();
            _serviceProvider.Setup(service => service.GetService(typeof(IDaoSender))).Returns(_mockDaoSender.Object);
            _serviceProvider.Setup(service => service.GetService(typeof(IPasswordEncryptionSender))).Returns(_mockEncryptionSender.Object);

            _viewModel = new LoginViewModel(_serviceProvider.Object);
        }

        [TestMethod]
        public void DoesUserMatchPassword_ReturnsTrue_WhenCredentialsMatch()
        {
            // Arrange
            _viewModel.Username = "testuser";
            _viewModel.Password = "testpassword";
            _mockDaoSender.Setup(dao => dao.DoesUserMatchPassword("testuser", "testpassword")).Returns(true);

            // Act
            var result = _viewModel.DoesUserMatchPassword();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DoesUserMatchPassword_ReturnsFalse_WhenCredentialsDoNotMatch()
        {
            // Arrange
            _viewModel.Username = "testuser";
            _viewModel.Password = "wrongpassword";
            _mockDaoSender.Setup(dao => dao.DoesUserMatchPassword("testuser", "wrongpassword")).Returns(false);

            // Act
            var result = _viewModel.DoesUserMatchPassword();

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LoginInfoNull_ReturnsTrue_WhenUsernameIsNull()
        {
            // Arrange
            _viewModel.Username = null;
            _viewModel.Password = "testpassword";

            // Act
            var result = _viewModel.LoginInfoNull();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LoginInfoNull_ReturnsTrue_WhenPasswordIsNull()
        {
            // Arrange
            _viewModel.Username = "testuser";
            _viewModel.Password = null;

            // Act
            var result = _viewModel.LoginInfoNull();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LoginInfoNull_ReturnsFalse_WhenUsernameAndPasswordAreNotNull()
        {
            // Arrange
            _viewModel.Username = "testuser";
            _viewModel.Password = "testpassword";

            // Act
            var result = _viewModel.LoginInfoNull();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
