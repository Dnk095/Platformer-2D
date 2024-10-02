using System.Collections.Generic;
using UnityEngine;

public class SpellZone : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            _enemies.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            _enemies.Remove(enemy);
    }

    public Enemy GetEnemy()
    {
        Enemy nearestEnemy = null;

        if (_enemies.Count > 0)
        {
            foreach (Enemy enemy in _enemies)
            {
                if (nearestEnemy == null)
                {
                    nearestEnemy = enemy;
                }
                else if (GetMagnitude(enemy.transform) < GetMagnitude(nearestEnemy.transform))
                {
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }

    private float GetMagnitude(Transform target)
    {
        return (target.position - transform.position).sqrMagnitude;
    }
}
