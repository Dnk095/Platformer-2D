using UnityEngine;

[RequireComponent(typeof(PolygonCollider2D))]
public class AidKit : MonoBehaviour
{
    private int _heal = 30;

    public int Heal => _heal;

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
