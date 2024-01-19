using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;


[RequireComponent(typeof(Rigidbody2D))]
public class Flyer : MonoBehaviour
{
    //stats
    public float moveSpeed = 10;
    public float distanceTreshold = 5f;

    // Store reference to the waypoint system
    [SerializeField] private FlyerWaypoints _waypoints;

    //[SerializeField]
    //private Transform _target { get; set; }

    private bool _isMovingToWaypoint = false;

    private Transform _currentWaypoint;
    private Rigidbody2D _rigidbody;

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

        _rigidbody.velocity = (_currentWaypoint.position - transform.position).normalized * moveSpeed * Time.deltaTime; 

        if (Vector2.Distance(transform.position, _currentWaypoint.position) < distanceTreshold)
        {
            _currentWaypoint = _waypoints.GetNextWayPoint(_currentWaypoint);
        }
    }


}
