using UnityEngine;

public class TransformChanges
{
    private Vector3 _position;

    public Vector3 Position => _position;

    public TransformChanges(Vector3 position)
    {
        _position = position;
    }
}
