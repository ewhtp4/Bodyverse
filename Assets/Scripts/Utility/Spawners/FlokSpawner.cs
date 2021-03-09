using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlokSpawner : MonoBehaviour
{
    public float min_X, max_X;
    public float min_y, max_y;
    public float timer = 0f;
    public GameObject[] floks;
    private int numberOfFlocks = 8;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", timer);
    }

    void Spawn()
    {
        float position_X = Random.Range(min_X, max_X);
        float position_y = Random.Range(min_y, max_y);
        Vector3 temp = transform.position;
        temp.x = position_X;
        temp.y = position_y;
        if(numberOfFlocks > 0)
        {
            if (Random.Range(0, 2) > 0)
            {
                Instantiate(floks[Random.Range(0, floks.Length)], transform.position, Quaternion.identity);
                timer = Random.Range(3, 10);
            }
            Invoke("Spawn", timer);
            numberOfFlocks = numberOfFlocks - 1;
        }
    }
}
