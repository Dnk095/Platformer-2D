using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;

    public event Action<string> WinGame;
    public event Action<int> GetHeal;
    public event Action<Enemy> Attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Monet monet))
        {
            _wallet.AddMoney();
            monet.Destroy();
        }
        else if (collision.TryGetComponent(out AidKit aidkit))
        {
            GetHeal?.Invoke(aidkit.Heal);
            aidkit.Destroy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Finish _))
            WinGame?.Invoke("win");
    }
}