using MacroTrackerCore.Services.DataAccessService;
using MacroTrackerCore.Entities;
using MacroTrackerCore.Services.EncryptionService;
using Moq;
using MacroTrackerCore.Services.ConfigurationService;

namespace MacroTrackerCoreTest.Services.DataAccessService;

[TestClass]
public class DatabaseDaoTests
{
    private DatabaseDao _databaseDao;

    [TestInitialize]
    public void Setup_Context_CreatesInMemoryDatabase()
    {
        _databaseDao = new DatabaseDao(true);
    }
}
