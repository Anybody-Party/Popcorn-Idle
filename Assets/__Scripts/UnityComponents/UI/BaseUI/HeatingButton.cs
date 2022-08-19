using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HeatingButton : ActionButton, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private ParticleSystem firePS;
    [SerializeField] private ParticleSystem smokePS;

    [HideInInspector] public UnityEvent<bool> OnChangePressState;

    private bool IsPressed;

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        OnChangePressState.Invoke(IsPressed);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        IsPressed = false;
        OnChangePressState.Invoke(IsPressed);
    }
}
