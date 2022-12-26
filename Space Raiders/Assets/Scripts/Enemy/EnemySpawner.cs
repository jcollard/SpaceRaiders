using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [field: SerializeField]
    public float SpawnRate { get; set; } = 3;
    [field: SerializeField]
    private List<IEnemyShip> EnemyQueue { get; set; } = new ();
    [field: SerializeField]
    public GameController GameController { get; set; }

    void Start()
    {
        this.InvokeRepeating(nameof(SpawnNext), SpawnRate, SpawnRate);
        // TODO: Look into CancelInvoke
    }

    private void SpawnNext()
    {
        if (EnemyQueue.Count > 0)
        {
            IEnemyShip enemy = EnemyQueue[0];
            EnemyQueue.RemoveAt(0);
            EnemyShipController.Spawn(enemy, GameController);
        }
    }

    public void EnqueueEnemies(List<IEnemyShip> enemies)
    {
        foreach (IEnemyShip e in enemies)
        {
            EnemyQueue.Add(e);
        }
    }
}
