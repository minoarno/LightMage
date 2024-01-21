using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Slider _slider;

    public void SetHealth(int health)
    {
        _slider.value = health;
    }
}
