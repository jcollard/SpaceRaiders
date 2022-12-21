using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    public float RotationSpeed;
    public Vector2 Speed;

    // Update is called once per frame
    void Update()
    {
        RotateMeteor();
        MoveMeteor();
    }

    private void RotateMeteor()
    {
        float newZ = transform.rotation.eulerAngles.z + (RotationSpeed * Time.deltaTime);
        Vector3 newR = new (0, 0, newZ);
        transform.rotation = Quaternion.Euler(newR);        
    }

    public void MoveMeteor()
    {
        float newX = transform.position.x + (Speed.x * Time.deltaTime);
        float newY = transform.position.y + (Speed.y * Time.deltaTime);
        transform.position = new Vector2(newX, newY);
    }
}
