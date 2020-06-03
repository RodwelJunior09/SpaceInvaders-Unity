using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Boss Wave Config")]
public class BossWaveConfig : ScriptableObject
{
    [Header("Boss Config")]
    [SerializeField] private GameObject _bossPrefab;
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private float _bossMoveSpeed = 10f;
    [SerializeField] private float _bombersMoveSpeed = 5f;

    [Header("Helpers Bombers Config")]
    [SerializeField] private GameObject _helperBombers;
    [SerializeField] private GameObject _helperBomberPath;
    [SerializeField] private int _numberOfHelpersBombers = 4;


    public GameObject GetBossPrefab()
    {
        return _bossPrefab;
    }
    public GameObject GetHelperBombers()
    {
        return _helperBombers;
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
