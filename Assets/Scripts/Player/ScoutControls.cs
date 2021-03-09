using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoutControls : MonoBehaviour
{
    float moveSpeed = 5f;
    public Rigidbody2D rb;
    public new Camera camera;
    Vector2 movement;
    Vector2 mousePosition;
    private GameObject virus;
    private GameObject babyVirus;
    public int health = 300;
    public GameObject WinScreen;

    void Start()
    {
        virus = GameObject.FindGameObjectWithTag("Virus");
        babyVirus = virus = GameObject.FindGameObjectWithTag("BabyVirus");
        WinScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (virus == null)
        {
            virus = GameObject.FindGameObjectWithTag("Virus");
            babyVirus = GameObject.FindGameObjectWithTag("BabyVirus");
            if (babyVirus == null)
            {
                babyVirus = GameObject.FindGameObjectWithTag("BabyVirus");
                if (virus == null && babyVirus == null)
                {
                    WinScreen.SetActive(true);
                    //SceneManager.LoadScene(0);
                }
            }

        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Virus"))
        {
            //Destroy(other.gameObject);
            //health = health - 12;
        }
        else if (other.CompareTag("BabyVirus"))
        {
            //Destroy(other.gameObject);
            //health = health - 24;
        }

        if (health <= 0)
        {
            //SceneManager.LoadScene(0);
        }
    }
}
