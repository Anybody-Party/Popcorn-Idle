using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Web
{
    [RequireComponent(typeof(Button))]
    public class JoinGroupOffer : MonoBehaviour
    {
#if VK
        [SerializeField] private int _groupId;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(JoinGroup);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(JoinGroup);
        }

        private void JoinGroup()
        {
            WebGlBridge.JoinGroup(_groupId);
        }
#endif
    }
}