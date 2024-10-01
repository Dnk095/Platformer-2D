using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SpellsButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Button _button;

    public event Action Select;
    public event Action DeSelect;
    public event Action OnClick;

    private void OnEnable()
    {
        _button.onClick.AddListener(Push);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Push);
    }

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Select?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DeSelect?.Invoke();
    }

    private void Push()
    {
        OnClick?.Invoke();
    }
}
