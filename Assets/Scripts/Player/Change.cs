using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("ChangeToPlayer", 0.2f);
    }

    void ChangeToPlayer()
    {
        gameObject.SetActive(false);
        Destroy(gameObject, 2f);
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
