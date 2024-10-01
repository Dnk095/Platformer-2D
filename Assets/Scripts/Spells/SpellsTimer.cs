using UnityEngine;
using UnityEngine.UI;

public class SpellsTimer : MonoBehaviour
{
    [SerializeField] private Slider _timer;
    [SerializeField] private Vampire _vampire;
    [SerializeField] private float _step;

    private void OnEnable()
    {
        _vampire.ChangeSpellTimer += OnCHangeSpellTimer;
    }

    private void OnDisable()
    {
        _vampire.ChangeSpellTimer -= OnCHangeSpellTimer;
    }

    private void OnCHangeSpellTimer(int curretnDuration, int maxDuration, int quantitySteps)
    {
        _timer.value = Mathf.MoveTowards(_timer.value, (float)curretnDuration / maxDuration, quantitySteps );
    }
}
