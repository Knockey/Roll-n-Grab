using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerRollMovement _player;
    [SerializeField] private Vector3 _offset;

    private void OnEnable()
    {
        _player.PlayerMoved += OnPlayerMoved;
    }

    private void OnDisable()
    {
        _player.PlayerMoved += OnPlayerMoved;
    }

    private void OnPlayerMoved()
    {
        transform.position = _offset + new Vector3(_player.transform.position.x, 0, _player.transform.position.z);
    }
}
