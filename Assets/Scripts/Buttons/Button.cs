using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{

    [SerializeField]
    private Sprite _onSprite;

    [SerializeField]
    private Button _previousButton;

    [SerializeField]
    private Button _nextButton;

    private SpriteRenderer _crystalRenderer;

    [SerializeField]
    private GameObject _crystalVisuals;

    [SerializeField]
    private GameObject _beamVisuals;

    public Transform buttonBase;


    public bool pressed = false;

    void Start()
    {
        _crystalRenderer = _crystalVisuals.GetComponent<SpriteRenderer>();

        if (_crystalRenderer == null)
        {

            Debug.LogError($"No Sprite Renderer on : {_crystalVisuals.name}");
            return;
        }

        if (!pressed)
            _beamVisuals.SetActive(false);
        else
            _crystalRenderer.sprite = _onSprite;

        if (_nextButton)
            _beamVisuals.transform.LookAt(_beamVisuals.transform.position + (_nextButton.buttonBase.position - _beamVisuals.transform.position));
        _beamVisuals.transform.Rotate(0, -90, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (pressed)
            return;

        if (!_previousButton)
            return;

        if (!_previousButton.pressed)
            return;

        pressed = true;
        _crystalRenderer.sprite = _onSprite;
        _beamVisuals.SetActive(true);
    }

    private void OnDrawGizmos()
    {
        if (pressed) Gizmos.color = Color.green;
        else Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(buttonBase.position, 0.5f);

        if (!_nextButton)
            return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(buttonBase.position, _nextButton.buttonBase.position - buttonBase.position);
    }
}
