#if !UNITY_EDITOR
using System.Collections;
using _Scripts.Web;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Web.Scripts
{
    public class WebGLBoot : MonoBehaviour
    {
        [SerializeField] private string _sceneToLoad;

#if UNITY_EDITOR

        private void Start()
        {
            SceneManager.LoadSceneAsync(_sceneToLoad);
        }

#else
    private IEnumerator Start()
    {
        yield return new WaitUntil(WebGlBridge.Inited);
        SceneManager.LoadSceneAsync(_sceneToLoad);
    }
#endif
    }
}