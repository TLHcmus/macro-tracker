using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Services.EncryptionService;
using MacroTrackerCore.Services.ReceiverService.DataAccessReceiver;
using MacroTrackerCore.Services.ReceiverService.EncryptionReceiver;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Text.Json;

namespace MacroTrackerCoreTest.Services.ReceiverService.DataAccesReceiver;

[TestClass]
public class DaoReceiverTests
{
    private Mock<IDao> _mockDao;
    private Mock<IPasswordEncryption> _mockPasswordEncryption;
    private DaoReceiver _daoReceiver;
    private JsonSerializerOptions _options; 

    [TestInitialize]
    public void Setup()
    {
        _mockDao = new Mock<IDao>();
        _mockPasswordEncryption = new Mock<IPasswordEncryption>();

        var serviceProvider = new ServiceCollection()
            .AddSingleton(_mockDao.Object)
            .AddSingleton(_mockPasswordEncryption.Object)
            .BuildServiceProvider();

        _daoReceiver = new DaoReceiver(serviceProvider);

        _options = new() { IncludeFields = true };
    }
}
