using IJunior.TypedScenes;
using UnityEngine;

public class ObstacleHit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerInput player))
        {
            Destroy(player);
            Game.Load();
        }
    }
}
