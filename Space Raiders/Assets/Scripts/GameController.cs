using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController PlayerTemplate;
    public Transform PlayerSpawnPoint;
    public float SpawnDelay = 3;
    public float SpawnAt = -1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnAt > 0 && Time.time > SpawnAt)
        {
            SpawnPlayer();
        }
    }

    private void SpawnPlayer()
    {
        PlayerController pc = Instantiate(PlayerTemplate);
        pc.GameController = this;
        pc.transform.position = PlayerSpawnPoint.position;
        SpawnAt = -1;
    }

    public void DestroyPlayer(PlayerController toDestroy)
    {
        Destroy(toDestroy.gameObject);
        SpawnAt = Time.time + SpawnDelay;
    }
}
