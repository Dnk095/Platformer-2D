using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private LadderMove _ladderMover;
    [SerializeField] private Health _health;
    [SerializeField] private Vampire _vampire;

    public event Action<string> EndGame;

    private void OnEnable()
    {
        _collisionHandler.WinGame += OnWinGame;
        _collisionHandler.GetHeal += OnGetHeal;
        _inputReader.IsAttack += OnIsAttack;
        _vampire.Healing += OnHealing;
        _health.Die += OnDie;
    }

    private void OnDisable()
    {
        _collisionHandler.WinGame -= OnWinGame;
        _collisionHandler.GetHeal -= OnGetHeal;
        _inputReader.IsAttack -= OnIsAttack;
        _vampire.Healing -= OnHealing;
        _health.Die -= OnDie;
    }

    private void Update()
    {
        if (_inputReader.HorizontalDirection != 0)
            _mover.Move(_inputReader.HorizontalDirection);

        if (_inputReader.VerticalDirection != 0 && _ladderMover.IsOnLadder)
            _ladderMover.MoveUp(_inputReader.VerticalDirection);

        if (_inputReader.VerticalDirection > 0 && _groundDetector.IsGround && _ladderMover.IsOnLadder == false)
            _mover.Jump();
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

    private void OnDie(string text)
    {
        EndGame?.Invoke(text);
        _inputReader.enabled = false;
        _playerAnimator.Die();
    }

    private void OnWinGame(string text)
    {
        EndGame?.Invoke(text);
        _inputReader.enabled = false;
        _playerAnimator.Win();
    }

    private void OnGetHeal(int heal)
    {
        _health.Heal(heal);
    }

    private void OnIsAttack()
    {
        _playerAnimator.Attack();
    }

    private void OnHealing(int heal)
    {
        _health.Heal(heal);
    }
}
