using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        var deltaY = Input.GetAxis("Vertical");
        var deltaX = Input.GetAxis("Horizontal");
        var newXpos = transform.position.x + deltaX;
        var newYpos = transform.position.y + deltaY;
        transform.position = new Vector2(newXpos, newYpos);
    }
}
