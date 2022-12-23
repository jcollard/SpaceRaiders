using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [field: SerializeField]
    private EnemySpawner EnemySpawner { get; set; }
    [field: SerializeField]
    public Transform EnemySpawnPoint { get; private set; }

    [field: SerializeField]
    public List<EnemyShipController> EnemyTypes { get; private set; }
    [field: SerializeField]
    public List<Transform> WayPoints { get; private set; }

    [field: SerializeField]
    public PlayerController PlayerTemplate { get; private set; }
    [field: SerializeField]
    public Transform PlayerSpawnPoint { get; private set; }
    [field: SerializeField]
    public float SpawnDelay { get; private set; } = 3;
    [field: SerializeField]
    public float SpawnAt { get; private set; }= -1;
    [field: SerializeField]
    public int Score { get; private set; } = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        List<Transform> waypoints = new ()
        {
            WayPoints[0], WayPoints[1], WayPoints[2]
        };
        List<Transform> waypoints2 = new ()
        {
            WayPoints[2], WayPoints[1], WayPoints[0]
        };
        List<Transform> waypoints3 = new ()
        {
            WayPoints[0], WayPoints[2], WayPoints[1], WayPoints[2]
        };
        List<IEnemyShip> enemies = new ()
        {
            new EnemyShip(EnemyTypes[0], waypoints, EnemySpawnPoint.position),
            new EnemyShip(EnemyTypes[0], waypoints2, EnemySpawnPoint.position),
            new EnemyShip(EnemyTypes[0], waypoints3, EnemySpawnPoint.position)
        };
        EnemySpawner.EnqueueEnemies(enemies);
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnAt > 0 && Time.time > SpawnAt)
        {
            SpawnPlayer();
        }
    }

    public void IncrementScore(int amount)
    {
        Score += amount;
    }

    private void SpawnPlayer()
    {
        PlayerController pc = PlayerController.Spawn(PlayerTemplate, this);
        pc.transform.position = PlayerSpawnPoint.position;
        SpawnAt = -1;
    }

    public void DestroyPlayer(PlayerController toDestroy)
    {
        Destroy(toDestroy.gameObject);
        SpawnAt = Time.time + SpawnDelay;
    }
}
