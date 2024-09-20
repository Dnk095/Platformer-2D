using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _speed;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0);
        _rigidbody.AddForce(new Vector2(0, _jumpForce));
    }

    public void Move(float inputDirectional)
    {
        if (inputDirectional < 0)
            transform.rotation = Quaternion.Euler(0, 0, 0);
        else
            transform.rotation = Quaternion.Euler(0, 180, 0);

        _rigidbody.velocity = new Vector2(inputDirectional * _speed, _rigidbody.velocity.y);
    }
}
