using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectibleSpawner : MonoBehaviour
{
    public float min_X, max_X;
    public float min_y, max_y;
    public float timer = 0f;
    public GameObject cell;
    private GameObject existingCell;
    private Transform existingCellTransform;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        existingCell = GameObject.FindGameObjectWithTag("Infectible");
        if(existingCell != null)
        {
            existingCellTransform = GameObject.FindGameObjectWithTag("Infectible").GetComponent<Transform>();
        }
        for(int i = 0; i < 35; i++)
        {
            float position_X = Random.Range(min_X, max_X);
            float position_y = Random.Range(min_y, max_y);
            Vector3 temp = transform.position;
            temp.x = position_X;
            temp.y = position_y;
            transform.position = temp;
            if(existingCellTransform != null)
            {
                if (Vector2.Distance(transform.position, existingCellTransform.position) < 3f)
                {
                    Vector3 temp2 = transform.position;
                    temp2.x = existingCellTransform.position.x + Random.Range(-4, 4);
                    temp2.y = existingCellTransform.position.y + Random.Range(-4, 4);
                    transform.position = temp2;
                }
            }
            Instantiate(cell, transform.position, Quaternion.identity);
        }
    }
}
