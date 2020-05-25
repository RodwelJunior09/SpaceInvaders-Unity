using System.Collections.Generic;
using UnityEngine;

public class EnemyPathway : MonoBehaviour
{
    // Config Variables
    [SerializeField] private WaveConfig waveConfig;
    [SerializeField] private float moveSpeed = 2f;

    private int wayPointIndex = 0;
    private List<Transform> _wayPoints;

    // Start is called before the first frame update
    void Start()
    {
        _wayPoints = waveConfig.GetWayPoints();
        transform.position = _wayPoints[wayPointIndex].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MovementOfEnemy();
    }

    private void MovementOfEnemy()
    {
        if (wayPointIndex <= _wayPoints.Count - 1)
        {
            var target = _wayPoints[wayPointIndex].transform.position;
            var movement = moveSpeed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target, movement);
            if (transform.position == target)
            {
                wayPointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
