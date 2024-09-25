using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private InputReader _inputReader;

    private int IsWalk = Animator.StringToHash(nameof(IsWalk));
    private int IsDie = Animator.StringToHash(nameof(IsDie));
    private int IsWin = Animator.StringToHash(nameof(IsWin));
    private int IsAttack = Animator.StringToHash(nameof(IsAttack));

    private void FixedUpdate()
    {
        if (_inputReader.HorizontalDirection == 0)
            _animator.SetBool(IsWalk, false);
        else if (_inputReader.HorizontalDirection != 0)
            _animator.SetBool(IsWalk, true);
    }

    public void Die()
    {
        _animator.SetBool(IsDie, true);
    }

    public void Win()
    {
        _animator.SetBool(IsWin, true);
    }

    public void Attack()
    {
        _animator.SetBool(IsAttack, true);
    }
}
