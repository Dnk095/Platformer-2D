using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SmothHealthBar : HealthBar
{
    [SerializeField] private float _barStep;

    private Coroutine _coroutine;

    protected override void OnChangeHealth(int currentHealth, int maxHealth)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);

        _coroutine = StartCoroutine(SmothDraw(currentHealth, maxHealth));
    }

    private IEnumerator SmothDraw(int currentHealth, int maxHealth)
    {
        float health = (float)currentHealth / maxHealth;

        while (_slider.value != health)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, health, _barStep * Time.deltaTime);
            yield return null;
        }
    }
}