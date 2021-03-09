using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class FlockCell : MonoBehaviour
{
    //Cell States
    public enum State
    {
        FlokRoam,
        Follow,
        Attack
    }
    public State state;

    //Attack Variables
    public GameObject attackParticle;
    private bool canAttack = false;
    public float attackTimer = 2f;
    private float currentAttackTimer;

    //Behavior variables
    private Transform leader;
    private Transform virus;
    private GameObject virusObj;

    public float followSpeed = 0.5f;
    public float stoppingDistance = 20f;
    public float retreatDistance = 6.5f;

    Flock cellFlock;
    public Flock CellFlock { get { return cellFlock; } }

    Collider2D cellCollider;
    public Collider2D AgentCollider { get { return cellCollider; } }

    [Range(1f, 100f)]
    public float health = 100f;

    [Range(1f, 100f)]
    public float attack = 3f;

    public AudioSource AttackSound;

    // Start is called before the first frame update
    void Start()
    {
        currentAttackTimer = attackTimer;
        state = State.FlokRoam;
        cellCollider = GetComponent<Collider2D>();
        //leader = GameObject.FindGameObjectWithTag("Leader").GetComponent<Transform>();
        AttackSound = GetComponent<AudioSource>();
        virusObj = GameObject.FindGameObjectWithTag("Virus");
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Virus") == null) { return; }
        if (virus == null)
        {
            virus = GameObject.FindGameObjectWithTag("Virus").GetComponent<Transform>();
            if (virus == null)
            {
                return;
            }
        }
        if (leader == null)
        {
            if (GameObject.FindGameObjectWithTag("Leader") == null)
            {
                return;
            }
            else
            {
                leader = GameObject.FindGameObjectWithTag("Leader").GetComponent<Transform>();
            }
        }
        if (virus == null) { state = State.Follow; }
        if (Vector2.Distance(transform.position, leader.position) < 3 && state != State.Attack)
        {
            state = State.Follow;
        }
        if (Vector2.Distance(transform.position, virus.position) < stoppingDistance)// && state == State.Follow)
        {
            state = State.Attack;
        }


        if (health <= 0)
        {
            //DestroyCell();
        }
    }

    public void Initialize(Flock flock)
    {
        cellFlock = flock;
    }
    public void Move(Vector2 velocity)
    {
        switch (state)
        {
            default:
            case State.FlokRoam:
                transform.up = velocity;
                transform.position += (Vector3)velocity * Time.deltaTime;
                break;
            case State.Follow:
                if (Vector2.Distance(transform.position, leader.position) > 2.2)
                {
                    transform.position = Vector2.MoveTowards(transform.position, leader.position, 6 * followSpeed * Time.deltaTime);
                }
                else
                {
                    state = State.FlokRoam;
                }
                break;
            case State.Attack:
                if (GameObject.FindGameObjectWithTag("Virus") == null) { state = State.Follow; }
                if (virus == null)
                {
                    state = State.Follow;
                }
                else
                {
                    AttackVirus();
                    if (Vector2.Distance(transform.position, virus.position) > stoppingDistance)
                    {
                        transform.position = Vector2.MoveTowards(transform.position, virus.position, 6 * followSpeed * Time.deltaTime);
                    }
                    else if (Vector2.Distance(transform.position, virus.position) < stoppingDistance && Vector2.Distance(transform.position, virus.position) > retreatDistance)
                    {
                        transform.position = this.transform.position;
                    }
                    else
                    {
                        transform.position = Vector2.MoveTowards(transform.position, virus.position, -1 * followSpeed * Time.deltaTime);
                    }
                }
                break;
        }
    }
    void AttackVirus()
    {
        attackTimer += Time.deltaTime;
        if (attackTimer > currentAttackTimer)
        {
            canAttack = true;
        }
        if (canAttack)
        {
            attackTimer = 0f;
            if (health <= 0)
            {
                DestroyCell();
            }
            canAttack = false;
            AttackSound.Play();
            Instantiate(attackParticle, transform.position, Quaternion.identity);
            //health = health - 3;
        }
    }

    void DestroyCell() 
    {
        gameObject.SetActive(false);
        Destroy(gameObject, .5f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BabyVirus"))
        {
            health = health - 50;
            if (health <= 0)
            {
                DestroyCell();
            }
        }
    }
}
