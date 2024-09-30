using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Slider))]
public class RotateBar : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.rotation = Quaternion.identity;
    }
}
