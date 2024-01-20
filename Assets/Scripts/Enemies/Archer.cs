using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Archer : MonoBehaviour
{

    [SerializeField]
    [Range(0.001f, 1)]
    private float _timestep = 0.1f;

    [SerializeField]
    private float _shootCooldown = 0f;

    [SerializeField]
    private ArcherStats _stats;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _shootCooldown += Time.deltaTime;

        if (_shootCooldown < _stats.maxShootCooldown)
            return;

        _shootCooldown -= _stats.maxShootCooldown;

        Vector3 playerPos = PlayerManager.instance.GetPlayerTransform().position;
        Vector3 midwayPoint, endPoint;
        CalculateMidwayPoint(playerPos, out midwayPoint, out endPoint);

        StartCoroutine(ArrowManager.instance.ShootArrow(transform.position, midwayPoint, endPoint));
    }

    private void CalculateMidwayPoint(Vector3 playerPos, out Vector3 midwayPoint, out Vector3 endPoint)
    {

        RaycastHit2D hit = Physics2D.Raycast(playerPos, Vector2.down);


        if (hit.collider != null)
        {

            endPoint = hit.point;
        }
        else
        {
            endPoint = playerPos;
        }


        midwayPoint = Vector3.Lerp(transform.position, playerPos, 0.5f);
        midwayPoint.y = playerPos.y + _stats.midwayPointOffset;
    }

    private void OnDrawGizmos()
    {
        if (!PlayerManager.instance)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.2f);
        Gizmos.DrawWireSphere(PlayerManager.instance.GetPlayerTransform().position, 0.2f);

        DrawArc();

    }

    private void DrawArc()
    {
        Vector3 previousP = transform.position;
        Vector3 pointA;
        Vector3 pointB;
        Vector3 currentP;

        Vector3 playerPos = PlayerManager.instance.GetPlayerTransform().position;
        Vector3 midwayPoint, endPoint;
        CalculateMidwayPoint(playerPos, out midwayPoint, out endPoint);

        for (float t = 0; t <= 1; t += _timestep)
        {

            pointA = Vector3.Lerp(transform.position, midwayPoint, t);
            pointB = Vector3.Lerp(midwayPoint, endPoint, t);
            currentP = Vector3.Lerp(pointA, pointB, t);

            Gizmos.DrawLine(previousP, currentP);
            previousP = currentP;
        }

        Gizmos.DrawLine(previousP, endPoint);
    }

}
