using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private GridObject[] _templates;
    [SerializeField] private PlayerScore _player;
    [SerializeField] private int _platformWidth;
    [SerializeField] private float _frontViewDistance;
    [SerializeField] private int _backwardViewDistance;
    [SerializeField] private float _cellSize;

    private Vector3 _spawnCenter;
    private Transform _playerPosition;
    private HashSet<Vector3Int> _cellMatrix = new HashSet<Vector3Int>();
    private HashSet<GridObject> _objectsInCells = new HashSet<GridObject>();

    private void Awake()
    {
        _playerPosition = _player.gameObject.transform;

        UpdateSpawnCenter();
    }

    private void OnEnable()
    {
        _player.ObjectPicked += OnObjectPicked;
    }

    private void OnDisable()
    {
        _player.ObjectPicked -= OnObjectPicked;
    }

    private void Update()
    {
        UpdateSpawnCenter();

        FillRadius(_spawnCenter, _backwardViewDistance, _frontViewDistance);

        TryDisableObjectsBehindPlayer();
    }

    private void UpdateSpawnCenter()
    {
        if (_playerPosition.position.x > _spawnCenter.x)
            _spawnCenter = _playerPosition.position;

        _spawnCenter.z = 0;
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

        InstantiateOnLayer(template, position);
    }

    private void InstantiateOnLayer(GridObject template, Vector3 position)
    {
        var objectToInstantiate = TryFindExistObject(template);

        if (objectToInstantiate != null)
        {
            objectToInstantiate.transform.position = position;
            objectToInstantiate.gameObject.SetActive(true);

            return;
        }

        var newObject = Instantiate(template, position, Quaternion.identity, transform);
        _objectsInCells.Add(newObject);
    }

    private GridObject TryFindExistObject(GridObject template)
    {
        return _objectsInCells.FirstOrDefault(obj => obj.ObjectType == template.ObjectType && obj.gameObject.activeSelf == false);
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

    private void TryDisableObjectsBehindPlayer()
    {
        foreach (var gridObject in _objectsInCells)
        {
            if (_playerPosition.position.x - gridObject.gameObject.transform.position.x > _backwardViewDistance)
            {
                gridObject.gameObject.SetActive(false);
            }
        } 
    }

    private void OnObjectPicked(GridObject gridObject)
    {
        _objectsInCells.Remove(gridObject);
    }
}
