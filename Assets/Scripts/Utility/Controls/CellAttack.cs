using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellAttack : MonoBehaviour
{
    public float speed;
    private Transform virus;
    private Vector2 target;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Attack";
        virus = GameObject.FindGameObjectWithTag("Virus").transform;
        target = new Vector2(virus.position.x, virus.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            DestroyAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Virus" ) {
            //DestroyAttack();
        }
    }

    void DestroyAttack()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, .5f);
    }
}
