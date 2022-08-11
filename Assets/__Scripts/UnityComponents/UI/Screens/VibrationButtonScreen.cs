using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

public class VibrationButtonScreen : BaseScreen
{
    [SerializeField] private ActionButton vibrationButton;
    [SerializeField] private Sprite vibrationOnSprite;
    [SerializeField] private Sprite vibrationOffSprite;
    [SerializeField] private Image vibractionStateImage;

    private void Start()
    {
        vibrationButton.OnClickEvent.AddListener(() =>
        EcsWorld.NewEntity()
        .Get<SetVibrationStateEvent>());

        vibrationButton.OnClickEvent.AddListener(UpdateButtonViev);
    }

    private void UpdateButtonViev()
    {
        vibractionStateImage.sprite = GameData.Instance.StaticData.IsVibrationOn ? vibrationOnSprite : vibrationOffSprite;
    }
}