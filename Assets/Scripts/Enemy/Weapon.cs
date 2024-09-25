using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Weapon : MonoBehaviour
{
    [SerializeField] private int _damage;

    public event Action<int> AttackPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
            player.TakeDamage(_damage);
    }
}
