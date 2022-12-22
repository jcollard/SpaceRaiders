using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float Speed;
    public float DamageBoost = 3;
    public GameController GameController;
    public GameObject Laser;
    public Transform FrontLaserSpawn;
    public Vector2 Velocity = new (0, 0);
    public Vector2 Min, Max;

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleFire();
        HandleDamageBoost();
        MoveShip();
    }

    private void HandleDamageBoost()
    {
        DamageBoost -= Time.deltaTime;
        DamageBoost = Mathf.Max(0, DamageBoost);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        AsteroidController asAsteroid = other.GetComponent<AsteroidController>();
        if (asAsteroid != null && DamageBoost <= 0)
        {
            GameController.DestroyPlayer(this);
        }
    }

    private void HandleFire()
    {
        if(Input.GetButtonDown("Fire"))
        {
            GameObject laser = Instantiate(Laser);
            laser.transform.position = FrontLaserSpawn.position;
        }
    }

    private void HandleMovement()
    {
        Velocity = HandleHorizontal(Input.GetAxis("Horizontal"));
        Velocity += HandleVertical(Input.GetAxis("Vertical"));
    }

    private Vector2 HandleVertical(float v) => new (0, Mathf.Clamp(v, -1, 1));
    private Vector2 HandleHorizontal(float h) => new (Mathf.Clamp(h, -1, 1), 0);

    private void MoveShip()
    {
        float newX = transform.position.x + (Velocity.x * Speed * Time.deltaTime);
        float newY = transform.position.y + (Velocity.y * Speed * Time.deltaTime);
        newX = Mathf.Clamp(newX, Min.x, Max.x);
        newY = Mathf.Clamp(newY, Min.y, Max.y);
        transform.position = new Vector2(newX, newY);
    }


}
