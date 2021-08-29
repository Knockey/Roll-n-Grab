using UnityEngine;

public class SurfaceProjection : MonoBehaviour
{
    private Vector3 _surfaceNormal;

    public Vector3 Project(Vector3 forward)
    {
        return Vector3.ProjectOnPlane(forward, _surfaceNormal);
    }

    private void OnCollisionStay(Collision collision)
    {
        _surfaceNormal = collision.contacts[0].normal;
    }
}
