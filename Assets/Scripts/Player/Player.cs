using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private LadderMove _ladderMover;
    [SerializeField] private Wallet _wallet;
    [SerializeField] private EndGameView _textView;
    [SerializeField] private PlayerAnimator _animator;

    private void FixedUpdate()
    {
        if (_inputReader.HorizontalDirection != 0)
            _mover.Move(_inputReader.HorizontalDirection);

        if (_inputReader.VerticalDirection != 0 && _ladderMover.IsOnLadder)
            _ladderMover.MoveUp(_inputReader.VerticalDirection);

        if (_inputReader.VerticalDirection > 0 && _groundDetector.IsGround && _ladderMover.IsOnLadder == false)
            _mover.Jump();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Monet monet))
        {
            _wallet.AddMoney();
            monet.Destroy();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.TryGetComponent(out Finish _))
        {
            Win();
        }
        else if (collisionObject.TryGetComponent(out Enemy _))
        {
            Die();
        }
    }

    private void Die()
    {
        string die = "YOU DIE";

        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        _animator.Die();
        _inputReader.enabled = false;
        _textView.DrawEndText(die);
    }

    private void Win()
    {
        string win = "YOU WIN";

        _animator.Win();
        _inputReader.enabled = false;
        _textView.DrawEndText(win);
    }
}
