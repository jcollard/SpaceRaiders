using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    [field: SerializeField]
    public List<Transform> WayPoints { get; private set; } = new();
    [field: SerializeField]
    public Transform FireSpawnPoint { get; private set; }

    [field: SerializeField]
    public float Speed { get; private set; } = 5;

    [field: SerializeField]
    public int NextWayPoint { get; private set; } = 0;
    [field: SerializeField]
    public GameObject LaserTemplate { get; private set; }
    [field: SerializeField]
    public float FireRate { get; private set; } = 2;
    [field: SerializeField]
    public float LastFire { get; private set; } = 0;

    void Start()
    {
        LastFire = Time.time;
    }

    public Vector2 Velocity
    {
        get
        {
            Vector2 target = WayPoints[NextWayPoint].position;
            Vector2 v = target - (Vector2)this.transform.position;
            v.Normalize();
            return v;
        }
    }

    public static EnemyShipController Spawn(IEnemyShip enemy)
    {
         EnemyShipController newEnemy = Instantiate(enemy.Template);
         newEnemy.WayPoints = enemy.WayPoints;
         newEnemy.transform.position = enemy.SpawnPoint;
         return newEnemy;
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
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        HandleFire();
        MoveShip();
        CheckWayPoint();
    }

    private void HandleFire()
    {
        if (Time.time > (LastFire + FireRate))
        {
            GameObject laser = Instantiate(LaserTemplate);
            laser.transform.position = FireSpawnPoint.position;
            LastFire = Time.time;
        }
    }

    private void MoveShip()
    {
        float newX = transform.position.x + (Velocity.x * Speed * Time.deltaTime);
        float newY = transform.position.y + (Velocity.y * Speed * Time.deltaTime);
        transform.position = new Vector2(newX, newY);
    }

    private void CheckWayPoint()
    {
        Vector2 target = WayPoints[NextWayPoint].position;
        float distance = Vector2.Distance(target, this.transform.position);
        if (distance < .1)
        {
            NextWayPoint = (NextWayPoint + 1) % WayPoints.Count;
        }
    }

}
