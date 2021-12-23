using UnityEngine;

[RequireComponent(typeof(JumpFX))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _length;
    [SerializeField] private float _duration;
    [SerializeField] private float _minGroundNormal;

    private bool _isGrounded;
    private ProgrammableMovementAnimation _playtime;
    private JumpFX _fx;

    private void Awake()
    {
        _playtime = new ProgrammableMovementAnimation(this);

        _fx = GetComponent<JumpFX>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_minGroundNormal < collision.contacts[0].normal.y)
            _isGrounded = true;
    }

    public void TryJump(Vector3 direction)
    {
        if (_isGrounded)
        {
            Jump(direction);
            _isGrounded = false;
        }
    }

    private void Jump(Vector3 direction)
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = transform.position + (direction * _length);
        ProgrammableMovementAnimation fxPlaytime = _fx.PlayAnimations(transform, _duration);

        _playtime.Play(_duration, (progress) =>
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, progress) + fxPlaytime.LastChanges.Position;

            return null;
        });
    }
}
