using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Loop the waves")]
    [SerializeField] private bool looping = false;

    [Header("Enemies Waves")]
    [SerializeField] int indexWave = 0; // Wave to start from
    [SerializeField] private List<WaveConfig> _wavesConfigs;

    [Header("Boss Wave")]
    [SerializeField] private BossWaveConfig _bossConfig;
    [SerializeField] private int _bossScoreThreshold = 1000;

    private GameStatus _gameStatus;
    private GameObject _bossEnemy;
    private bool _bossSpawned;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        _gameStatus = FindFirstObjectByType<GameStatus>();
        do
        {
            if (!_bossSpawned && IsBossTime())
            {
                yield return StartCoroutine(AppearEnemyBoss());
            }

            if (_bossSpawned)
            {
                // Boss defeated, the win scene takes over from here
                if (!_bossEnemy) yield break;
                yield return StartCoroutine(AppearsBossHelpers());
            }
            else
            {
                yield return StartCoroutine(SpawnAllWaves());
            }

            // Always wait at least a frame so an empty pass can't spin
            yield return null;
        } while (looping);
    }

    private bool IsBossTime()
    {
        return _bossConfig && _gameStatus && _gameStatus.GetScore() >= _bossScoreThreshold;
    }

    private IEnumerator AppearEnemyBoss()
    {
        _bossEnemy = Instantiate(_bossConfig.GetBossPrefab(), _bossConfig.GetBossWayPoints().First().transform.position,
            Quaternion.identity);
        _bossEnemy.GetComponent<BossPathway>().SetBossWaveConfig(_bossConfig);
        _bossSpawned = true;
        yield return new WaitForSeconds(5);
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
        for (int i = indexWave; i < _wavesConfigs.Count; i++)
        {
            // Stop the regular waves as soon as the boss should come in
            if (IsBossTime()) yield break;
            yield return StartCoroutine(SpawnAllEnemies(_wavesConfigs[i]));
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
