using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WorldBounds : MonoBehaviour
{
    [HideInInspector]
    public Bounds bounds;
    void Awake()
    {
        bounds = GetComponent<BoxCollider2D>().bounds;
    }
}
