using UnityEngine;

public class JumpFX : MonoBehaviour
{
    [SerializeField] private AnimationCurve _yAnimation;
    [SerializeField] private float _height;
    [SerializeField] private float _sphereCastOffset;
    [SerializeField] private LayerMask _jumpLayerMask;

    private ProgrammableMovementAnimation _playtime;

    private void Awake()
    {
        _playtime = new ProgrammableMovementAnimation(this);
    }

    public ProgrammableMovementAnimation PlayAnimations(Transform jumper, float duration)
    {
        _playtime.Play(duration, (float progress) =>
        {
            Vector3 position = Vector3.Scale(new Vector3(0, _height * _yAnimation.Evaluate(progress), 0), jumper.up);

            return new TransformChanges(position);
        });

        return _playtime;
    }
}
