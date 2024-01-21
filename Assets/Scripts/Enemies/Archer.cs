using UnityEngine;

public class Archer : MonoBehaviour, IEnemy
{

    [SerializeField]
    [Range(0.001f, 1)]
    private float _timestep = 0.1f;

    [SerializeField]
    private float _shootCooldown = 0f;

    [SerializeField]
    private ArcherStats _stats;

    int _currentHealth;

    private LayerMask _toHitLayerMask;

    private Vector3 _shootTarget;
    private Vector3 _shootRaycastDir;

    // Start is called before the first frame update
    void Start()
    {
        _currentHealth = _stats.maxHealth;

        _toHitLayerMask = LayerMask.GetMask("Player", "Terrain");
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerManager.instance || !PlayerManager.instance.GetPlayerTransform())
            return;


        _shootCooldown += Time.deltaTime;

        if (_shootCooldown < _stats.maxShootCooldown)
            return;


        Vector3 playerPos = PlayerManager.instance.GetPlayerTransform().position;

        if (Vector3.Distance(transform.position, playerPos) > _stats.awakeDistance)
            return;

        _shootCooldown = 0;

        _shootRaycastDir = playerPos - transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, _shootRaycastDir, float.MaxValue, _toHitLayerMask);

        if (!hit)
            return;

        int bitshiftedMask = LayerMask.GetMask("Player") >> hit.collider.gameObject.layer;

        if (bitshiftedMask != 1)
            return;

        Vector3 midwayPoint, endPoint;
        CalculateMidwayPoint(playerPos, out midwayPoint, out endPoint);

        ArrowManager.instance.StartShootArrow(transform.position, midwayPoint, endPoint);
    }

    private void CalculateMidwayPoint(Vector3 playerPos, out Vector3 midwayPoint, out Vector3 endPoint)
    {

        RaycastHit2D hit = Physics2D.Raycast(playerPos, Vector2.down, float.MaxValue, ~LayerMask.GetMask("Player"));


        if (hit.collider != null)
            endPoint = hit.point;
        else
            endPoint = playerPos;

        midwayPoint = Vector3.Lerp(transform.position, playerPos, 0.5f);
        midwayPoint.y = playerPos.y + _stats.midwayPointOffset;
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _stats.awakeDistance);


        if (!PlayerManager.instance || !PlayerManager.instance.GetPlayerTransform())
            return;


        Gizmos.DrawLine(transform.position, transform.position + _shootRaycastDir);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_shootTarget, 1);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.2f);

        Gizmos.DrawWireSphere(PlayerManager.instance.GetPlayerTransform().position, 0.2f);


        DrawArc();


    }

    private void DrawArc()
    {
        Vector3 previousP = transform.position;
        Vector3 pointA;
        Vector3 pointB;
        Vector3 currentP;

        Vector3 playerPos = PlayerManager.instance.GetPlayerTransform().position;
        Vector3 midwayPoint, endPoint;
        CalculateMidwayPoint(playerPos, out midwayPoint, out endPoint);

        for (float t = 0; t <= 1; t += _timestep)
        {

            pointA = Vector3.Lerp(transform.position, midwayPoint, t);
            pointB = Vector3.Lerp(midwayPoint, endPoint, t);
            currentP = Vector3.Lerp(pointA, pointB, t);

            Gizmos.DrawLine(previousP, currentP);
            previousP = currentP;
        }

        Gizmos.DrawLine(previousP, endPoint);
    }

    void IEnemy.DoDamage(int damage)
    {
        if (damage == 0)
            return;

        _currentHealth -= damage;

        if (_currentHealth > 0)
            return;

        Destroy(gameObject);
    }
}
