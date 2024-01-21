using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Walker : MonoBehaviour, IEnemy
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

    private int _currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _currentHealth = _stats.maxHealth;
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

        if (Math.Abs(transform.position.x - _currentPoint.position.x) <  _stats.maxWayPointDistance && _currentPoint == _pointA)
        {
            _currentPoint = _pointB;
            _moveSign *= -1;
        }

        if (Math.Abs(transform.position.x - _currentPoint.position.x) < _stats.maxWayPointDistance && _currentPoint == _pointB)
        {
            _currentPoint = _pointA;
            _moveSign *= -1;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        int bitshiftedMask = LayerMask.GetMask("Player") >> collision.collider.gameObject.layer;

        if (bitshiftedMask != 1)
            return;

        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(_stats.maxDamage);
    }

    void IEnemy.DoDamage(int damage)
    {
        if (damage == 0)
            return;

        _currentHealth -= damage;

        if (_currentHealth >= 0)
            return;

        Destroy(gameObject);
    }

}
