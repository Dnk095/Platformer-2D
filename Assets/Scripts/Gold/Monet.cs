using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Monet : MonoBehaviour
{
    public event Action PickUp;

    public void Destroy()
    {
        Destroy(gameObject);
        PickUp?.Invoke();
    }
}
