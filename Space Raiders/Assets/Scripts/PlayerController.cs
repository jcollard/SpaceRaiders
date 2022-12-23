using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    public Sprite LeanLeft { get; private set; }
    [field: SerializeField]
    public Sprite LeanRight { get; private set; }
    [field: SerializeField]
    public Sprite Forward { get; private set; }

    [field: SerializeField]
    public float Speed { get; private set; } = 5;
    
    [field: SerializeField]
    public float DamageBoost { get; private set; } = 3;
    public bool HasDamageBoost => DamageBoost > 0;
    public bool IsVisible => !HasDamageBoost || Mathf.Sin(Time.time * 20) > 0;
    
    [field: SerializeField]
    public GameController GameController { get; private set; }
    
    [field: SerializeField]
    public GameObject Laser { get; private set; }
    
    [field: SerializeField]
    public Transform FrontLaserSpawn { get; private set; }
    
    [field: SerializeField]
    public Vector2 Velocity { get; private set; } = new (0, 0);
    
    [field: SerializeField]
    public Vector2 Min {get; private set; }

    [field: SerializeField]
    public Vector2 Max {get; private set; }

    public static PlayerController Spawn(PlayerController template, GameController gc)
    {
        PlayerController pc = Instantiate(template);
        pc.GameController = gc;
        return pc;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleFire();
        HandleDamageBoost();
        MoveShip();
        UpdateSprite();
    }

    private void HandleDamageBoost()
    {
        this.GetComponent<Renderer>().enabled = IsVisible;
        DamageBoost -= Time.deltaTime;
        DamageBoost = Mathf.Max(0, DamageBoost);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        PlayerImpactor asImpactor = other.GetComponent<PlayerImpactor>();
        if (asImpactor != null && DamageBoost <= 0)
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

    private void UpdateSprite()
    {
        if (Velocity.x < 0)
        {
            GetComponent<SpriteRenderer>().sprite = LeanLeft;
        }
        else if (Velocity.x > 0)
        {
            GetComponent<SpriteRenderer>().sprite = LeanRight;
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = Forward;
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
