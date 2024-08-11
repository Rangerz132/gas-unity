using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private TextMeshProUGUI healtText;
    [SerializeField] private Image healtBar;

    private void OnEnable()
    {
        Health.OnTakeDamage += UpdateHealth;
        Health.OnHeal += UpdateHealth;
        Health.OnMaxHealChange += UpdateHealth;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= UpdateHealth;
        Health.OnHeal -= UpdateHealth;
        Health.OnMaxHealChange -= UpdateHealth;
    }

    void Start()
    {
        UpdateHealth();
    }

    /// <summary>
    /// Update the Healt Bar
    /// </summary>
    public void UpdateHealth()
    {
        float fillAmount = health.CurrentHealth / health.MaxHealth;
        healtBar.fillAmount = fillAmount;

        healtText.text = $"{health.CurrentHealth} / {health.MaxHealth}";
    }
}
