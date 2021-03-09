using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infected : MonoBehaviour
{
    //Virus States
    private enum State
    {
        Happy,
        Infected
    }
    private State state;

    public GameObject viruses;
    public float spawnTimer = 10f;
    private float currentSpawnTimer;
    private bool canSpawn;

    public int health = 6;
    // Start is called before the first frame update
    void Start()
    {
        currentSpawnTimer = 0;
        state = State.Happy;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Happy:
                //TODO
                break;
            case State.Infected:
                SpawnBabyViruses();
                break;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Virus"))
        {
            state = State.Infected;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Virus"))
        {
            state = State.Happy;
        }
    }

    void SpawnBabyViruses()
    {
        currentSpawnTimer += Time.deltaTime;
        if (currentSpawnTimer > spawnTimer)
        {
            canSpawn = true;
        }
        if (canSpawn)
        {
            currentSpawnTimer = 0f;
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                Instantiate(viruses, transform.position, Quaternion.identity);
                canSpawn = false;
            }
            health = health - 2;
            if(health <= 0)
            {
                Death();
            }
        }
    }

    void Death()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 2f);
    }
}
