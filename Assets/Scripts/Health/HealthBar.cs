using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    protected Slider _slider;

    private void OnEnable()
    {
        _health.ChangeHeath += OnChangeHealth;
    }

    private void OnDisable()
    {
        _health.ChangeHeath -= OnChangeHealth;
    }

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    protected virtual void OnChangeHealth(int currentHealth, int maxHealth)
    {
        _slider.value = (float)currentHealth / maxHealth;
    }
}
