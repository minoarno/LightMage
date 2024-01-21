using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform _player;

    public Vector3 offset = new Vector3(0, 2, -10);
    public float smoothTime = 0.25f;
    Vector3 currentVelocity;

    private void LateUpdate()
    {
        if (_player == null)
            return;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            _player.position + offset,
            ref currentVelocity,
            smoothTime
            );
    }
}
