using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    // Store reference to the waypoint system
    [SerializeField] private PlatformWaypoints _waypoints;
    [SerializeField] private PlatformStats _stats;

    private Transform _currentWaypoint;
    private Rigidbody2D _rigidbody;


    private Vector3 _moveDir;

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

    }

    // Update is called once per frame
    void Update()
    {
        _moveDir = (_currentWaypoint.position - transform.position).normalized * _stats.moveSpeed * Time.deltaTime;

        _rigidbody.velocity = _moveDir;
        if (Vector2.Distance(transform.position, _currentWaypoint.position) < _stats.distanceTreshold)
        {
            _currentWaypoint = _waypoints.GetNextWayPoint(_currentWaypoint);
        }
    }
}
