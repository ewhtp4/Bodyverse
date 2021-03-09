using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject Texts;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Scout" || collision.tag == "Leader")
        {
            Texts.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Scout" || collision.tag == "Scout")
        {
            Texts.SetActive(false);
        }
        }

}
