using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class LadderMove : MonoBehaviour
{
    [SerializeField] private Vector3 _direction;

    private bool _isOnLadder;

    private Rigidbody2D _rigidbody;

    public bool IsOnLadder => _isOnLadder;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Ladder>(out _))
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = new Vector2(0, 0);
            _isOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Ladder>(out _))
        {
            _rigidbody.gravityScale = 1;
            _isOnLadder = false;
        }
    }

    public void MoveUp(float directional)
    {
        if (directional < 0)
            transform.Translate(-_direction * Time.deltaTime);
        else if (directional > 0)
            transform.Translate(_direction * Time.deltaTime);
    }
}