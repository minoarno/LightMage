using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformWaypoints : MonoBehaviour
{
    [Range(0.001f, 10)]
    [SerializeField] private float waypointSize = 2;

    private int _curWaypoint = 0;



    private void OnDrawGizmosSelected()
    {
        if (transform.childCount == 0)
            return;


        for (int i = 0; i < transform.childCount; i++)
        {
            if(i == _curWaypoint)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.GetChild(i).position, waypointSize);
                continue;
            }

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.GetChild(i).position, waypointSize);
        }

        Gizmos.color = Color.red;

        for (int i = 0; i < transform.childCount - 1; ++i)
        {
            Gizmos.DrawLine(transform.GetChild(i).position, transform.GetChild(i + 1).position);
        }

        Gizmos.DrawLine(transform.GetChild(transform.childCount - 1).position, transform.GetChild(0).position);
    }


    public Transform GetNextWayPoint(Transform currentWaypoint)
    {
        if (currentWaypoint == null)
        {
            _curWaypoint = 0;
            return transform.GetChild(0);
        }


        if (currentWaypoint.GetSiblingIndex() < transform.childCount - 1)
        {
            _curWaypoint = currentWaypoint.GetSiblingIndex() + 1;
            return transform.GetChild(currentWaypoint.GetSiblingIndex() + 1);
        }
        else
        {
            _curWaypoint = 0;
            return transform.GetChild(0);
        }

    }
}
