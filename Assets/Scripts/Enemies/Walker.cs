using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Walker : MonoBehaviour
{
    [SerializeField]
    private float _moveSpeed = 10;

    /// <summary>
    /// Right Point
    /// </summary>
    public GameObject pointA;
    /// <summary>
    /// Left Point
    /// </summary>
    public GameObject pointB;



    private Transform _currentPoint;
    private Rigidbody2D _rigidbody;

    /// <summary>
    ///  used to show left of right
    ///  either -1 or 1
    /// </summary>
    private int _moveSign = 1;

    private float _walkerWidthHalved;




    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();

        if (!pointA || !pointB)
            return;

        _currentPoint = pointB.transform;
        _walkerWidthHalved = GetComponent<BoxCollider2D>().size.x;
        

    }


    private void OnDrawGizmosSelected()
    {
        
    }

    bool IsGrounded()
    {
        return _rigidbody.velocity.y == 0;
    }

    // Update is called once per frame
    void Update()
    {

        if (!pointA || !pointB)
            return;

        if (!IsGrounded())
            return;

        Vector2 toGoPoint = _currentPoint.position - transform.position;

        _rigidbody.velocity = new Vector2(_moveSpeed * _moveSign, 0);



        if (Math.Abs(transform.position.x - _currentPoint.position.x) < _walkerWidthHalved && _currentPoint == pointA.transform)
        {
            _currentPoint = pointB.transform;
            _moveSign *= -1;
        }

        if (Math.Abs(transform.position.x - _currentPoint.position.x) < _walkerWidthHalved && _currentPoint == pointB.transform)
        {
            _currentPoint = pointA.transform;
            _moveSign *= -1;
        }

    }
}
