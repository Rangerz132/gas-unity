using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AbilitySlotGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Ability ability;
    public float abilityCooldownTime;
    [SerializeField] private AbilityInfoGUI abilityInfoGUI;
    [SerializeField] private Image icon;
    [SerializeField] private Image overlayCooldownIcon;
    
    void Update() { }

    /// <summary>
    /// Set the Ability Slot Icon image
    /// </summary>
    /// <param name="abilityIcon"></param>
    public void SetAbilityIcon(Sprite abilityIcon)
    {
        this.icon.sprite = abilityIcon;
    }

    /// <summary>
    /// Set the Ability Slot Overlay filled image value
    /// </summary>
    /// <param name="value"></param>
    public void SetAbilityCooldownOverlay(float value)
    {
        overlayCooldownIcon.fillAmount = value;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        abilityInfoGUI.UpdateAbilityData(ability);
        abilityInfoGUI.ShowGUI();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        abilityInfoGUI.gameObject.SetActive(false);
        abilityInfoGUI.HideGUI();
    }
}
