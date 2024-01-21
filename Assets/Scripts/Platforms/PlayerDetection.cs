using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        var playerController = collider.GetComponent<CharacterController2D>();

        if (!playerController)
            return;

        playerController.SetParent(transform);

    }


    private void OnTriggerExit2D(Collider2D collider)
    {
        var playerController = collider.GetComponent<CharacterController2D>();

        if (!playerController)
            return;

        playerController.ResetParent();
    }
}
