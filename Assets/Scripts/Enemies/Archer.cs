using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Archer : MonoBehaviour
{

    [SerializeField]
    Transform ArrowLandLocation;

    [SerializeField]
    [Range(0.001f, 1)]
    private float _timestep = 0.1f;

    [SerializeField]
    private float _shootCooldown = 0.5f;
    [SerializeField]
    private float _midwayPointOffset = 30;





    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _shootCooldown += Time.deltaTime;

        if (_shootCooldown < 2f)
            return;

        _shootCooldown -= 2f;

        Vector3 midwayPoint = Vector3.Lerp(transform.position, ArrowLandLocation.position, 0.5f);
        midwayPoint.y += _midwayPointOffset;

        StartCoroutine(ArrowManager.instance.ShootArrow(transform.position, midwayPoint, ArrowLandLocation.position));
    }

    private void OnDrawGizmos()
    {
        if (!ArrowLandLocation)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1);
        Gizmos.DrawWireSphere(ArrowLandLocation.position, 1);

        DrawArc();

    }

    private void DrawArc()
    {
        Vector3 PreviousP = transform.position;
        Vector3 PointA;
        Vector3 PointB;
        Vector3 CurrentP;

        Vector3 midwayPoint = Vector3.Lerp(transform.position, ArrowLandLocation.position, 0.5f);
        midwayPoint.y += _midwayPointOffset;

        for (float t = 0; t <= 1; t += _timestep)
        {

            PointA = Vector3.Lerp(transform.position, midwayPoint, t);
            PointB = Vector3.Lerp(midwayPoint, ArrowLandLocation.position, t);
            CurrentP = Vector3.Lerp(PointA, PointB, t);

            Gizmos.DrawLine(PreviousP, CurrentP);
            PreviousP = CurrentP;
        }

        Gizmos.DrawLine(PreviousP, ArrowLandLocation.position);
    }

}
