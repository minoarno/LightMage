using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player")]
public class PlayerData : ScriptableObject
{
    public int _maxHealth = 10;
    public float _movementSpeed = 750f;
    public float _fireBallCooldown = 1f;
}
