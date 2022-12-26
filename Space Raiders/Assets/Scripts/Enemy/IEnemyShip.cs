using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyShip
{
    public EnemyShipController Template { get; }
    public List<Transform> WayPoints { get; }
    public Vector2 SpawnPoint { get; }
}