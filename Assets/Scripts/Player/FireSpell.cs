using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof(CircleCollider2D))]
public class FireBall : MonoBehaviour
{
    [SerializeField] private float _fireBallSpeed = 5f;
    [HideInInspector] public bool _isMarkForDelete = false;

    private Rigidbody2D _rigidBody;
    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void SetTargetPosition(Vector3 startPosition, Vector3 endPosition)
    {
        Debug.Log("Fireball start position " + startPosition);

        Vector3 travelVector = (endPosition - startPosition).normalized;
        Vector3 fireballVelocity = travelVector * _fireBallSpeed;
        _rigidBody.velocity = fireballVelocity;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + new Vector3(_rigidBody.velocity.x, _rigidBody.velocity.y, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null)
            return;

        _isMarkForDelete = true;

        //Value 8 is the 9th layer, in this case that's an enemy
        if (collision.gameObject.layer == 8)
        {
            //TO DO: Do Damage to the enemy
        }
    }

}
