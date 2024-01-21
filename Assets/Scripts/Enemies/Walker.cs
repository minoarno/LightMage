using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Walker : MonoBehaviour, IEnemy
{
    [SerializeField]
    private WalkerStats _stats;

    [SerializeField]
    private Transform _pointA;
    [SerializeField]
    private Transform _pointB;

    [SerializeField]
    private SpriteRenderer _spriteRender;
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


        if (_pointB.position.x < _pointA.position.x)
            _spriteRender.flipX = false;
        else
            _spriteRender.flipX = true;

        _currentPoint = _pointB;
        _walkerWidthHalved = GetComponent<BoxCollider2D>().size.x;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_pointA.position, 0.5f);
        Gizmos.DrawWireSphere(_pointB.position, 0.5f);




        if (!_currentPoint)
            return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_currentPoint.position, 0.5f);

        if (!_rigidbody)
            return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_rigidbody.velocity.x, _rigidbody.velocity.y));
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


        _rigidbody.velocity = (_currentPoint.position - transform.position) * _stats.moveSpeed;
        //ignore y
        _rigidbody.velocity *= Vector2.right;
        //normalize to keep consistant
        _rigidbody.velocity = _rigidbody.velocity.normalized;

        if (Math.Abs(transform.position.x - _currentPoint.position.x) < _stats.maxWayPointDistance && _currentPoint == _pointA)
        {
            _currentPoint = _pointB;
            _moveSign *= -1;
            _spriteRender.flipX = !_spriteRender.flipX;
        }

        if (Math.Abs(transform.position.x - _currentPoint.position.x) < _stats.maxWayPointDistance && _currentPoint == _pointB)
        {
            _currentPoint = _pointA;
            _moveSign *= -1;
            _spriteRender.flipX = !_spriteRender.flipX;
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
