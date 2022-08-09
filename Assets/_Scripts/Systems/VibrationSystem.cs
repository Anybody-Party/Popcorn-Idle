using RH.Utilities.ComponentSystem;
using UnityEngine;


public class VibrationSystem : BaseSystem
{
    protected override void Init()
    {
        UIEvents.VibrationButtonTap.AddListener(SetVibrationState);
    }

    public override void Dispose()
    {
        UIEvents.VibrationButtonTap.RemoveListener(SetVibrationState);
    }

    private void SetVibrationState()
    {
        DataManager.Instance.SettingsData.IsVibrationOn = !DataManager.Instance.SettingsData.IsVibrationOn;
        Handheld.Vibrate();
    }
}
