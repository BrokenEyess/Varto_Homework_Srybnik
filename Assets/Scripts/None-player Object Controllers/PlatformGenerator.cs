using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlatformGenerator : MonoBehaviour
{
    [Header("Global settings:")] 
    [Space] 
    [SerializeField] private Transform _target;
    [Space] 
    [Header("Spawn settings")] 
    [Space] 
    [SerializeField] private Platform _platformPrefab;
    [SerializeField] private int _stepsCountToSpawn;
    [SerializeField] private float _stepCountToDelete;
    [SerializeField] private float _stepHeight;
    [SerializeField] private Vector2 _bounds;

    private Queue<Platform> _spawnedPlatforms;
    
    private float _lastPlatformSpawnedOnPlayerPosition;
    private float _lastPlatformDeletedOnPlayerPosition;

    private void Awake()
    {
        _spawnedPlatforms = new Queue<Platform>();
        _lastPlatformDeletedOnPlayerPosition = _lastPlatformSpawnedOnPlayerPosition * _target.position.y;

        for (int i = 0; i < _stepsCountToSpawn; i++)
        {
            SpawnPlatform(i + 1);
        }
    }

    private void SpawnPlatform(int stepsCount)
    {
        float platformPositionX = Random.Range(_bounds.x, _bounds.y);
        float platformPositionY = _target.position.y + stepsCount * _stepHeight;
        Vector3 platformPosition = new Vector3(platformPositionX, platformPositionY, transform.position.z);
        Platform spawnedPlatform = Instantiate(_platformPrefab, platformPosition, Quaternion.identity, this.transform);
        spawnedPlatform.Init(_target);
        _spawnedPlatforms.Enqueue(spawnedPlatform);
    }

    private void Update()
    {
        CheckToSpawnPlatform();
        CheckToDeletePlatform();
    }

    private void CheckToSpawnPlatform()
    {
        if (_target.position.y - _lastPlatformSpawnedOnPlayerPosition > _stepHeight)
        {
            SpawnPlatform(_stepsCountToSpawn);
            _lastPlatformSpawnedOnPlayerPosition += _stepHeight;
        }
    }

    private void CheckToDeletePlatform()
    {
        if (_target.position.y - _lastPlatformDeletedOnPlayerPosition > _stepHeight * _stepCountToDelete)
        {
            var platformToDelete = _spawnedPlatforms.Dequeue();
            if (platformToDelete && platformToDelete.gameObject)
            {
                Destroy(platformToDelete.gameObject);
            }
            _lastPlatformDeletedOnPlayerPosition += _stepHeight;
        }
    }
}
