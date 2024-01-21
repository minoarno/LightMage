using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlatformStats", menuName = "Platforms/PlatformStats")]
public class PlatformStats : ScriptableObject
{
    public float moveSpeed = 10;
    public float distanceTreshold = 5f;
}
