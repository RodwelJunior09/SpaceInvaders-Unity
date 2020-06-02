using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int indexWave = 0;
    [SerializeField] private bool looping = false;
    [SerializeField] private List<WaveConfig> wavesConfigs;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (looping)
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int i = indexWave; i < wavesConfigs.Count; i++)
        {
            var currentWave = wavesConfigs[i];
            yield return StartCoroutine(SpawnAllEnemies(currentWave));
        }
    }

    private IEnumerator SpawnAllEnemies(WaveConfig currentWave)
    {
        for (int i = 0; i < currentWave.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(currentWave.GetEnemyPrefab(), currentWave.GetWayPoints()[0].transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathway>().SetWaveConfig(currentWave);
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawn());
        }
    }
}
