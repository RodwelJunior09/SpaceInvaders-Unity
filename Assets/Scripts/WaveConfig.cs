using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private float _timeBetweenSpawn = 0.5f;
    [SerializeField] private float _spawnRandomFactor = 0.3f;
    [SerializeField] private int _numberOfEnemies = 10;
    [SerializeField] private float _moveSpeed = 1f;

    public GameObject GetEnemyPrefab()
    {
        return _enemyPrefab;
    }
    public List<Transform> GetWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform childTransform in _pathPrefab.transform)
        {
            waveWayPoints.Add(childTransform);
        }
        return waveWayPoints;
    }
    public float GetTimeBetweenSpawn()
    {
        return _timeBetweenSpawn;
    }
    public float GetSpawnRandomFactor()
    {
        return _spawnRandomFactor;
    }
    public int GetNumberOfEnemies()
    {
        return _numberOfEnemies;
    }
    public float GetMoveSpeed()
    {
        return _moveSpeed;
    }
}
