using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _money = 0;

    public event Action<int> ChangeMoney;

    public void AddMoney()
    {
        int additionalMoney = 1;

        _money += additionalMoney;
        ChangeMoney?.Invoke(_money);
    }
}
