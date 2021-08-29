using UnityEngine;

public class JumpFX : MonoBehaviour
{
    [SerializeField] private AnimationCurve _yAnimation;
    [SerializeField] private float _height;
    [SerializeField] private float _sphereCastOffset;
    [SerializeField] private LayerMask _jumpLayerMask;

    private ProgrammableMovementAnimation _playtime;
    private SphereCollider _collider;

    private void Awake()
    {
        _playtime = new ProgrammableMovementAnimation(this);
        _collider = GetComponent<SphereCollider>();
    }

    public ProgrammableMovementAnimation PlayAnimations(Transform jumper, float duration)
    {
        _playtime.Play(duration, (float progress) =>
        {
            Vector3 position = Vector3.Scale(new Vector3(0, _height * _yAnimation.Evaluate(progress), 0), jumper.up);

            /*if (Physics.SphereCast(transform.position, _collider.radius + _sphereCastOffset, transform.up * -1f, out RaycastHit hit, _jumpLayerMask))
            {
                Debug.Log("А всее");
                position.y = hit.transform.position.y;
                return new TransformChanges(position);
            }
            */
            return new TransformChanges(position);
        });

        return _playtime;
    }
}
