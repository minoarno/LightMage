using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] Transform _attackPosition;
    [SerializeField] Camera _camera;
    [SerializeField] GameObject _fireBallPrefab;
    [SerializeField] PlayerData _playerData;


    private float _currentCooldown = 0;
    Vector3 mousePosition = new Vector3(0, 0, 0);


    // Update is called once per frame
    void Update()
    {
        _currentCooldown += Time.deltaTime;


        if (!Input.GetMouseButtonDown(0) || _currentCooldown < _playerData._fireBallCooldown)
            return;

        _currentCooldown = 0;
        Vector3 mouseInput = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane);
        mousePosition = _camera.ScreenToWorldPoint(mouseInput);
        mousePosition.z = 0;
        SpawnFireBall();
    }

    private void SpawnFireBall()
    {
        GameObject fireBall = Instantiate(_fireBallPrefab);


        Transform fireballTransform = fireBall.transform;
        fireballTransform.position = _attackPosition.position;

        fireballTransform.LookAt(_attackPosition.position + (mousePosition - _attackPosition.position));
        fireballTransform.Rotate(0, -90, 0);

        fireBall.GetComponent<FireBall>().SetTargetPosition(_attackPosition.position, mousePosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(_attackPosition.position, mousePosition);
    }
}
