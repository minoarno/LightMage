using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]
public class FireBall : MonoBehaviour
{
    [SerializeField] FireSpellData _spellData;

    private float _currentFireBallLifeTime;
    private Rigidbody2D _rigidBody;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _currentFireBallLifeTime += Time.deltaTime;

        if(_currentFireBallLifeTime >= _spellData._fireBallLifeTime)
            Destroy(gameObject);

    }

    public void SetTargetPosition(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 travelVector = (endPosition - startPosition).normalized;
        Vector3 fireballVelocity = travelVector * _spellData._fireBallSpeed;
        _rigidBody.velocity = fireballVelocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == null)
            return;

        Destroy(gameObject);

        int bitshiftedMask = LayerMask.GetMask("Enemy") >> collision.collider.gameObject.layer;

        if (bitshiftedMask != 1)
            return;

        //DO damage to enemy
        IEnemy enemy = collision.gameObject.GetComponent(typeof(IEnemy)) as IEnemy;

        if(enemy != null)
            enemy.DoDamage(_spellData._damage);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector2(transform.position.x, transform.position.y) + _rigidBody.velocity);
    }

}
