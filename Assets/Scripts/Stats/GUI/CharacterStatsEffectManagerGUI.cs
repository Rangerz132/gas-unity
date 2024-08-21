using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatsEffectManagerGUI : MonoBehaviour
{
    [SerializeField] private CharacterStatsEffectGUI characterStatsEffectGUI;
    [SerializeField] private GameObject visual;
    private List<CharacterStatsEffectGUI> characterStatsEffectGUIList = new List<CharacterStatsEffectGUI>();

    private void OnEnable()
    {
        CharacterStatsSlotDetailsGUI.OnCharacterStatsSlotDetailsEnter += CreateEffects;
        CharacterStatsSlotDetailsGUI.OnCharacterStatsSlotDetailsExit += DestroyEffects;
        CharacterStatsSlotDetailsGUI.OnCharacterStatsSlotDetailsMove += SetPosition;
    }

    private void OnDisable()
    {
        CharacterStatsSlotDetailsGUI.OnCharacterStatsSlotDetailsEnter -= CreateEffects;
        CharacterStatsSlotDetailsGUI.OnCharacterStatsSlotDetailsExit -= DestroyEffects;
        CharacterStatsSlotDetailsGUI.OnCharacterStatsSlotDetailsMove -= SetPosition;
    }

    public void SetPosition(Vector2 position)
    {
        transform.localPosition = position;
    }

    public void CreateEffects(CharacterStat characterStat, Vector2 position)
    {
        SetPosition(position);

        for (var i = 0; i < characterStat.derivedStatEffects.Count; i++)
        {
            // Instantiate effect
            CharacterStatsEffectGUI characterStatsEffectGUIInstance = Instantiate(characterStatsEffectGUI);
            characterStatsEffectGUIInstance.transform.SetParent(gameObject.transform);

            // Set effect
            characterStatsEffectGUIInstance.SetEffect(characterStat.derivedStatEffects[i]);

            // Add effect to the list
            characterStatsEffectGUIList.Add(characterStatsEffectGUIInstance);
        }

        if (characterStat.derivedStatEffects.Count > 0)
        {
            Show();
        }
    }

    public void DestroyEffects()
    {
        // Destroy all effects
        for (var i = 0; i < characterStatsEffectGUIList.Count; i++)
        {
            Destroy(characterStatsEffectGUIList[i].gameObject);
        }

        // Clear the effect list
        characterStatsEffectGUIList.Clear();

        Hide();
    }

    public void Show()
    {
        visual.SetActive(true);
    }

    public void Hide()
    {
        visual.SetActive(false);
    }
}
