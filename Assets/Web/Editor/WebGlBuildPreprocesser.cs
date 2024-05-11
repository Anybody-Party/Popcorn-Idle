using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

public class WebGlBuildPreprocesser : IPreprocessBuildWithReport
{
    public int callbackOrder { get; }

    public void OnPreprocessBuild(BuildReport report)
    {
#if CRAZY_GAMES
        if (PlayerSettings.WebGL.decompressionFallback)
            DisableDecompressionFallback();
#else
        if (!PlayerSettings.WebGL.decompressionFallback)
            EnableDecompressionFallback();
#endif
    }

    private static void EnableDecompressionFallback()
    {
        PlayerSettings.WebGL.decompressionFallback = true;
        Debug.Log("Decompression fallback was enabled!");
    }

    private static void DisableDecompressionFallback()
    {
        PlayerSettings.WebGL.decompressionFallback = false;
        Debug.Log("Decompression fallback was disabled!");
    }
}