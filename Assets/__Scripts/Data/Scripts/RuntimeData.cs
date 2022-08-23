using System;

[Serializable]
public class RuntimeData : BaseData
{
    public float Temperature;

    public bool IsHeatButtonPressed;
    public bool IsTapSpeedUpWorking;

    public override void ResetData()
    {
    }
}