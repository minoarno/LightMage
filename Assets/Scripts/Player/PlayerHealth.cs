using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] PlayerData _playerData;
    [SerializeField] Healthbar _healthBar;
    [SerializeField] GameObject _restartButton;
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

        _healthBar.SetHealth(_currentHealth);

        if (_currentHealth > 0)
            return;
        
        _restartButton.SetActive(true);
        Destroy(gameObject);
    }

}
