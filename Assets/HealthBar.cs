using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Image healthBar;
    public Gradient gradient;

    void Update()
    {
        healthBar.color = gradient.Evaluate(healthBar.fillAmount);
    }
}
