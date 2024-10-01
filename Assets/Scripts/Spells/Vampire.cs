using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(SpellArea))]
public class Vampire : MonoBehaviour
{
    [SerializeField] private SpellsButton _button;
    [SerializeField] private int _healthPerSecond;
    [SerializeField] private int _cooldown = 10;
    [SerializeField] private int _duration = 6;

    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider;

    private SpellArea _area;

    [SerializeField] private Enemy _enemy = null;

    private bool _onCooldown = false;
    private bool _used = false;

    private Coroutine _coroutine;

    public event Action<int> UseSpell;
    public event Action<int, int, int> ChangeSpellTimer;
    public event Action TryGetEnemy;

    private void OnEnable()
    {
        _button.OnClick += GetHealth;
        _button.Select += OnSelected;
        _button.DeSelect += OnDeselect;
        _area.FindEnemy += OnTryGetEnemy;
    }

    private void OnDisable()
    {
        _button.OnClick += GetHealth;
        _button.Select -= OnSelected;
        _button.DeSelect -= OnDeselect;
        _area.FindEnemy -= OnTryGetEnemy;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _area = GetComponent<SpellArea>();
        _circleCollider = GetComponent<CircleCollider2D>();
    }

    private void GetHealth()
    {
        if (_coroutine != null && _onCooldown == false)
            StopCoroutine(_coroutine);

        if (_onCooldown == false)
            _coroutine = StartCoroutine(StiilHitPoint());
    }

    private IEnumerator StiilHitPoint()
    {
        _circleCollider.enabled = true;
        _onCooldown = true;
        _used = true;

        int delay = 1;
        int currentDuretion = _duration;

        WaitForSeconds wait = new(delay);

        while (currentDuretion > 0)
        {
            _spriteRenderer.enabled = true;

            TryGetEnemy?.Invoke();

            if (_enemy != null)
            {
                UseSpell?.Invoke(_healthPerSecond);
                _enemy.TakeDamage(_healthPerSecond);
            }
            else
            {
                Debug.Log("no enemy");
            }

            _enemy = null;

            currentDuretion -= delay;

            yield return wait;

            ChangeSpellTimer(currentDuretion, _duration, _duration / delay);
        }

        _circleCollider.enabled = false;

        if (_coroutine != null)
            yield return null;

        _coroutine = StartCoroutine(Cooldown());
        _used = false;
    }

    private IEnumerator Cooldown()
    {
        _spriteRenderer.enabled = false;

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
        _spriteRenderer.enabled = true;
    }

    private void OnDeselect()
    {
        if (_used == false)
            _spriteRenderer.enabled = false;
    }

    private void OnTryGetEnemy(Enemy enemy)
    {
        _enemy = enemy;
    }
}