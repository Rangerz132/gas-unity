using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatsEffectGUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI effectDescriptionText;
    [SerializeField] private TextMeshProUGUI effectDescriptionDataText;
    [SerializeField] private Color effectDescriptionDataTextColor;

    public void SetEffect(DerivedCharacterStatEffect derivedCharacterStatEffect)
    {
        effectDescriptionText.text = derivedCharacterStatEffect.description;
    }
}
