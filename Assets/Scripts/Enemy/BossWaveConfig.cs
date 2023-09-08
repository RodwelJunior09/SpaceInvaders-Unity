using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss Wave Config")]
public class BossWaveConfig : ScriptableObject
{
    [Header("Boss Config")]
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private GameObject _bossPathPrefab;
    [SerializeField] private float _bossMoveSpeed = 10f;

    [Header("Helpers Bombers Config")]
    [SerializeField] private GameObject _helperBombers;
    [SerializeField] private GameObject _helperBomberPath;
    [SerializeField] private int _numberOfHelpersBombers = 4;
    [SerializeField] private float _bombersMoveSpeed = 5f;
    [SerializeField] private float _timeBetweenSpawnBombers = 1f;

    public GameObject GetBossPrefab()
    {
        return _bossPrefab;
    }
    public GameObject GetHelpersBombersPrefab()
    {
        return _helperBombers;
    }

    public List<Transform> GetBossWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform childTransform in _bossPathPrefab.transform)
        {
            waveWayPoints.Add(childTransform);
        }
        return waveWayPoints;
    }

    public List<Transform> GetHelpersBombersWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach (Transform pathTransform in _helperBomberPath.transform)
        {
            waveWayPoints.Add(pathTransform);
        }

        return waveWayPoints;
    }

    public float GetTimeBetweenSpawn()
    {
        return _timeBetweenSpawnBombers;
    }

    public float GetBombersSpeed()
    {
        return _bombersMoveSpeed;
    }

    public float GetBossSpeed()
    {
        return _bossMoveSpeed;
    }

    public int GetNumberOfHelpersBombers()
    {
        return _numberOfHelpersBombers;
    }
}
