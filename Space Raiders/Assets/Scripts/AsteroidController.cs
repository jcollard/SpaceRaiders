using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [field: SerializeField]
    public AsteroidController OnDestroyedTemplate { get; private set; }
    [field: SerializeField]
    public float RotationSpeed { get; private set; }
    [field: SerializeField]
    public Vector2 Speed { get; private set; }

    public static AsteroidController Spawn(AsteroidController template, float rotationSpeed, Vector2 speed)
    {
        AsteroidController newAsteroid = Instantiate(template);
        newAsteroid.RotationSpeed = rotationSpeed;
        newAsteroid.Speed = speed;
        return newAsteroid;
    }

    // Update is called once per frame
    void Update()
    {
        RotateMeteor();
        MoveMeteor();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LaserController asLaser = other.GetComponent<LaserController>();
        if (asLaser != null)
        {
            OnLaserHit(asLaser);
        }

    }

    private void OnLaserHit(LaserController laser)
    {
        Destroy(laser.gameObject);
        if (OnDestroyedTemplate != null)
        {
            AsteroidController newObj = Instantiate(OnDestroyedTemplate);
            newObj.transform.position = this.transform.position;
            newObj.RotationSpeed = RotationSpeed;
            newObj.Speed = Speed;
        }
        Destroy(this.gameObject);
    }

    private void RotateMeteor()
    {
        float newZ = transform.rotation.eulerAngles.z + (RotationSpeed * Time.deltaTime);
        Vector3 newR = new(0, 0, newZ);
        transform.rotation = Quaternion.Euler(newR);
    }

    public void MoveMeteor()
    {
        float newX = transform.position.x + (Speed.x * Time.deltaTime);
        float newY = transform.position.y + (Speed.y * Time.deltaTime);
        transform.position = new Vector2(newX, newY);
    }
}
