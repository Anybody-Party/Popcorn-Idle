using System;
using System.Collections;
using __Scripts.UnityComponents.REDO.SingletonAccess;
using _Scripts.Web;
using Client;
using Leopotam.Ecs;
using UnityEngine;

public class AdsManager : MonoBehaviourSingleton<AdsManager>
{
    [SerializeField] private AudioManager _music;
    [SerializeField] private float _interstitialsCooldown = 60f;

    private Action _turnOnMusic;
    private bool _musicNeedsTurnOn;
    private bool _isInterReady;
    public EcsWorld EcsWorld;

    private void Start()
    {
        _turnOnMusic = TurnOnMusic;
        /*if (_music.Enabled)
        {
            _musicNeedsTurnOn = true;
            _music.AdsToggleAudio(false);
        }*/

        //WebGlBridge.ShowInterstitialAd(gameObject, _turnOnMusic, _turnOnMusic);
        _music.ToggleAudio(true);
        StartCoroutine(ShowInterstitials());
    }
    
    public void ShowRewarded()
    {
        StopAllCoroutines();
        if (_music.Enabled)
        {
            _musicNeedsTurnOn = true;
            _music.AdsToggleAudio(false);
        }

        WebGlBridge.ShowRewardedAd(gameObject, LevelCompleteReward, FailRewarded, OnVideoShowed);
    }

    private void LevelCompleteReward()
    {
        EcsWorld.NewEntity().Get<AdLevelRewardCompleteRequest>();
    }
    
    private void FailRewarded()
    {
        
    }
    
    private void OnVideoShowed()
    {
        TurnOnMusic();
        StartCoroutine(ShowInterstitials());
    }

    private IEnumerator ShowInterstitials()
    {
        var delay = new WaitForSeconds(_interstitialsCooldown);
        while (true)
        {
            yield return delay;
            _isInterReady = true;
        }
    }

    public void ShowInterstitial()
    {
        if (_music.Enabled)
        {
            _musicNeedsTurnOn = true;
            _music.AdsToggleAudio(false);
        }

        WebGlBridge.ShowInterstitialAd(gameObject, _turnOnMusic, _turnOnMusic);
        _isInterReady = false;
        //MaxAdsManager.instance.ShowInter("cooldown_inter");
    }

    private void TurnOnMusic()
    {
        if (_musicNeedsTurnOn)
        {
            _musicNeedsTurnOn = false;
            _music.AdsToggleAudio(true);
        }
    }
}