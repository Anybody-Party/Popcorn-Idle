using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

[DisplayName("My Startup Selector")]
[Serializable]
public class Language : IStartupLocaleSelector
{
    [DllImport("__Internal")]
    private static extern string GetLanguage();

    public Locale GetStartupLocale(ILocalesProvider availableLocales)
    {
        Locale result = null;
#if !UNITY_EDITOR && (YANDEX || GAME_PIX)
        string language = GetLanguage();
        Debug.Log($"try language: {language}");
        result = availableLocales.GetLocale(language);
#elif CRAZY_GAMES
        CrazyGames.CrazySDK.Instance.GetUserInfo(userInfo => result = availableLocales.GetLocale(userInfo.countryCode));
#endif
        return result;
    }
}