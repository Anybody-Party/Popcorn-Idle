using System;

[Serializable]
public class RuntimeData : BaseData
{
    public float Temperature;

    public bool HeatButtonPointerDown;

    public override void ResetData()
    {
    }
}