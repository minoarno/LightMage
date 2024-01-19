using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] CharacterController2D characterController;
    [SerializeField] private float _MovementSpeed = 40f;

    private float _HorizontalMovement = 0f;
    private bool _IsJumping = false;

    // Update is called once per frame
    void Update()
    {
        _HorizontalMovement = Input.GetAxisRaw("Horizontal") * _MovementSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump"))
            _IsJumping = true;
    }

    private void FixedUpdate()
    {
        characterController.Move(_HorizontalMovement, false, _IsJumping);
        _IsJumping = false;
    }
}
