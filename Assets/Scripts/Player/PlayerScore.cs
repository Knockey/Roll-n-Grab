using System;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    private float _score;

    public event Action<float> CoinCollected;
    public event Action<GridObject> ObjectPicked;

    private void Awake()
    {
        _score = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Coin coin) && collision.gameObject.TryGetComponent(out GridObject gridObject))
        {
            _score += coin.Score;
            CoinCollected?.Invoke(_score);
            ObjectPicked?.Invoke(gridObject);
        }
    }
}