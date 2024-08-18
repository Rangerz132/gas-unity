using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthManager healthManager;
    [SerializeField] private TextMeshProUGUI healtText;
    [SerializeField] private Image healtBar;

    private void OnEnable()
    {
        HealthManager.OnTakeDamage += UpdateHealth;
        HealthManager.OnHeal += UpdateHealth;
        HealthManager.OnMaxHealChange += UpdateHealth;
    }

    private void OnDisable()
    {
        HealthManager.OnTakeDamage -= UpdateHealth;
        HealthManager.OnHeal -= UpdateHealth;
        HealthManager.OnMaxHealChange -= UpdateHealth;
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
        float fillAmount = healthManager.CurrentHealth / healthManager.MaxHealth;
        healtBar.fillAmount = fillAmount;

        healtText.text = $"{healthManager.CurrentHealth} / {healthManager.MaxHealth}";
    }
}
