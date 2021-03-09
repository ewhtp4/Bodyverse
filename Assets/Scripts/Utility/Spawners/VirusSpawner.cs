using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusSpawner : MonoBehaviour
{
    public float min_X, max_X;
    public float min_y, max_y;
    public float timer = 5f;
    
    public GameObject[] viruses;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Spawn", timer);
        viruses = GameObject.FindGameObjectsWithTag("Virus");

        if (viruses == null)
        {
            Invoke("Spawn", timer);
            viruses = GameObject.FindGameObjectsWithTag("Virus");
        }
       
    }
     
    public void Spawn()
    {
        float position_X = Random.Range(min_X, max_X);
        float position_y = Random.Range(min_y, max_y);
        Vector3 temp = transform.position;
        temp.x = position_X;
        temp.y = position_y;
        for(int i = 0; i < viruses.Length; i ++)
        {
            //Instantiate(viruses[i], temp, Quaternion.identity);
            Instantiate(viruses[i], spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            timer = Random.Range(20, 50);
        }
        Invoke("Spawn", timer);
    }
}
