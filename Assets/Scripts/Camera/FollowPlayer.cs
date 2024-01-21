using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] WorldBounds _worldBounds;

    public Vector3 offset = new Vector3(0, 2, -10);
    public float smoothTime = 0.25f;


    private Vector3 _currentVelocity;
    private Vector3 _targetPosition;
    private Camera _camera;

    private void Awake() => _camera = Camera.main;

    private Bounds _cameraBounds;


    private void Start()
    {
        float height = _camera.orthographicSize;
        float width = height * _camera.aspect;


        float minX = _worldBounds.bounds.min.x + width;
        float maxX = _worldBounds.bounds.max.x - width;
        float minY = _worldBounds.bounds.min.y + height;
        float maxY = _worldBounds.bounds.max.y - height;


        _cameraBounds = new Bounds();

        _cameraBounds.SetMinMax(new Vector2(minX, minY), new Vector2(maxX, maxY));

    }

    private void LateUpdate()
    {
        if (_player == null)
            return;


        _targetPosition = Vector3.SmoothDamp(
            transform.position,
            _player.position + offset,
            ref _currentVelocity,
            smoothTime
            );

        _targetPosition = GetCameraBounds();
        transform.position = _targetPosition;
    }


    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(_targetPosition.x, _cameraBounds.min.x, _cameraBounds.max.x),
            Mathf.Clamp(_targetPosition.y, _cameraBounds.min.y, _cameraBounds.max.y),
            transform.position.z);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(_targetPosition, 0.5f);
    }
}
