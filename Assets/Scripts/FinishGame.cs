using TMPro;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField] private CollisionHandler _handler;
    [SerializeField] private TextMeshProUGUI _text;

    private void OnEnable()
    {
        _handler.EndGame += OnEndGame;
    }

    private void OnDisable()
    {
        _handler.EndGame -= OnEndGame;
    }

    private void OnEndGame(string text)
    {
        _text.enabled = true;
        _text.color = Color.red;
        _text.text = "You " + text;
    }
}
