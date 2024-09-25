using TMPro;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Player _player;

    private void OnEnable()
    {
        _player.EndGame += OnEndGame;
    }

    private void OnDisable()
    {
        _player.EndGame -= OnEndGame;
    }

    private void OnEndGame(string text)
    {
        _text.enabled = true;
        _text.color = Color.red;
        _text.text = "You " + text;
    }
}