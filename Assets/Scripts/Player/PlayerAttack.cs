using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] FireBallManager _fireBallManager;
    [SerializeField] Transform _attackPosition;
    [SerializeField] Camera _camera;

    //Debug
    RaycastHit2D hit;
    Vector3 mousePosition = new Vector3(0, 0, 0);
    Vector3 colliderHitPosition = new Vector3(0, 0, 0);
    //Vector3Int collisionPositionInt = new Vector3Int(0, 0, 0);


    // Update is called once per frame
    void Update()
    {
        Vector3 mouseInput = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane);
        mousePosition = _camera.ScreenToWorldPoint(mouseInput);
        //Debug.Log("Screen to world pos " + mousePosition);

        if (!Input.GetMouseButtonDown(0))
            return;


        _fireBallManager.SpawnFireBall(_attackPosition.position, mousePosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_attackPosition.position, mousePosition);
    }
}
