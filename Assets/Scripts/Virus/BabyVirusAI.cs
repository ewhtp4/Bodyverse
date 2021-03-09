using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyVirusAI : MonoBehaviour
{
    [Range(1f, 100f)]
    public float health = 100f;

    //Border x and y
    public float min_Y, max_Y;
    public float min_X, max_X;

    //Roam Variables
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    float roamSpeed = 2.5f;

    //Groww Variables
    public float growTimer = 45f;
    public GameObject virus;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "BabyVirus";
        //startingPosition = transform.position;
        roamPosition = GetRoamingPosition();
    }

    // Update is called once per frame
    void Update()
    {
        growTimer = growTimer - Time.deltaTime;
        if(growTimer <= 0)
        {
            Grow();
        }
        transform.position = Vector2.MoveTowards(transform.position, roamPosition, roamSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, roamPosition) < 1f)
        {
            roamPosition = GetRoamingPosition();
        }
    }

    private Vector3 GetRoamingPosition()
    {
        return startingPosition + GetRandomDirection() * Random.Range(10f, 70f);
    }

    public static Vector3 GetRandomDirection()
    {
        return new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)).normalized;
    }

    void Border()
    {
        Vector3 temp = transform.position;
        if (temp.y > max_Y)
        {
            temp.y = max_Y;
            transform.position = temp;
        }
        if (temp.y < min_Y)
        {
            temp.y = min_Y;
            transform.position = temp;
        }
        if (temp.x > max_X)
        {
            temp.x = max_X;
            transform.position = temp;
        }
        if (temp.x < min_X)
        {
            temp.x = min_X;
            transform.position = temp;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            health = health - 1;
            if (health <= 0)
            {
                gameObject.SetActive(false);
                Destroy(gameObject, 2f);
            }
        }
    }

    void Grow()
    {
        //Invoke("SpawnNext", 0.4f);
        Instantiate(virus, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
        Destroy(gameObject, 2f);
    }
}


