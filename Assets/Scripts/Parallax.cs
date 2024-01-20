using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Transform camTransform;
    [SerializeField] float relativeMove = .3f;
    [SerializeField] bool lockY = false;


    void Update()
    {
        if (lockY)
            transform.position = new Vector2(camTransform.position.x * relativeMove, camTransform.position.y * relativeMove);
        else
            transform.position = new Vector2(camTransform.position.x * relativeMove, camTransform.position.y);
    }
}
