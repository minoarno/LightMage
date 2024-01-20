using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;




[RequireComponent(typeof(Rigidbody2D))]
public class Flyer : MonoBehaviour
{
    enum FlyerStates
    {
        Flying,
        Divebombing,
        Dead,
    }

    [SerializeField]
    private FlyerStats _stats;


    // Store reference to the waypoint system
    [SerializeField] private FlyerWaypoints _waypoints;

    private Transform _currentWaypoint;
    private Rigidbody2D _rigidbody;

    private Vector3 _moveDir;
    private Vector3 _diveBombTarget;
    private Vector3 _diveBombRaycastDir;

    private FlyerStates _flyerState = FlyerStates.Flying;
    private float _timeSinceDivebomb = 0;

    private LayerMask _toHitLayerMask;



    // Start is called before the first frame update
    void Start()
    {
        if (_waypoints == null)
        {
            Debug.LogError($"No Positions given to flyer: {gameObject.name}");
            return;
        }


        _rigidbody = GetComponent<Rigidbody2D>();

        _currentWaypoint = _waypoints.GetNextWayPoint(_currentWaypoint);

        transform.position = _currentWaypoint.position;

        _currentWaypoint = _waypoints.GetNextWayPoint(_currentWaypoint);

        _toHitLayerMask = LayerMask.GetMask("Player", "Terrain");
    }


    // Update is called once per frame
    void Update()
    {

        switch (_flyerState)
        {
            case FlyerStates.Flying:
                DoWaypoints();
                break;
            case FlyerStates.Divebombing:
                DoDiveBombing();
                break;
            case FlyerStates.Dead:
                break;
            default:
                break;
        }
    }

    private void DoDiveBombing()
    {
        _moveDir = (_diveBombTarget - transform.position).normalized * _stats.moveSpeed * Time.deltaTime;

        _rigidbody.velocity = _moveDir;

        if (Vector2.Distance(transform.position, _diveBombTarget) < _stats.distanceTreshold)
        {
            _flyerState = FlyerStates.Flying;
            _diveBombRaycastDir = Vector3.zero;
        }
    }

    private void DoWaypoints()
    {

        _moveDir = (_currentWaypoint.position - transform.position).normalized * _stats.moveSpeed * Time.deltaTime;

        _rigidbody.velocity = _moveDir;
        if (Vector2.Distance(transform.position, _currentWaypoint.position) < _stats.distanceTreshold)
        {
            _currentWaypoint = _waypoints.GetNextWayPoint(_currentWaypoint);
        }


        _timeSinceDivebomb += Time.deltaTime;

        if (_timeSinceDivebomb <= _stats.minDivebombDelay)
            return;
        _timeSinceDivebomb = 0;


        if (_stats.divebombChance < Random.value)
            return;

        StartDiveBomb();

    }

    private void StartDiveBomb()
    {
        if (!PlayerManager.instance || !PlayerManager.instance.GetPlayerTransform())
            return;

        Vector3 playerPos = PlayerManager.instance.GetPlayerTransform().position;

        _diveBombRaycastDir = playerPos - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _diveBombRaycastDir, float.MaxValue, _toHitLayerMask);

        if (!hit)
            return;

        int bitshiftedMask = LayerMask.GetMask("Player") >> hit.collider.gameObject.layer;

        if (bitshiftedMask != 1)
            return;


        _flyerState = FlyerStates.Divebombing;

        _diveBombTarget = playerPos;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + _moveDir);

        Gizmos.DrawLine(transform.position, transform.position + _diveBombRaycastDir);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_diveBombTarget, 1);
    }

}
