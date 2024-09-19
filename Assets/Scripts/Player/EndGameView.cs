using TMPro;
using UnityEngine;

public class EndGameView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;

    public void DrawEndText(string text)
    {
        _text.enabled = true;
        _text.text = text;
    }
}
