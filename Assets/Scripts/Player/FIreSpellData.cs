using UnityEngine;

[CreateAssetMenu(fileName = "SpellData", menuName = "ScriptableObjects/Spells/FireBall")]
public class FireSpellData : ScriptableObject
{
    public float _fireBallSpeed = 5f;
    public float _fireBallLifeTime = 5f;
    public int _damage = 5;
}
