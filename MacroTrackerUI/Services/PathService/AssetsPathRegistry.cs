using System.Collections.Generic;

namespace MacroTrackerUI.Services.PathService;

/// <summary>
/// This class is used to register important assets path
/// </summary>
public class AssetsPathRegistry
{
    public static Dictionary<string, string> RegisteredAssetsPath { get; } = RegisterAssetsPath();

    /// <summary>
    /// Register assets path
    /// </summary>
    /// <returns></returns>
    private static Dictionary<string, string> RegisterAssetsPath()
    {
        var registeredAssetsPath = new Dictionary<string, string>
        {
            { "ExerciseIcon", "ms-appx:///Assets/ExerciseIcons" }
        };

        return registeredAssetsPath;
    }
}
