using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null) return;

        int bitshiftedMask = LayerMask.GetMask("Player") >> collision.gameObject.layer;

        if (bitshiftedMask != 1)
            return;

        SceneManager.LoadScene("GameEndScene");
    }
}
