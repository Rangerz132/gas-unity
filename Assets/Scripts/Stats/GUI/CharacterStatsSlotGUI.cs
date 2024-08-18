using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStatsSlotGUI : MonoBehaviour
{
    private CharacterStat characterStat;
    [SerializeField] private TextMeshProUGUI statName;
    [SerializeField] private TextMeshProUGUI statValue;
    [SerializeField] private CharacterStatsSlotButtonGUI characterStatsButtonDecrease;
    [SerializeField] private CharacterStatsSlotButtonGUI characterStatsButtonIncrease;


    public void SetCharacterStatsInfo(CharacterStatType characterStatType, CharacterStat characterStat)
    {
        this.characterStat = characterStat;

        // Update text value
        statName.text = characterStatType.ToString();
        statValue.text = characterStat.stat.Value.ToString();

        // Update button 
        characterStatsButtonDecrease.stat = characterStat.stat;
        characterStatsButtonIncrease.stat = characterStat.stat;
        characterStatsButtonDecrease.characterStatsSlotGUI = this;
        characterStatsButtonIncrease.characterStatsSlotGUI = this;
        SetButtonInteractivity();

    }

    public void SetStatValue()
    {
        statValue.text = characterStat.stat.Value.ToString();
    }

    public void SetButtonInteractivity()
    {
        if (characterStat.stat.Value <= 0)
        {
            characterStatsButtonDecrease.DisableButton();
        }
        else
        {
            characterStatsButtonDecrease.EnableButton();
        }
    }
}
