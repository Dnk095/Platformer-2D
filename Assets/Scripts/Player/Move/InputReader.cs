using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    public float VerticalDirection { get; private set; }
    public float HorizontalDirection { get; private set; }

    public event Action IsAttack;

    private void Update()
    {
        HorizontalDirection = Input.GetAxis(Horizontal);
        VerticalDirection = Input.GetAxis(Vertical);

        if (Input.GetMouseButtonDown(0))
            IsAttack?.Invoke();
    }
}
