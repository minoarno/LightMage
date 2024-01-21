using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Transform camTransform;
    [SerializeField] Transform _startPosition;
    [SerializeField] float relativeMove = .3f;
    [SerializeField] bool lockY = false;

    private void Start()
    {
        _startPosition.position = transform.position;
        transform.position = camTransform.position;
        Debug.Log(camTransform.position);
    }
    void Update()
    {
        if (lockY)
            transform.position = new Vector2(camTransform.position.x * relativeMove, camTransform.position.y * relativeMove);
        else
            transform.position = new Vector2(camTransform.position.x * relativeMove, camTransform.position.y);
    }
}
