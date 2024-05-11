#if CRAZY_GAMES
using CrazyGames;
using UnityEngine;

public class CrazyBannerActivator : MonoBehaviour
{
    [SerializeField] private CrazyBanner _crazyBanner;

    private void Start()
    {
        ShowBanner();
    }

    public void ShowBanner()
    {
        _crazyBanner.MarkVisible(true);
        CrazyAds.Instance.updateBannersDisplay();
    }

    public void HideBanner()
    {
        _crazyBanner.MarkVisible(false);
        CrazyAds.Instance.updateBannersDisplay();
    }
}
#endif