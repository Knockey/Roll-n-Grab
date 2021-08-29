using UnityEngine;

public class GridObject : MonoBehaviour
{
    [SerializeField] private GridLayer _layer;
    [SerializeField] private int _spawnChance;

    public GridLayer Layer => _layer;
    public int SpawnChance => _spawnChance;

    private void OnValidate()
    {
        _spawnChance = Mathf.Clamp(_spawnChance, 1, 100);
    }
}
