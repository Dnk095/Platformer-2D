using TMPro;
using UnityEngine;

public class WalletView : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _wallet.ChangeMoney += OnChangeMoney;
    }

    private void OnDisable()
    {
        _wallet.ChangeMoney -= OnChangeMoney;
    }

    private void OnChangeMoney(int money)
    {
        _text.text = "Money :" + money;
    }
}
