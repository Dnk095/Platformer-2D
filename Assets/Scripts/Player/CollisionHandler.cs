using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private InputReader _inputReader;

    public event Action<string> EndGame;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Monet monet))
        {
            _wallet.AddMoney();
            monet.Destroy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.TryGetComponent(out Finish _))
        {
            Win();
            EndGame?.Invoke("win");
        }
        else if (collisionObject.TryGetComponent(out Enemy _))
        {
            EndGame?.Invoke("die");
            Die();
        }
    }

    private void Die()
    {
        _animator.Die();
        _inputReader.enabled = false;
    }

    private void Win()
    {
        _animator.Win();
        _inputReader.enabled = false;
    }
}
