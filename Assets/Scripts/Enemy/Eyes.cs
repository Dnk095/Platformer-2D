using System;
using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class Eyes : MonoBehaviour
{
    public event Action<Vector3> SeePLayer;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            Vector3 currentTarget = player.transform.position;
            SeePLayer?.Invoke(new Vector3(currentTarget.x, currentTarget.y, currentTarget.z));
        }
    }
}
