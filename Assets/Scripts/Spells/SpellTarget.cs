using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vampire))]
public class SpellTarget : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemies;

    private Vampire _vampire;
    private Enemy _nearestEnemy = null;

    public event Action<Enemy> EnemyFound;

    private void OnEnable()
    {
        _vampire.GetEnemy += OnUseSpell;
    }

    private void OnDisable()
    {
        _vampire.GetEnemy -= OnUseSpell;
    }

    private void Awake()
    {
        _vampire = GetComponent<Vampire>();
    }

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

    private void OnUseSpell()
    {
        if (_enemies.Count > 0)
            FindNearestEnemy();

        EnemyFound?.Invoke(_nearestEnemy);
        _nearestEnemy = null;
    }

    private void FindNearestEnemy()
    {
        foreach (Enemy enemy in _enemies)
        {
            if (_nearestEnemy == null)
                _nearestEnemy = enemy;
            else if (GetMagnitude(enemy.transform) < GetMagnitude(_nearestEnemy.transform))
                _nearestEnemy = enemy;
        }
    }

    private float GetMagnitude(Transform target)
    {
        return (target.position - transform.position).sqrMagnitude;
    }
}
