using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityInfoGUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI abilityName;
    [SerializeField] private TextMeshProUGUI abilityDescription;

    void Update() { }

    public void UpdateAbilityData(Ability ability)
    {
        abilityName.text = ability.displayName;
        abilityDescription.text = ability.description;
    }

    public void ShowGUI()
    {
        gameObject.SetActive(true);
    }

    public void HideGUI() 
    { 
        gameObject.SetActive(false); 
    }
}
