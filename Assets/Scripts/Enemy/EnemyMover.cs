using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform[] _targetPoint;
    [SerializeField] private float _speed;

    private Vector3 _targetPosition;

    private int _currentPoint = 0;
    private bool _moveToPLayer = false;
    private bool _isAttack = false;

    private void FixedUpdate()
    {
        if (transform.position == _targetPoint[_currentPoint].position)
        {
            _currentPoint = ++_currentPoint % _targetPoint.Length;
            transform.Rotate(0, 180, 0, Space.World);
        }
        else if (transform.position == _targetPosition)
        {
            _moveToPLayer = false;
        }

        if (_moveToPLayer == false && _isAttack == false)
            transform.position = Vector3.MoveTowards(transform.position,
                   _targetPoint[_currentPoint].position, _speed * Time.fixedDeltaTime);
        else if (_moveToPLayer == true && _isAttack == false)
            transform.position = Vector3.MoveTowards(transform.position,
                  _targetPosition, _speed * Time.fixedDeltaTime);
    }

    public void ChangeTarget(Vector3 position)
    {
        _targetPosition = new Vector2(position.x, transform.position.y);
        _moveToPLayer = true;
    }

    public void ChangeState(bool isAttack)
    {
        _isAttack = isAttack;
    }
}
