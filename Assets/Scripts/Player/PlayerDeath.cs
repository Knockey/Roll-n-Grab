using IJunior.TypedScenes;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private float _deathHeight;

    private void OnValidate()
    {
        if (_deathHeight > -1f)
            _deathHeight = -1f;
    }

    private void Update()
    {
        if (transform.position.y < _deathHeight)
            Game.Load();
    }
}
