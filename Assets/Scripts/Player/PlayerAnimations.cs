using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    [SerializeField] private PlayerInput _player;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _player.PlayerRolled += OnPlayerRolled;
        _player.PlayerStoped += OnPlayerStoped;
    }

    private void OnDisable()
    {
        _player.PlayerRolled -= OnPlayerRolled;
        _player.PlayerStoped -= OnPlayerStoped;
    }

    private void OnPlayerRolled()
    {
        _animator.Play(PlayerTypedAnimation.Roll);
    }

    private void OnPlayerStoped()
    {
        _animator.Play(PlayerTypedAnimation.Idle);
    }
}
