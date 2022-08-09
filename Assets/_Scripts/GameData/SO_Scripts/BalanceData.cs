using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BalanceData", menuName = "GameData/BalanceData")]
public class BalanceData : BaseData
{
    public int MoneyMaxCap;

    public override void ResetData()
    {
        throw new System.NotImplementedException();
    }
}
