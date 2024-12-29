using System.Collections.Generic;

namespace MacroTrackerUI.Services.PathService;

/// <summary>
/// This class is used to register important assets path.
/// </summary>
public class AssetsPathRegistry
{
    /// <summary>
    /// Gets the dictionary of registered assets paths.
    /// </summary>
    public static Dictionary<string, string> RegisteredAssetsPath { get; } = RegisterAssetsPath();

    /// <summary>
    /// Registers the assets paths.
    /// </summary>
    /// <returns>A dictionary containing the registered assets paths.</returns>
    private static Dictionary<string, string> RegisterAssetsPath()
    {
        var registeredAssetsPath = new Dictionary<string, string>
        {
            { "ExerciseIcon", "ms-appx:///Assets/ExerciseIcons" }
        };

        return registeredAssetsPath;
    }
}
