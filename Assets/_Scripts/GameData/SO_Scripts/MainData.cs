using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MainData", menuName = "GameData/MainData")]
public class MainData : BaseData
{
    public int LevelNumber;
    public int Money;

    public override void ResetData()
    {
        
    }
}