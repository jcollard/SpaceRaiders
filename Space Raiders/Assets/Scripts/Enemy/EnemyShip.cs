using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShip : IEnemyShip
{
    public EnemyShipController Template { get; }
    public List<Transform> WayPoints { get; }
    public Vector2 SpawnPoint { get; }

    public EnemyShip(EnemyShipController template, List<Transform> waypoints, Vector2 spawn)
    {
        Template = template;
        WayPoints = waypoints;
        SpawnPoint = spawn;
    }
}