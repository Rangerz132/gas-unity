using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatsEffectGUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI effectDescriptionText;
    [SerializeField] private TextMeshProUGUI effectDescriptionDataText;

    public void SetEffect(DerivedCharacterStatEffect derivedCharacterStatEffect)
    {
        effectDescriptionText.text = derivedCharacterStatEffect.description;
        effectDescriptionDataText.text = derivedCharacterStatEffect.descriptionData.Replace("${}", derivedCharacterStatEffect.effectPerPoint.ToString());
        effectDescriptionDataText.color = derivedCharacterStatEffect.descriptionDataColor;
    }
}
