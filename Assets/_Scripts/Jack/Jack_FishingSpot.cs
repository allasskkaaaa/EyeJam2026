using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jack_FishingSpot : MonoBehaviour
{
    public Fishable fishable;
    public float radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, radius);
    }
}
