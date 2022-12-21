using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float SpawnRate;
    public float LastSpawn;
    public float MinRotation, MaxRotation;
    public Vector2 MinSpeed, MaxSpeed;
    public Transform SpawnPoint;
    public AsteroidController Template;
    // Start is called before the first frame update
    void Start()
    {
        LastSpawn = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (ShouldSpawn())
        {
            SpawnAsteroid();
        }
    }

    private void SpawnAsteroid()
    {
        AsteroidController ac = Instantiate(Template);
        ac.transform.position = SpawnPoint.position;
        ac.RotationSpeed = Random.Range(MinRotation, MaxRotation);
        ac.Speed = new (Random.Range(MinSpeed.x, MaxSpeed.x), Random.Range(MinSpeed.y, MaxSpeed.y));
        LastSpawn = Time.time;
    }

    private bool ShouldSpawn()
    {
        return Time.time > (LastSpawn + SpawnRate);
    }
}
