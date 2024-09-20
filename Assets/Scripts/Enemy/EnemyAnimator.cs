using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    private int IsAttack = Animator.StringToHash(nameof(IsAttack));

    [SerializeField] private Animator _enemyAnimator;

    public void Attack()
    {
        _enemyAnimator.SetBool(IsAttack, true);
    }

    public void StopAttack()
    {
        _enemyAnimator.SetBool(IsAttack, false);
    }
}
