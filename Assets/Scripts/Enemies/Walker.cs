using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Walker : MonoBehaviour
{

    [SerializeField]
    private WalkerStats _stats;

    /// <summary>
    /// Right Point
    /// </summary>
    [SerializeField]
    private Transform _pointA;
    /// <summary>
    /// Left Point
    /// </summary>
    [SerializeField]
    private Transform _pointB;
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

        if (!_pointA || !_pointB)
            return;

        _currentPoint = _pointB;
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

        if (!_pointA || !_pointB)
            return;

        if (!IsGrounded())
            return;


        _rigidbody.velocity = new Vector2(_stats.moveSpeed * _moveSign, 0);

        if (Math.Abs(transform.position.x - _currentPoint.position.x) < _walkerWidthHalved && _currentPoint == _pointA)
        {
            _currentPoint = _pointB;
            _moveSign *= -1;
        }

        if (Math.Abs(transform.position.x - _currentPoint.position.x) < _walkerWidthHalved && _currentPoint == _pointB)
        {
            _currentPoint = _pointA;
            _moveSign *= -1;
        }

    }
}
