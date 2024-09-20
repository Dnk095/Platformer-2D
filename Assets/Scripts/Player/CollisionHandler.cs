using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private PlayerAnimator _animator;

    private Weapon _weapon;

    public event Action<string> EndGame;
    public event Action<int> Attacked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Monet monet))
        {
            _wallet.AddMoney();
            monet.Destroy();
        }
        else if (collision.TryGetComponent(out Weapon weapon))
        {
            _weapon = weapon;
            _weapon.AttackPlayer += OnAttackPlayer;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Finish _))
        {
            Win();
            EndGame?.Invoke("win");
        }
    }

    private void OnAttackPlayer(int damage)
    {
        Attacked?.Invoke(damage);
        _weapon.AttackPlayer -= OnAttackPlayer;
    }

    private void Win()
    {
        _animator.Win();
    }
}