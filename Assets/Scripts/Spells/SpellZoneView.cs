using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class SpellZoneView : MonoBehaviour
{
    [SerializeField] private Vampire _vampire;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _vampire.Select += OnSelect;
        _vampire.DeSelect += OnDeSelect;
    }

    private void OnDisable()
    {
        _vampire.Select -= OnDeSelect;
        _vampire.DeSelect -= OnDeSelect;
    }

    private void OnSelect()
    {
        _spriteRenderer.enabled = true;
    }

    private void OnDeSelect()
    {
        _spriteRenderer.enabled = false;
    }
}