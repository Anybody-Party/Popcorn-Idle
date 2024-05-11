using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Web
{
    [RequireComponent(typeof(Button))]
    public class RecommendOffer : MonoBehaviour
    {
#if VK
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Recommend);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Recommend);
        }

        private static void Recommend()
        {
            WebGlBridge.Recommend();
        }
#endif
    }
}