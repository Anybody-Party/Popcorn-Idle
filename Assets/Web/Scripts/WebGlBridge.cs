using System;
using System.Runtime.InteropServices;
using __Scripts.UnityComponents.REDO.SingletonAccess;
using UnityEngine;

namespace _Scripts.Web
{
    public class WebGlBridge : MonoBehaviourSingleton<WebGlBridge>
    {
        public static void ShowInterstitialAd(GameObject callerGameObject,
            Action closeFunction = null, Action errorFunction = null)
        {
#if UNITY_WEBGL && !UNITY_EDITOR && (YANDEX || VK || GAME_PIX)
            ShowInter(callerGameObject.name, closeFunction?.Method.Name, errorFunction?.Method.Name);
#elif CRAZY_GAMES
            CrazyGames.CrazyAds.Instance.beginAdBreak(() => closeFunction?.Invoke(), () => errorFunction?.Invoke());
#elif GAME_DISTRIBUTION
            GameDistribution.Instance.ShowAd();
            print("Inter showed");
#elif GAME_MONETIZE
            GameMonetize.Instance.ShowAd();
            print("Inter showed");
#else
            closeFunction?.Invoke();
            print("Inter showed");
#endif
        }

        public static void ShowRewardedAd(GameObject callerGameObject, Action successFunction,
            Action failFunction, Action closeFunction)
        {
#if UNITY_WEBGL && !UNITY_EDITOR && (YANDEX || VK || GAME_PIX)
            ShowVideo(callerGameObject.name, successFunction?.Method.Name,
                failFunction?.Method.Name, closeFunction?.Method.Name);
#elif UNITY_WEBGL && !UNITY_EDITOR && GAME_DISTRIBUTION
            GameDistribution.OnRewardedVideoSuccess += OnVideoSuccess;
            GameDistribution.OnRewardedVideoFailure += OnVideoFailure;
            GameDistribution.Instance.ShowRewardedAd();

            void OnVideoSuccess()
            {
                OnRewardedVideoSuccess(successFunction, closeFunction);
                GameDistribution.OnRewardedVideoSuccess -= OnVideoSuccess;
            }

            void OnVideoFailure()
            {
                OnRewardedVideoFail(failFunction, closeFunction);
                GameDistribution.OnRewardedVideoFailure -= OnVideoFailure;
            }
#elif CRAZY_GAMES
            CrazyGames.CrazyAds.Instance.beginAdBreakRewarded(() => successFunction?.Invoke(),
                () => failFunction?.Invoke());
#elif GAME_MONETIZE
            void OnSuccess()
            {
                OnRewardedVideoSuccess(successFunction, closeFunction);
                GameMonetize.OnResumeGame -= OnSuccess;
            }
            
            GameMonetize.OnResumeGame += OnSuccess;
            GameMonetize.Instance.ShowAd();
#else
            OnRewardedVideoSuccess(successFunction, closeFunction);
#endif
        }
        
        [DllImport("__Internal")]
        public static extern bool Inited();

        [DllImport("__Internal")]
        public static extern string AddToFavorites();

        [DllImport("__Internal")]
        public static extern string JoinGroup(int groupId);

        [DllImport("__Internal")]
        public static extern string Recommend();

        [DllImport("__Internal")]
        private static extern string ShowInter(string objectName,
            string onCloseFunctionName,
            string onErrorFunctionName);

        [DllImport("__Internal")]
        private static extern string ShowVideo(string objectName, string onSuccessFunctionName,
            string onFailFunctionName, string onCloseFunctionName);

        private static void OnRewardedVideoSuccess(Action successFunction, Action closeFunction)
        {
            successFunction?.Invoke();
            closeFunction?.Invoke();
        }

        private static void OnRewardedVideoFail(Action failFunction, Action closeFunction)
        {
            failFunction?.Invoke();
            closeFunction?.Invoke();
        }
    }
}