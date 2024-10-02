using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class SpleeAreaView : MonoBehaviour
{
    [SerializeField] private Vampire _vampire;

    private SpriteRenderer _spriteRenderer;

    private void OnEnable()
    {
        _vampire.Select += OnSelect;
        _vampire.DeSelect += OnDeselect;
    }

    private void OnDisable()
    {
        _vampire.Select -= OnDeselect;
        _vampire.DeSelect -= OnDeselect;
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnSelect()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnDeselect()
    {
        _spriteRenderer.enabled = false;
    }
}
