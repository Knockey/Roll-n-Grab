using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _score;

    public float Score => _score;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerScore player))
        {
            Destroy(gameObject);
        }
    }
}
