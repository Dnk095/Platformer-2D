using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    private int _maxHeath = 100;
    private int _currentHealth = 100;
    private int _minHealth = 0;

    public event Action<int, int> ChangeHeath;
    public event Action<string> Die;

    private void Start()
    {
        ChangeHeath?.Invoke(_currentHealth, _maxHeath);
    }

    public void TakeDamage(int damage)
    {
        if (damage > 0)
        {
            _currentHealth = Mathf.Clamp(_currentHealth - damage, _minHealth, _maxHeath);
            ChangeHeath?.Invoke(_currentHealth, _maxHeath);
        }

        if (_currentHealth <= 0)
            Die?.Invoke("die");
    }

    public void Heal(int heal)
    {
        if (heal > 0)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + heal, _minHealth, _maxHeath);
            ChangeHeath?.Invoke(_currentHealth, _maxHeath);
        }
    }
}