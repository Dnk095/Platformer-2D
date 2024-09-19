using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    private int _money = 0;

    public event Action<int> ChangeMoney;

    public void AddMoney()
    {
        _money+=1;
        ChangeMoney?.Invoke(_money);
    }
}
