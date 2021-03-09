using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockCell cellPrefab;
    List<FlockCell> cells = new List<FlockCell>();
    public FlockBehavior behavior;

    [Range(10, 500)]
    public int startingCount;
    const float cellDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    [Range(1f, 40f)]
    public float flokTimer;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        startingCount = Random.Range(8, 25);
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockCell newCell = Instantiate(
                cellPrefab,
                Random.insideUnitCircle * startingCount * cellDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newCell.name = "Cell " + i;
            newCell.Initialize(this);
            cells.Add(newCell);
        }
        //flokTimer = 120f;
    }

    // Update is called once per frame
    void Update()
    {
        //flokTimer = flokTimer - Time.deltaTime;
        //if(flokTimer <= 0)
        //{
        //    Destroy(gameObject, .5f);
        //}
        foreach (FlockCell cell in cells)
        {
            List<Transform> context = GetNearbyObjects(cell);
            //FOR DEMO ONLY
            //cell.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, 6f);
            Vector2 move = behavior.CalculateMove(cell, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            cell.Move(move);

            if(cell.health <= 0)
            {
                Destroy(gameObject, .5f);
            }
        }
    }

    List<Transform> GetNearbyObjects(FlockCell cell)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(cell.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != cell.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

}
