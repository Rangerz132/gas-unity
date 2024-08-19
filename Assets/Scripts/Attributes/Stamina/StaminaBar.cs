using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : MonoBehaviour
{
    [SerializeField] private StaminaManager staminaManager;
    [SerializeField] private TextMeshProUGUI staminaText;
    [SerializeField] private Image staminaBar;

    private void OnEnable()
    {
        StaminaManager.OnStaminaConsume += UpdateStamina;
        StaminaManager.OnStaminaRegen += UpdateStamina;
        StaminaManager.OnMaxStaminaChange += UpdateStamina;
    }

    private void OnDisable()
    {
        StaminaManager.OnStaminaConsume -= UpdateStamina;
        StaminaManager.OnStaminaRegen -= UpdateStamina;
        StaminaManager.OnMaxStaminaChange -= UpdateStamina;
    }

    void Start()
    {
        UpdateStamina();
    }

    /// <summary>
    /// Update the StaminaManager Bar
    /// </summary>
    public void UpdateStamina()
    {
        float fillAmount = staminaManager.CurrentStamina / staminaManager.MaxStamina;
        staminaBar.fillAmount = fillAmount;

        staminaText.text = $"{Mathf.Ceil(staminaManager.CurrentStamina)} / {staminaManager.MaxStamina} SP";
    }
}

