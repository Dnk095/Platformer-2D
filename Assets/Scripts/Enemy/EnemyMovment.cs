using UnityEngine;

public class EnemyMovment : MonoBehaviour
{
    [SerializeField] private Transform[] _targetPoint;
    [SerializeField] private float _speed;

    private int _currentPoint=0;

    private void FixedUpdate()
    {
        if (transform.position == _targetPoint[_currentPoint].position)
        {
            _currentPoint = ++_currentPoint%_targetPoint.Length;
            transform.Rotate(0, 180, 0,Space.World);
        }

        transform.position = Vector3.MoveTowards(transform.position, _targetPoint[_currentPoint].position, _speed * Time.fixedDeltaTime);
    }
}
