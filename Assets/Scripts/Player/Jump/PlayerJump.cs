using UnityEngine;

[RequireComponent(typeof(JumpFX))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SurfaceProjection))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float _length;
    [SerializeField] private float _duration;

    private ProgrammableMovementAnimation _playtime;
    private JumpFX _fx;
    private Rigidbody _rigidbody;
    private SurfaceProjection _surfaceProjection;

    private void Awake()
    {
        _playtime = new ProgrammableMovementAnimation(this);

        _fx = GetComponent<JumpFX>();
        _rigidbody = GetComponent<Rigidbody>();
        _surfaceProjection = GetComponent<SurfaceProjection>();
    }

    public void Jump(Vector3 direction)
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
