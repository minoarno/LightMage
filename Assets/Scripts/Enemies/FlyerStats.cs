using UnityEngine;

[CreateAssetMenu(fileName = "FlyerStats", menuName = "Enemies/FlyerStats")]

public class FlyerStats : ScriptableObject
{
    public float moveSpeed = 10;
    public float distanceTreshold = 5f;
    public float divebombChance = 0.02f;
    public float minDivebombDelay = 7;
    public int maxHealth = 5;
    public int damage = 2;
}
