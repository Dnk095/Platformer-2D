using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Eyes _eyes;
    [SerializeField] private EnemyMover _enemyMover;
    [SerializeField] private EnemyAnimator _enemyAnimator;
    [SerializeField] private float _attackDistance;
    [SerializeField] private Health _health;


    private void OnEnable()
    {
        _eyes.SeePLayer += OnSee;
    }

    private void OnDisable()
    {
        _eyes.SeePLayer -= OnSee;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnSee(Vector3 position)
    {
        CountDistance(position);
        _enemyMover.ChangeTarget(position);
    }

    private void TryAttack(float currentDistance)
    {
        if (currentDistance <= _attackDistance)
        {
            _enemyMover.ChangeState(true);
            _enemyAnimator.Attack();
        }
        else
        {
            _enemyMover.ChangeState(false);
            _enemyAnimator.StopAttack();
        }
    }

    private void CountDistance(Vector3 target)
    {
        float currentDistance;

        Vector2 currentVector = new(transform.position.x - target.x, transform.position.y - target.y);

        currentDistance = Mathf.Pow(currentVector.x, 2) + Mathf.Pow(currentVector.y, 2);

        TryAttack(currentDistance);
    }
}
