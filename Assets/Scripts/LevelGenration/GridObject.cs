using UnityEngine;

public enum GridObjectType
{
    Ground,
    Obstacle,
    Pickable
}

public class GridObject : MonoBehaviour 
{
    [SerializeField] private GridLayer _layer;
    [SerializeField] private int _spawnChance;
    [SerializeField] private GridObjectType _objectType;

    public GridLayer Layer => _layer;
    public int SpawnChance => _spawnChance;
    public GridObjectType ObjectType => _objectType;

    private void OnValidate()
    {
        _spawnChance = Mathf.Clamp(_spawnChance, 1, 100);
    }
}
