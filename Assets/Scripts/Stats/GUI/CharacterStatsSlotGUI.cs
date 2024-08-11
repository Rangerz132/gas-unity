using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatsSlotGUI : MonoBehaviour
{
    private CharacterStatsManagerGUI characterStatsManagerGUI;
    private Stat stat;
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statValue;
    [SerializeField] private CharacterStatsSlotButtonGUI characterStatsButtonDecrease;
    [SerializeField] private CharacterStatsSlotButtonGUI characterStatsButtonIncrease;


    public void SetCharacterStatsInfo(CharacterStatsManagerGUI characterStatsManagerGUI, CharacterStatType characterStatType, Stat stat)
    {
        // Set stat
        this.characterStatsManagerGUI = characterStatsManagerGUI;
        this.stat = stat;

        // Update text value
        statName.text = characterStatType.ToString();
        statValue.text = stat.Value.ToString();

        // Update button 
        characterStatsButtonDecrease.stat = stat;
        characterStatsButtonIncrease.stat = stat;
        characterStatsButtonDecrease.characterStatsSlotGUI = this;
        characterStatsButtonIncrease.characterStatsSlotGUI = this;
        SetButtonInteractivity();

    }

    public void SetStatValue()
    {
        statValue.text = stat.Value.ToString();
    }

    public void SetButtonInteractivity()
    {
        if (stat.Value <= 0)
        {
            characterStatsButtonDecrease.DisableButton();
        }
        else
        {
            characterStatsButtonDecrease.EnableButton();
        }

    }

    public void updateStatEffect()
    {
        characterStatsManagerGUI.characterStats.ApplyAllEffects();
    }
}
