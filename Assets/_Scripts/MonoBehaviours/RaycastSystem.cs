using UnityEngine;

public class RaycastSystem : MonoBehaviour
{
    private Camera _camera;
    private int _layerMask;

    private void Start()
    {
        _camera = Camera.main;
        _layerMask = 1 << LayerMask.NameToLayer("Default") | 1 << LayerMask.NameToLayer("Key");
    }

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Tap();
    }

    private void Tap()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out var hit, 50f, _layerMask))
        //    GlobalEvents.TapIntent?.Invoke(hit.collider.gameObject, hit.point);
    }

}
