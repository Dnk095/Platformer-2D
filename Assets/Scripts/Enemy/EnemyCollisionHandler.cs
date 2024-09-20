using System;
using UnityEngine;

public class EnemyCollisionHandler : MonoBehaviour
{
    [SerializeField] private EnemyAnimator _enemyAnimator;

    public event Action<bool> Attack;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
        {
            Attack?.Invoke(true);
            _enemyAnimator.Attack();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player _))
        {
            Attack?.Invoke(false);
            _enemyAnimator.StopAttack();
        }
    }
}
