using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.Service
{
    /// <summary>
    /// This class is used to register important services
    /// </summary>
    public class ServiceRegistry
    {
        public static Dictionary<string, object> RegisteredService { get; } = RegisterService();

        /// <summary>
        /// Register important services
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, object> RegisterService()
        {
            var registeredService = new Dictionary<string, object>();

            registeredService.Add("IDao", new DataAcess.MockDao());

            return registeredService;
        }
    }
}
