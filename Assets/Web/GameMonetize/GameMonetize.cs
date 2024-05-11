using System;
using System.Runtime.InteropServices;
using __Scripts.UnityComponents.REDO.SingletonAccess;
using UnityEngine;

public class GameMonetize : MonoBehaviourSingleton<GameMonetize>
{
    public string GAME_ID = "YOUR_GAME_ID_HERE";

    public static Action OnResumeGame;
    public static Action OnPauseGame;

    [DllImport("__Internal")]
    private static extern void InitApi(string gameKey);

    [DllImport("__Internal")]
    private static extern void ShowBanner();

    protected void Start()
    {
        Init();
    }

    private void Init()
    {
        DontDestroyOnLoad(this);
        
        try
        {
            InitApi(GAME_ID);
        }
        catch (EntryPointNotFoundException)
        {
            Debug.LogWarning("Initialization failed. Make sure you are running a WebGL build in a browser");
        }
    }

    internal void ShowAd()
    {
        try
        {
            ShowBanner();
        }
        catch (EntryPointNotFoundException)
        {
            Debug.LogWarning("ShowBanner failed. Make sure you are running a WebGL build in a browser");
        }
    }

    /// <summary>
    /// Resume the game, this method is used when an ad has been showed
    /// </summary>
    void ResumeGame()
    {
       if (OnResumeGame != null) OnResumeGame();
    }

    /// <summary>
    /// Pause the game, this method is used when we show an ad
    /// </summary>
    void PauseGame()
    {
        if(OnPauseGame != null) OnPauseGame();
    }
}
