using TMPro;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.ChangeHeath += OnChangeHealth;
    }

    private void OnDisable()
    {
        _health.ChangeHeath -= OnChangeHealth;
    }

    private void OnChangeHealth(int currentHealth, int maxHealth)
    {
        _text.text = "HP:" + currentHealth + "/" + maxHealth;
    }
}
