using MacroTrackerUI.Services.SenderService.DataAccessSender;
using MacroTrackerUI.Services.SenderService.EncryptionSender;
using MacroTrackerUI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerUITest.ViewModels;

[TestClass]
public class SignupViewModelTests
{
    private Mock<IDaoSender> _mockDaoSender;
    private Mock<IPasswordEncryptionSender> _mockPasswordEncryptionSender;
    private ServiceProvider _serviceProvider;
    private SignUpViewModel _viewModel;

    [TestInitialize]
    public void Setup()
    {
        var serviceCollection = new ServiceCollection();
        _mockDaoSender = new Mock<IDaoSender>();
        _mockPasswordEncryptionSender = new Mock<IPasswordEncryptionSender>();

        serviceCollection.AddSingleton(_mockDaoSender.Object);
        serviceCollection.AddSingleton(_mockPasswordEncryptionSender.Object);
        _serviceProvider = serviceCollection.BuildServiceProvider();

        _viewModel = new SignUpViewModel(_serviceProvider);
    }

    [TestMethod]
    public void IsSignUpValid_UsernameIsNull_ReturnsNull()
    {
        _viewModel.Username = null;
        var result = _viewModel.IsSignUpValid(out string promptMessage);
        Assert.IsNull(result);
        Assert.IsNull(promptMessage);
    }

    [TestMethod]
    public void IsSignUpValid_UsernameExists_ReturnsFalse()
    {
        _viewModel.Username = "existingUser";
        _mockDaoSender.Setup(s => s.DoesUsernameExist(It.IsAny<string>())).Returns(true);

        var result = _viewModel.IsSignUpValid(out string promptMessage);

        Assert.IsFalse(result);
        Assert.AreEqual("Username has already existed!", promptMessage);
    }

    [TestMethod]
    public void IsSignUpValid_PasswordNotStrong_ReturnsFalse()
    {
        _viewModel.Username = "newUser";
        _viewModel.Password = "weak";
        _viewModel.ReenteredPassword = "weak";
        _mockDaoSender.Setup(s => s.DoesUsernameExist(It.IsAny<string>())).Returns(false);

        var result = _viewModel.IsSignUpValid(out string promptMessage);

        Assert.IsFalse(result);
        Assert.AreEqual("Password is not strong!", promptMessage);
    }

    [TestMethod]
    public void IsSignUpValid_PasswordsDoNotMatch_ReturnsFalse()
    {
        _viewModel.Username = "newUser";
        _viewModel.Password = "StrongPassword";
        _viewModel.ReenteredPassword = "DifferentPassword";
        _mockDaoSender.Setup(s => s.DoesUsernameExist(It.IsAny<string>())).Returns(false);

        var result = _viewModel.IsSignUpValid(out string promptMessage);

        Assert.IsFalse(result);
        Assert.AreEqual("Passwords do not match!", promptMessage);
    }

    [TestMethod]
    public void IsSignUpValid_ValidSignUp_ReturnsTrue()
    {
        _viewModel.Username = "newUser";
        _viewModel.Password = "StrongPassword";
        _viewModel.ReenteredPassword = "StrongPassword";
        _mockDaoSender.Setup(s => s.DoesUsernameExist(It.IsAny<string>())).Returns(false);
        _mockPasswordEncryptionSender.Setup(s => s.EncryptPasswordToDatabase(It.IsAny<string>())).Returns("encryptedPassword");

        var result = _viewModel.IsSignUpValid(out string promptMessage);

        Assert.IsTrue(result);
        Assert.AreEqual("Sign up successfully!", promptMessage);
        _mockDaoSender.Verify(s => s.AddUser(It.Is<(string, string)>(u => u.Item1 == "newUser" && u.Item2 == "encryptedPassword")), Times.Once);
    }
}
