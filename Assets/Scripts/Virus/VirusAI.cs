using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirusAI : MonoBehaviour
{
    //Virus States
    private enum State { 
        Start,
        Roaming,
        Infect
    }
    private State state;

    [Range(1f, 100f)]
    public int health = 100;
   // public HealthBar healthBar;

    //Roam Variables
    private Vector3 startingPosition;
    private Vector3 roamPosition;
    public float roamSpeed = 0.9f;
    private List<Transform> infectibles;
    private Transform infectible;
    private int target;
    public float targetRange = 30f;
    //Respawn variables
    public GameObject viruses;
    public GameObject attackParticle;
    //Start Variables
    private SpriteRenderer sprite;
    public LayerMask layerToImpact;
    public float fieldOfImpact = 20f;
    public float force = 30f;
    private Collider2D[] tragets;
    public float hitcount = 4f;

    public Slider helth;
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
        sprite = GetComponent<SpriteRenderer>();
        //healthBar.SetHealth(health, maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //HealthDisplay
        if(helth != null) helth.value = health;
        ///
        if (GameObject.FindGameObjectWithTag("Infectible") == null)
        {
            return;
        }
        else
        {
            infectible = GameObject.FindGameObjectWithTag("Infectible").GetComponent<Transform>();
        }

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
                if(GameObject.FindGameObjectWithTag("Infectible") == null)
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
            Destroy(other.gameObject);
            health = health - 12;
            //healthBar.SetHealth(health, maxHealth);
        }
        else if (other.CompareTag("PlayerAttack"))
        {
            Destroy(other.gameObject);
            health = health - 24;
            //healthBar.SetHealth(health, maxHealth);
        }
        if (health <= 0)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 2f);
            Hit();
            SpawnNext();
        }
    }

    void Hit()
    {
        Collider2D[] tragets = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToImpact);

        foreach(Collider2D target in tragets)
        {
            Vector2 direction = target.transform.position - transform.position;
            target.GetComponent<Rigidbody2D>().AddForce(direction * force);
        }
    }

    void SpawnNext()
    {
        //Option for a random spawn location
        //Vector3 spawnPoint = transform.position;
        //spawnPoint.x = Random.Range(min_X, max_X);
        //spawnPoint.y = Random.Range(min_Y, max_Y);
        Instantiate(viruses, transform.position, Quaternion.identity); 
    }

    void Attack1()
    {
        Vector2 direction1;
        Vector2 direction2;
        Vector2 direction3;
        Vector2 direction4;
        direction1.x = transform.position.x + 5;
        direction1.y = transform.position.y;
        direction2.x = transform.position.x - 5;
        direction2.y = transform.position.y;
        direction3.y = transform.position.y + 5;
        direction3.x = transform.position.x;
        direction4.y = transform.position.y - 5;
        direction4.x = transform.position.x;
        Instantiate(attackParticle, direction1, Quaternion.identity);
        Instantiate(attackParticle, direction2, Quaternion.identity);
        Instantiate(attackParticle, direction3, Quaternion.identity);
        Instantiate(attackParticle, direction4, Quaternion.identity);
    }
}
