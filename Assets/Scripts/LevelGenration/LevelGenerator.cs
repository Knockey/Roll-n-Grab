using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridObject[] _templates;
    [SerializeField] private Transform _player;
    [SerializeField] private int _platformWidth;
    [SerializeField] private float _frontViewDistance;
    [SerializeField] private int _backwardViewDistance;
    [SerializeField] private float _cellSize;

    private HashSet<Vector3Int> _cellMatrix = new HashSet<Vector3Int>();

    private void Update()
    {
        var currentCenter = _player.position;
        currentCenter.z = 0;
        FillRadius(currentCenter, _backwardViewDistance, _frontViewDistance);
    }

    private void FillRadius(Vector3 center, float backwardDistance, float forwardDistance)
    {
        var forwardSpawnDistance = (int)(forwardDistance / _cellSize);
        var backwardSpawnDistance = (int)(backwardDistance / _cellSize);
        var fillAreaCenter = WorldToGridPosition(center);

        for (int x = -backwardSpawnDistance; x < forwardSpawnDistance; x++)
        {
            for (int z = -_platformWidth / 2; z < _platformWidth / 2; z++)
            {
                TryCreateOnLayer(GridLayer.Ground, fillAreaCenter + new Vector3Int(x, 0, z));
                TryCreateOnLayer(GridLayer.OnGround, fillAreaCenter + new Vector3Int(x, 0, z));
            }
        }
    }

    private void TryCreateOnLayer(GridLayer layer, Vector3Int gridPosition)
    {
        gridPosition.y = (int)layer - (int)_cellSize;

        if (_cellMatrix.Contains(gridPosition))
            return;
        else
            _cellMatrix.Add(gridPosition);

        var template = GetRandomTemplate(layer);

        if (template == null)
            return;

        var position = GridToWorldPosition(gridPosition);

        Instantiate(template, position, Quaternion.identity, transform);
    }

    private GridObject GetRandomTemplate(GridLayer layer)
    {
        var templatesToSpawn = _templates.Where(template => template.Layer == layer && template.SpawnChance > Random.Range(0, 100));

        foreach (var template in templatesToSpawn)
        {
            return template;
        }

        return null;
    }
    private Vector3 GridToWorldPosition(Vector3Int gridPosition)
    {
        return new Vector3(
            gridPosition.x * _cellSize,
            gridPosition.y * _cellSize,
            gridPosition.z * _cellSize);
    }

    private Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        return new Vector3Int(
            (int)(worldPosition.x / _cellSize),
            (int)(worldPosition.y / _cellSize),
            (int)(worldPosition.z / _cellSize));
    }
}
