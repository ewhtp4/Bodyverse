using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageChange : MonoBehaviour
{
    //private GameObject trigger;
    private GameObject virus;
    public GameObject player;
    private Transform virusTransfrom;
    private bool virusFound;

    // Start is called before the first frame update
    void Start()
    {
        virusFound = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (GameObject.FindGameObjectWithTag("Virus") == null)
        {
            return;
        }
        else
        {
            virus = GameObject.FindGameObjectWithTag("Virus");
            Debug.Log("There is a virus");
            virusTransfrom = GameObject.FindGameObjectWithTag("Virus").GetComponent<Transform>();
            if(Vector2.Distance(transform.position, virusTransfrom.position) < 2.2)
            {
                virusFound = true;
                Debug.Log("Virus Found");
            }
        }
     }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Trigger") && virusFound == true)
        {
            SceneManager.LoadScene(2);
            //Debug.Log("Morf");
            //Invoke("Morf", 0.1f);
            //gameObject.SetActive(false);
            //Destroy(gameObject, 2f);
        }
    }

    void Morf()
    {
        Instantiate(player, transform.position, Quaternion.identity);
    }
}
