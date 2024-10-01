using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private int _damage;

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.TryGetComponent(out Enemy enemy))
    //        enemy.TakeDamage(_damage);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            enemy.TakeDamage(_damage);
    }
}
