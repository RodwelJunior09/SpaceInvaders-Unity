using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Loop the waves")]
    [SerializeField] private bool looping = false;

    [Header("Enemies Waves")]
    [SerializeField] int indexWave = 0;
    [SerializeField] private List<WaveConfig> _wavesConfigs;

    [Header("Boss Wave")]
    [SerializeField] private BossWaveConfig _bossConfig;

    private GameStatus _gameStatus;
    private GameObject _bossEnemy;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _gameStatus = FindObjectOfType<GameStatus>();
        while (looping)
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
    }

    private IEnumerator SpawnBossWave()
    {
        if (GameObject.FindGameObjectsWithTag("Boss").Length < 1)
        {
            yield return StartCoroutine(AppearEnemyBoss());
        }
        yield return StartCoroutine(AppearsBossHelpers());
    }

    private IEnumerator AppearEnemyBoss()
    {
        var boss = Instantiate(_bossConfig.GetBossPrefab(), _bossConfig.GetBossWayPoints().First().transform.position,
            Quaternion.identity);
        boss.GetComponent<BossPathway>().SetBossWaveConfig(_bossConfig);
        yield return new WaitForSeconds(1);
    }

    private IEnumerator AppearsBossHelpers()
    {
        // Appears boss helpers
        for (int i = 0; i < _bossConfig.GetNumberOfHelpersBombers(); i++)
        {
            var bomber = Instantiate(_bossConfig.GetHelpersBombersPrefab(),
                _bossConfig.GetHelpersBombersWayPoints().First().transform.position, Quaternion.identity);
            bomber.GetComponent<BossPathway>().SetBossWaveConfig(_bossConfig);
            yield return new WaitForSeconds(_bossConfig.GetTimeBetweenSpawn());
        }
    }


    private IEnumerator SpawnAllWaves()
    {
        if (_gameStatus.GetScore() > 1000)
        {
            yield return StartCoroutine(SpawnBossWave());
        }
        for (int i = indexWave; i < _wavesConfigs.Count; i++)
        {
            var currentWave = _wavesConfigs[i];
            if (_gameStatus.GetScore() <= 1000) yield return StartCoroutine(SpawnAllEnemies(currentWave));
            else { StopCoroutine(SpawnAllEnemies(currentWave)); break; }
        }
    }

    private IEnumerator SpawnAllEnemies(WaveConfig currentWave)
    {
        for (int i = 0; i < currentWave.GetNumberOfEnemies(); i++)
        {
            var newEnemy = Instantiate(currentWave.GetEnemyPrefab(), currentWave.GetWayPoints().First().transform.position, Quaternion.identity);
            newEnemy.GetComponent<EnemyPathway>().SetWaveConfig(currentWave);
            yield return new WaitForSeconds(currentWave.GetTimeBetweenSpawn());
        }
    }
}
