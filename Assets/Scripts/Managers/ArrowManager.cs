using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class ArrowManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _arrowPrefab;

    public static ArrowManager instance;
    private void Awake()
    {

        //Singleton
        if (instance != null)
        {
            Destroy(this);
            return;
        }

        instance = this;

        DontDestroyOnLoad(instance);
    }

    public IEnumerator ShootArrow(Vector3 start, Vector3 apex, Vector3 target)
    {
        GameObject go = Instantiate(_arrowPrefab);
        Vector3 previousPos = start;
        float distanceTraveled = 0;
        while (distanceTraveled < 1)
        {
            if (target == null)
            {
                Destroy(go);
                yield break;

            }

            distanceTraveled += Time.deltaTime;

            Vector3 pointA = Vector3.Lerp(start, apex, distanceTraveled);
            Vector3 pointB = Vector3.Lerp(apex, target, distanceTraveled);
            Vector3 currentPosition = Vector3.Lerp(pointA, pointB, distanceTraveled);

            if(go != null)
            {
                go.transform.position = currentPosition;

                go.transform.LookAt(currentPosition + (currentPosition - previousPos));

                go.transform.Rotate(0, -90, 0);

                previousPos = currentPosition;

            }
            yield return null;
        }

        if(go != null)
            go.transform.position = target;
        
        yield return new WaitForSeconds(1.0f);

        Destroy(go);
    }

}
