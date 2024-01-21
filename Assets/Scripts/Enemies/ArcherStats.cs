using UnityEngine;

[CreateAssetMenu(fileName = "ArcherStats", menuName = "Enemies/ArcherStats")]
public class ArcherStats : ScriptableObject
{
    public int maxDamage = 2;
    public int maxHealth = 5;
    public float maxShootCooldown = 2f;
    public float midwayPointOffset = 30;
    public float awakeDistance = 10;
}
