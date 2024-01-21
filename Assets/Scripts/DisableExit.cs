using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableExit : MonoBehaviour
{
    [SerializeField] Button _finalButton;

    private bool _isGone = false;
    private void Update()
    {
        if (!_finalButton.pressed || _isGone)
            return;

        _isGone = !_isGone;
        gameObject.GetComponent<BoxCollider2D>().enabled = !_isGone;
        gameObject.GetComponent<SpriteRenderer>().enabled = !_isGone;
    }
}
