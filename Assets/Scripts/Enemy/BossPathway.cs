using System.Collections.Generic;
using UnityEngine;

public class BossPathway : MonoBehaviour
{
    private int _wayPointIndex = 0;
    private BossWaveConfig _bossWaveConfig;
    private List<Transform> _bossWayPoints;
    private List<Transform> _bomberWayPoints;

    void Start()
    {
        _bossWayPoints = _bossWaveConfig.GetBossWayPoints();
        _bomberWayPoints = _bossWaveConfig.GetHelpersBombersWayPoints();
        if (tag.Contains("Boss")) transform.position = _bossWayPoints[_wayPointIndex].transform.position;
        else transform.position = _bomberWayPoints[_wayPointIndex].transform.position;
    }

    void Update()
    {
        if (gameObject.tag.Contains("Boss")) MovementOfBoss();
        else MovementOfBombers();
    }

    void MovementOfBoss()
    {
        if (_wayPointIndex <= _bossWayPoints.Count - 1)
        {
            var target = _bossWayPoints[_wayPointIndex].transform.position;
            var movement = _bossWaveConfig.GetBossSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, movement);
            if (transform.position == target) _wayPointIndex++;
        }
        else
        {
            _wayPointIndex = 0;
        }
    }

    private void MovementOfBombers()
    {
        if (_wayPointIndex <= _bomberWayPoints.Count - 1)
        {
            var target = _bomberWayPoints[_wayPointIndex].transform.position;
            var movement = _bossWaveConfig.GetBombersSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, movement);
            if (transform.position == target)
            {
                _wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetBossWaveConfig(BossWaveConfig bossConfig)
    {
        this._bossWaveConfig = bossConfig;
    }
}
