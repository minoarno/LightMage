using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] private float _cameraDepthDistance = -15f;
    // Update is called once per frame
    void Update()
    {
        Vector3 currentPos = new Vector3(_player.transform.position.x, _player.transform.position.y, _cameraDepthDistance);
        transform.position = currentPos;
    }
}
