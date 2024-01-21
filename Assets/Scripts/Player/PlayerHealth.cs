using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    int _currentHealth;

    private void Start()
    {
        _currentHealth = _playerData._maxHealth;
    }
    public void TakeDamage(int damage)
    {
        if (damage == 0)
            return;

        _currentHealth -= damage;
        Debug.Log("Player health left: " + _currentHealth);

        if (_currentHealth > 0)
            return;

        Destroy(gameObject);
    }

}
