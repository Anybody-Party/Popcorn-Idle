using RH.Utilities.ComponentSystem;
using RH.Utilities.Coroutines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleMoneySystem : BaseSystem
{
    public override void Dispose()
    {
        GlobalEvents.ExampleAddMoney.RemoveListener(ExampleAddMoney);
    }

    protected override void Init()
    {
        GlobalEvents.ExampleAddMoney.AddListener(ExampleAddMoney);
    }

    private void ExampleAddMoney(int _count)
    {
        if (DataManager.Instance.MainData.Money + _count < 0) // NotEnough
        {
            return;
        }

        if (_count > 0 && DataManager.Instance.MainData.Money + _count > DataManager.Instance.BalanceData.MoneyMaxCap) // Max Cap
        {
            DataManager.Instance.MainData.Money = DataManager.Instance.BalanceData.MoneyMaxCap;
            GlobalEvents.ExampleMoneyAdded?.Invoke(_count);
            return;
        }

        DataManager.Instance.MainData.Money += _count;
        GlobalEvents.ExampleMoneyAdded?.Invoke(_count);
    }
}
