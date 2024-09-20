using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private PlayerAnimator _playerAnimator;

    private int _maxHeath = 100;
    private int _currentHealth = 100;

    public event Action<int, int> ChangeHeath;
    public event Action<string> Die;

    private void Awake()
    {
        ChangeHeath?.Invoke(_currentHealth, _maxHeath);
    }

    private void OnEnable()
    {
        _collisionHandler.Attacked += Change;
    }

    private void OnDisable()
    {
        _collisionHandler.Attacked -= Change;
    }

    public void Change(int damage)
    {
        _currentHealth -= damage;

        ChangeHeath?.Invoke(_currentHealth, _maxHeath);

        if (_currentHealth <= 0)
        {
            Die?.Invoke("die");
            _playerAnimator.Die();
            _collisionHandler.Attacked-=Change;
        }
    }
}
