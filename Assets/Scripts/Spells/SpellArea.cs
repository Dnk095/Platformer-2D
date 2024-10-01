using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Vampire))]
public class SpellArea : MonoBehaviour
{
    [SerializeField] private List<Enemy> _enemy;
    private Vampire _vampire;
    private Enemy _nearestEnemy = null;

    public event Action<Enemy> FindEnemy;

    private void OnEnable()
    {
        _vampire.TryGetEnemy += OnUseSpell;
    }

    private void OnDisable()
    {
        _vampire.TryGetEnemy -= OnUseSpell;
    }

    private void Awake()
    {
        _vampire = GetComponent<Vampire>();
    }

    private void OnUseSpell()
    {
        if (_enemy.Count > 0)
            FindNearestEnemy();

        FindEnemy?.Invoke(_nearestEnemy);
        _nearestEnemy = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            _enemy.Add(enemy);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
            _enemy.Remove(enemy);
    }

    private void FindNearestEnemy()
    {
        foreach (Enemy enemy in _enemy)
        {
            if (_nearestEnemy == null)
                _nearestEnemy = enemy;
            else if (GetDistance(enemy.transform) < GetDistance(_nearestEnemy.transform))
                _nearestEnemy = enemy;
        }
    }

    private float GetDistance(Transform target)
    {
        return Vector2.Distance(transform.position, target.position);
    }
}
