using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var playerHealth = collision.GetComponent<PlayerHealth>();

        if (!playerHealth)
            return;

        playerHealth.TakeDamage(int.MaxValue);
    }
}
