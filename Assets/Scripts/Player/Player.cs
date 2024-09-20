using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private LadderMove _ladderMover;
    [SerializeField] private CollisionHandler _collisionHandler;
    [SerializeField] private Health _health;

    public event Action<string> EndGame;

    private void OnEnable()
    {
        _collisionHandler.EndGame += EventReaction;
        _health.Die += EventReaction;
    }

    private void OnDisable()
    {
        _health.Die -= EventReaction;
        _collisionHandler.EndGame -= EventReaction;
    }

    private void FixedUpdate()
    {
        if (_inputReader.HorizontalDirection != 0)
            _mover.Move(_inputReader.HorizontalDirection);

        if (_inputReader.VerticalDirection != 0 && _ladderMover.IsOnLadder)
            _ladderMover.MoveUp(_inputReader.VerticalDirection);

        if (_inputReader.VerticalDirection > 0 && _groundDetector.IsGround && _ladderMover.IsOnLadder == false)
            _mover.Jump();
    }

    private void EventReaction(string text)
    {
        EndGame?.Invoke(text);
        _inputReader.enabled = false;
    }

}
