using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PurpleTwoAI : MonoBehaviour
{
    //Virus States
    private enum State
    {
        Start,
        Roaming,
        Infect
    }
    private State state;

    [Range(1f, 100f)]
    public float health = 100f;

    //Roam Variables
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    public float roamSpeed = 0.9f;
    private List<Transform> infectibles;
    private Transform infectible;
    private int target;
    public float targetRange = 30f;
    //Respawn variables


    //Health
    public Slider HealthSlider;
    private void Awake()
    {
        state = State.Start;
    }
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Virus";
        //startingPosition = transform.position;
        roamPosition = GetRoamingPosition();

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Infectible") == null)
        {
            return;
        }
        else
        {
            infectible = GameObject.FindGameObjectWithTag("Infectible").GetComponent<Transform>();
        }
        //Health
        if (HealthSlider != null) HealthSlider.value = health;
        //
        switch (state)
        {
            default:
            case State.Start:
                state = State.Roaming;
                break;
            case State.Roaming:
                transform.position = Vector2.MoveTowards(transform.position, roamPosition, roamSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, roamPosition) < 1f)
                {
                    roamPosition = GetRoamingPosition();
                }
                if (infectible == null)
                {
                    if (GameObject.FindGameObjectWithTag("Infectible") == null)
                    {
                        infectible = GameObject.FindGameObjectWithTag("Infectible").GetComponent<Transform>();
                    }
                }
                else
                {
                    FindTarget();
                }
                break;
            case State.Infect:
                if (GameObject.FindGameObjectWithTag("Infectible") == null)
                {
                    state = State.Roaming;
                }
                else
                {
                    transform.position = Vector2.MoveTowards(transform.position, infectible.position, roamSpeed * 2 * Time.deltaTime);
                }
                break;


                
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

    private void FindTarget()
    {
        //TODO Change to a cell thats attacked
        if (Vector2.Distance(transform.position, infectible.position) < targetRange)
        {
            state = State.Infect;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Attack"))
        {
            health = health - 12;
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("PlayerAttack"))
        {
            health = health - 20;
            Destroy(other.gameObject);
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 2f);
        }
    }

    void Attack()
    {

    }
}
