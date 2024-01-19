using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/Player")]
public class PlayerData : ScriptableObject
{
    public int currentHealth = 10;
    public float _movementSpeed = 750f;
}
