using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public float SpawnRate = 2;
    public float LastSpawn = 0;
    public float MinRotation = -90, MaxRotation = 90;
    public Vector2 MinSpeed, MaxSpeed;
    public Transform MinSpawnPoint, MaxSpawnPoint;
    public Vector2 MinSpawnVector => MinSpawnPoint.position;
    public Vector2 MaxSpawnVector => MaxSpawnPoint.position;
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
        Vector2 spawnPoint = new (Random.Range(MinSpawnVector.x, MaxSpawnVector.x), MinSpawnVector.y);
        ac.transform.position = spawnPoint;
        ac.RotationSpeed = Random.Range(MinRotation, MaxRotation);
        ac.Speed = new (Random.Range(MinSpeed.x, MaxSpeed.x), Random.Range(MinSpeed.y, MaxSpeed.y));
        LastSpawn = Time.time;
    }

    private bool ShouldSpawn()
    {
        return Time.time > (LastSpawn + SpawnRate);
    }
}
