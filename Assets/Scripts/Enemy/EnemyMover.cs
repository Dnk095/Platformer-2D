using UnityEngine;

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform[] _targetPoint;
    [SerializeField] private EnemyCollisionHandler _collisionHandler;
    [SerializeField] private Eyes _eyes;
    [SerializeField] private float _speed;

    private Vector3 _playerTransform;

    private int _currentPoint = 0;

    private bool _isAttack;
    private bool _seePlayer;

    private void OnEnable()
    {
        _collisionHandler.Attack += OnAttack;
        _eyes.SeePLayer += OnSee;
    }

    private void OnDisable()
    {
        _collisionHandler.Attack -= OnAttack;
        _eyes.SeePLayer -= OnSee;
    }

    private void FixedUpdate()
    {
        if (transform.position == _targetPoint[_currentPoint].position)
        {
            _currentPoint = ++_currentPoint % _targetPoint.Length;
            transform.Rotate(0, 180, 0, Space.World);
        }

        if (_isAttack == false && _seePlayer == false)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                _targetPoint[_currentPoint].position, _speed * Time.fixedDeltaTime);
        }
        else if (_seePlayer == true && _isAttack == false)
        {
            transform.position = Vector3.MoveTowards(transform.position,
               _playerTransform, _speed * Time.fixedDeltaTime);

            if (transform.position == _playerTransform)
            {
                _seePlayer = false;
            }
        }
    }

    private void OnAttack(bool attack)
    {
        _isAttack = attack;
    }

    private void OnSee(Vector3 position)
    {
        _playerTransform = position;
        _seePlayer = true;
    }
}
