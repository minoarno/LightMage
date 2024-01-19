using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2D _characterController;
    [SerializeField] PlayerData _playerData;

    private float _horizontalMovement = 0f;
    private bool _isJumping = false;

    // Update is called once per frame
    void Update()
    {
        
        _horizontalMovement = Input.GetAxisRaw("Horizontal") * _playerData._movementSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
            _isJumping = true;
    }

    private void FixedUpdate()
    {
        _characterController.Move(_horizontalMovement, false, _isJumping);
        _isJumping = false;
    }
}
