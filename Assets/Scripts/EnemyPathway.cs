using System.Collections.Generic;
using UnityEngine;

public class EnemyPathway : MonoBehaviour
{
    private int _wayPointIndex = 0;
    private WaveConfig _waveConfig;
    private List<Transform> _wayPoints;

    // Start is called before the first frame update
    void Start()
    {
        _wayPoints = _waveConfig.GetWayPoints();
        transform.position = _wayPoints[_wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovementOfEnemy();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this._waveConfig = waveConfig;
    }

    private void MovementOfEnemy()
    {
        if (_wayPointIndex <= _wayPoints.Count - 1)
        {
            var target = _wayPoints[_wayPointIndex].transform.position;
            var movement = _waveConfig.GetMoveSpeed() * Time.deltaTime;
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
}
