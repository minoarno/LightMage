using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WalkerStats", menuName = "Enemies/WalkerStats")]
public class WalkerStats : ScriptableObject
{
    public int maxDamage = 1;
    public int maxHealth = 4;
    public float moveSpeed = 10;
    public float maxWayPointDistance = 0.5f;
}
