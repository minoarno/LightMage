using UnityEngine;
using UnityEngine.Tilemaps;

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

        SpawnFireBall();
    }

    private void SpawnFireBall()
    {
        GameObject fireBall = Instantiate(_fireBallPrefab);

        fireBall.transform.position = _attackPosition.position;
        
        //float angleDiff = Vector3.Dot(fireBall.transform.position, mousePosition);
        //fireBall.transform.GetChild(0).LookAt(_attackPosition.position + (mousePosition));
        //fireBall.transform.Rotate(0, 0, angleDiff + 45);

        fireBall.GetComponent<FireBall>().SetTargetPosition(_attackPosition.position, mousePosition);
    }

   // private void OnDrawGizmos()
   // {
   //     Gizmos.DrawLine(_attackPosition.position, mousePosition);
   // }
}
