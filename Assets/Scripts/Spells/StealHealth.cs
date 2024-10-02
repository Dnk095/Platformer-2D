using System;
using UnityEngine;

[RequireComponent(typeof(Vampire))]
public class StealHealth : MonoBehaviour
{
    [SerializeField] private int _healthPerSecond;

    private Vampire _vampire;

    public event Action<int> StealedHealth;

    private void OnEnable()
    {
        _vampire.UseSpell += OnUseSpeel;
    }

    private void OnDisable()
    {
        _vampire.UseSpell -= OnUseSpeel;
    }

    private void Awake()
    {
        _vampire = GetComponent<Vampire>();
    }

    private void OnUseSpeel(Enemy enemy)
    {
        int enemyHealth = enemy.GetComponent<Health>().CurrentHealth;

        if (enemyHealth <= 0)
            return;

        if (enemyHealth < _healthPerSecond)
            StealedHealth?.Invoke(enemyHealth);
        else
            StealedHealth?.Invoke(_healthPerSecond);
    }
}
