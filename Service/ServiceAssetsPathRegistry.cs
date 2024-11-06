using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTracker.Service
{
    /// <summary>
    /// This class is used to register important assets path
    /// </summary>
    public class ServiceAssetsPathRegistry
    {
        public static Dictionary<string, string> RegisteredAssetsPath { get; } = RegisterAssetsPath();

        /// <summary>
        /// Register assets path
        /// </summary>
        /// <returns></returns>
        private static Dictionary<string, string> RegisterAssetsPath()
        {
            var registeredAssetsPath = new Dictionary<string, string>();

            registeredAssetsPath.Add("ExerciseIcon", "ms-appx:///Assets/ExerciseIcons");

            return registeredAssetsPath;
        }
    }
}
