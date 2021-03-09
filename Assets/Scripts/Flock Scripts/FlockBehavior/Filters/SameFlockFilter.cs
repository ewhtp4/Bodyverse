using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Filter/Same Flock")]
public class SameFlockFilter : ContextFilter
{
    public override List<Transform> Filter(FlockCell cell, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();
        foreach (Transform item in original)
        {
            FlockCell itemAgent = item.GetComponent<FlockCell>();
            if (itemAgent != null && itemAgent.CellFlock == cell.CellFlock)
            {
                filtered.Add(item);
            }
        }
        return filtered;
    }
}