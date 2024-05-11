using System;
using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

public static class WebGlBuildPostprocesser
{
    private const string YandexSdkText = "<script type='text/javascript' src='yandexSDK.js'></script>";
    private const string VkSdkText = "<script type='text/javascript' src='vkSDK.js'></script>";
    private const string GamePixSdkText = "<script type='text/javascript' src='gamePixSDK.js'></script>";
    private const string InitSdkText = "<script> initSdk() </script>";
    private const string HtmlHeadTag = "<head>";
    private const string HtmlHeadCloseTag = "</head>";
    private const string IndexFileName = "index.html";


    [PostProcessBuild]
    private static void PostBuild(BuildTarget target, string pathToBuiltProject)
    {
        string path = Path.Combine(pathToBuiltProject + $"/{IndexFileName}");
#if YANDEX
        InsertSdkText(path, YandexSdkText);
        InsertInitSdkText(path, InitSdkText);
        Debug.Log($"Yandex SDK was injected in {IndexFileName}");
#elif VK
        InsertSdkText(path, VkSdkText);
        InsertInitSdkText(path, InitSdkText);
        Debug.Log($"VK SDK was injected in {IndexFileName}");
#elif GAME_PIX
        InsertSdkText(path, GamePixSdkText);
        InsertInitSdkText(path, InitSdkText);
        Debug.Log($"GamePix SDK was injected in {IndexFileName}");
#endif
        DeleteNotUsedSdks(pathToBuiltProject);
    }

    private static void DeleteNotUsedSdks(string pathToBuiltProject)
    {
#if !YANDEX
        File.Delete(Path.Combine(pathToBuiltProject + "/yandexSDK.js"));
#endif
#if !VK
        File.Delete(Path.Combine(pathToBuiltProject + "/vkSDK.js"));
#endif
#if !GAME_PIX
        File.Delete(Path.Combine(pathToBuiltProject + "/gamePixSDK.js"));
#endif
    }

    private static void InsertSdkText(string filePath, string sdkText)
    {
        string fileText = File.ReadAllText(filePath);
        InsertTextBeforeTag(ref fileText, sdkText, HtmlHeadCloseTag);
        File.WriteAllText(filePath, fileText);
    }

    private static void InsertInitSdkText(string filePath, string initSdkText)
    {
        string fileText = File.ReadAllText(filePath);
        InsertTextBeforeTag(ref fileText, initSdkText, HtmlHeadCloseTag);
        File.WriteAllText(filePath, fileText);
    }

    private static void InsertTextBeforeTag(ref string text, string newText, string tag)
    {
        text = text.Insert(text.IndexOf(tag, StringComparison.Ordinal), $"    {newText}\n");
    }

    private static void InsertTextAfterTag(ref string text, string newText, string tag)
    {
        text = text.Insert(text.IndexOf(tag, StringComparison.Ordinal) + tag.Length, $"\n    {newText}");
    }
}