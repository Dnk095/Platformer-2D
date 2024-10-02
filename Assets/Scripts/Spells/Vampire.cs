using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(StealHealth))]
[RequireComponent(typeof(SpellTarget))]
public class Vampire : MonoBehaviour
{
    [SerializeField] private SpellsButton _button;
    [SerializeField] private int _healthPerSecond;
    [SerializeField] private int _cooldown = 10;
    [SerializeField] private int _duration = 6;

    private CircleCollider2D _circleCollider;
    private SpellTarget _area;
    private Enemy _enemy = null;
    private Coroutine _coroutine;
    private StealHealth _stealHealth;

    private bool _onCooldown = false;
    private bool _used = false;


    public event Action<Enemy> UseSpell;
    public event Action<int, int, int> ChangeSpellTimer;
    public event Action GetEnemy;
    public event Action Select;
    public event Action DeSelect;
    public event Action<int> Healing;

    private void OnEnable()
    {
        _button.OnClick += StealHealth;
        _button.Select += OnSelected;
        _button.DeSelect += OnDeselect;
        _area.EnemyFound += OnTryGetEnemy;
        _stealHealth.StealedHealth += OnStealedHealth;
    }

    private void OnDisable()
    {
        _button.OnClick -= StealHealth;
        _button.Select -= OnSelected;
        _button.DeSelect -= OnDeselect;
        _area.EnemyFound -= OnTryGetEnemy;
        _stealHealth.StealedHealth -= OnStealedHealth;
    }

    private void Awake()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
        _area = GetComponent<SpellTarget>();
        _stealHealth = GetComponent<StealHealth>();
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
        int currentDuretion = _duration;

        WaitForSeconds wait = new(delay);

        while (currentDuretion > 0)
        {
            _circleCollider.enabled = true;
            Select?.Invoke();

            GetEnemy?.Invoke();

            if (_enemy != null)
            {
                UseSpell?.Invoke(_enemy);
            }

            _enemy = null;

            currentDuretion -= delay;

            yield return wait;

            ChangeSpellTimer(currentDuretion, _duration, _duration / delay);
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

    private void OnTryGetEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }

    private void OnStealedHealth(int health)
    {
        Healing?.Invoke(health);
        _enemy.TakeDamage(health);
    }
}