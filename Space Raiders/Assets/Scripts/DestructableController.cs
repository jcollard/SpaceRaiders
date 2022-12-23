using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class DestructableController : MonoBehaviour
{
    public GameController GameController { get; set; }
    [field: SerializeField]
    public int Points { get; private set; }
    [field: SerializeField]
    private UnityEvent<LaserController, DestructableController>  OnHit { get; set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        LaserController asLaser = other.GetComponent<LaserController>();
        if (asLaser != null)
        {
            if (OnHit.GetPersistentEventCount() == 0)
            {
                DefaultDestroy(asLaser);
            }
            else
            {
                OnHit.Invoke(asLaser, this);
            }
        }
    }

    public void DefaultDestroy(LaserController laser)
    {
        GameController.IncrementScore(Points);
        Destroy(laser.gameObject);
        Destroy(this.gameObject);
    }
}
