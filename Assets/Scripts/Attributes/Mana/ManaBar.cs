using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBar : MonoBehaviour
{
    [SerializeField] private ManaManager manaManager;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private Image manaBar;

    private void OnEnable()
    {
        ManaManager.OnManaConsume += UpdateMana;
        ManaManager.OnManaRegen += UpdateMana;
        ManaManager.OnMaxManaChange += UpdateMana;
    }

    private void OnDisable()
    {
        ManaManager.OnManaConsume -= UpdateMana;
        ManaManager.OnManaRegen -= UpdateMana;
        ManaManager.OnMaxManaChange -= UpdateMana;
    }

    void Start()
    {
        UpdateMana();
    }

    /// <summary>
    /// Update the Mana Bar
    /// </summary>
    public void UpdateMana()
    {
        float fillAmount = manaManager.CurrentMana / manaManager.MaxMana;
        manaBar.fillAmount = fillAmount;

        manaText.text = $"{Mathf.Ceil(manaManager.CurrentMana)} / {manaManager.MaxMana} MP";
    }
}

