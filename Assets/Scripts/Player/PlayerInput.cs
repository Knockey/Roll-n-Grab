using UnityEngine;

[RequireComponent(typeof(PlayerRollMovement))]
[RequireComponent(typeof(PlayerJump))]
public class PlayerInput : MonoBehaviour
{
    private PlayerRollMovement _movement;
    private PlayerJump _jump;
    private float _horizontalInput;
    private float _verticalInput;

    private void Awake()
    {
        _movement = GetComponent<PlayerRollMovement>();
        _jump = GetComponent<PlayerJump>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis(AxisTyped.Horizontal);
        _verticalInput = Input.GetAxis(AxisTyped.Vertical);

        _movement.Move(new Vector3(_verticalInput, 0, -_horizontalInput));

        if (Input.GetKeyDown(KeyCode.Space))
            _jump.Jump(new Vector3(_verticalInput, 0, -_horizontalInput));
    }
}
