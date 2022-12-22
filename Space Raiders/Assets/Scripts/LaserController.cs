using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{

    public Vector2 Speed = new (0, 5);

    // Update is called once per frame
    void Update()
    {
        MoveLaser();
    }

    // private void OnTriggerEnter2D(Collider2D other) {
    //     // LaserController asLaser = other.GetComponent<LaserController>();
    //     Debug.Log($"{this}.OnTriggerEnter2D({other}");
    // }

    private void MoveLaser()
    {
        float newX = transform.position.x + (Speed.x * Time.deltaTime);
        float newY = transform.position.y + (Speed.y * Time.deltaTime);
        transform.position = new Vector2(newX, newY);
    }
}
