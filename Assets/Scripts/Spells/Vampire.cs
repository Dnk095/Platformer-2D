using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D), typeof(SpellZone))]
public class Vampire : MonoBehaviour
{
    [SerializeField] private SpellsButton _button;
    [SerializeField] private int _healthPerSecond;
    [SerializeField] private int _cooldown = 10;
    [SerializeField] private int _duration = 6;

    private CircleCollider2D _circleCollider;
    private SpellZone _spellZone;
    private Coroutine _coroutine;

    private bool _onCooldown = false;
    private bool _used = false;

    public event Action<int, int, int> ChangeSpellTimer;
    public event Action<int> Healing;
    public event Action Select;
    public event Action DeSelect;

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _spellZone = GetComponent<SpellZone>();
    }

    private void OnEnable()
    {
        _button.OnClick += StealHealth;
        _button.Select += OnSelected;
        _button.DeSelect += OnDeselect;
    }

    private void OnDisable()
    {
        _button.OnClick -= StealHealth;
        _button.Select -= OnSelected;
        _button.DeSelect -= OnDeselect;
    }

    private void StealHealth()
    {
        if (_coroutine != null && _onCooldown == false)
            StopCoroutine(_coroutine);

        if (_onCooldown == false)
            _coroutine = StartCoroutine(StealHitPoint());
    }

    private IEnumerator StealHitPoint()
    {
        Select?.Invoke();
        _onCooldown = true;
        _used = true;

        int delay = 1;
        int currentDuration = _duration;

        Enemy enemy;
        WaitForSeconds wait = new(delay);

        while (currentDuration > 0)
        {
            _circleCollider.enabled = true;
            Select?.Invoke();

            enemy = _spellZone.GetEnemy();

            if (enemy != null)
            {
                StealHealth(enemy);
                enemy = null;
            }

            currentDuration -= delay;

            yield return wait;

            ChangeSpellTimer(currentDuration, _duration, _duration / delay);
        }

        _circleCollider.enabled = false;
        DeSelect?.Invoke();

        if (_coroutine != null)
            yield return null;

        _coroutine = StartCoroutine(Cooldown());
        _used = false;
    }

    private IEnumerator Cooldown()
    {
        DeSelect?.Invoke();

        int delay = 1;
        int currentCooldown = 0;

        WaitForSeconds wait = new(delay);

        while (currentCooldown < _cooldown)
        {
            currentCooldown += delay;

            yield return new WaitForSeconds(delay);

            ChangeSpellTimer(currentCooldown, _cooldown, _cooldown / delay);
        }

        _onCooldown = false;
    }

    private void OnSelected()
    {
        Select?.Invoke();
    }

    private void OnDeselect()
    {
        if (_used == false)
            DeSelect?.Invoke();
    }

    private void StealHealth(Enemy enemy)
    {
        int health = GetStealedHealth(enemy);

        Healing?.Invoke(health);
        enemy.TakeDamage(health);
    }

    private int GetStealedHealth(Enemy enemy)
    {
        int enemyHealth = 0;

        if (enemy.TryGetComponent(out Health health))
            enemyHealth = health.CurrentHealth;

        if (enemyHealth < _healthPerSecond)
            return enemyHealth;
        else
            return _healthPerSecond;
    }
}