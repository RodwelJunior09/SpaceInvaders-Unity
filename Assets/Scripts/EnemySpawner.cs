using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> wavesConfigs;

    private int indexWave = 0;

    // Start is called before the first frame update
    void Start()
    {
        var currentWave = wavesConfigs[indexWave];
        StartCoroutine(SpawnAllEnemies(currentWave));
    }

    private IEnumerator SpawnAllEnemies(WaveConfig currentWave)
    {
        for (int i = 0; i < currentWave.GetNumberOfEnemies(); i++)
        {
            Instantiate(currentWave.GetEnemyPrefab(), currentWave.GetWayPoints()[0].transform.position, Quaternion.identity);
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawn());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
