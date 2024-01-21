using UnityEngine;

public class ArrowCollision : MonoBehaviour
{
    [SerializeField] ArcherStats _archerStats;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == null) return;

        int bitshiftedMask = LayerMask.GetMask("Spell") >> collision.collider.gameObject.layer;
        if (bitshiftedMask == 1)
            Destroy(gameObject);


        bitshiftedMask = LayerMask.GetMask("Player") >> collision.collider.gameObject.layer;
        if (bitshiftedMask != 1)
            return;

        PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(_archerStats.maxDamage);
        Destroy(gameObject);
    }
}
