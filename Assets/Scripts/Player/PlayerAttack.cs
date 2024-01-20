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
        Vector3 blah = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _camera.nearClipPlane);
        mousePosition = _camera.ScreenToWorldPoint(blah);
        //Debug.Log("Screen to world pos " + mousePosition);

        if (!Input.GetMouseButtonDown(0))
            return;


        if (!Physics2D.Raycast(mousePosition, Vector2.zero))
            return;

        Debug.Log("Hit something");

        if (hit.collider == null)
            return;

        Debug.Log("Object hit: " + hit.collider.gameObject.name);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(_attackPosition.position, mousePosition);
    }
}
