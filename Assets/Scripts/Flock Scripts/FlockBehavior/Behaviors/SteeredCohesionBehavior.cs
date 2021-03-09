using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Steered Cohesion")]
public class SteeredCohesionBehavior : FilteredFlockBehavior
{

    Vector2 currentVelocity;
    public float cellSmoothTime = 0.5f;

    public override Vector2 CalculateMove(FlockCell cell, List<Transform> context, Flock flock)
    {
        //if no neighbors, return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        //add all points together and average
        Vector2 cohesionMove = Vector2.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(cell, context);
        foreach (Transform item in filteredContext)
        {
            cohesionMove += (Vector2)item.position;
        }
        cohesionMove /= context.Count;

        //create offset from cell position
        cohesionMove -= (Vector2)cell.transform.position;
        cohesionMove = Vector2.SmoothDamp(cell.transform.up, cohesionMove, ref currentVelocity, cellSmoothTime);
        return cohesionMove;
    }
}
