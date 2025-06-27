using UnityEngine;

namespace LIM_4087.Scripts
{
    public class UvScroller : MonoBehaviour
    {
        public Material targetMaterial;
        public float speedX = 0f;
        public float speedY = 0f;

        private Vector2 _offset;
        private Vector2 _initOffset;

        private void Start()
        {
            _offset = targetMaterial.mainTextureOffset;
            _initOffset = targetMaterial.mainTextureOffset;
        }

        private void OnDisable()
        {
            targetMaterial.mainTextureOffset = _initOffset;
        }

        private void Update()
        {
            _offset.x += speedX * Time.deltaTime;
            _offset.y += speedY * Time.deltaTime;
            targetMaterial.mainTextureOffset = _offset;
        }
    }
}