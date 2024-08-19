using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManaBar : BaseAttributeBar<ManaManager>
{
    private void OnEnable()
    {
        ManaManager.OnAttributeConsume += UpdateAttribute;
        ManaManager.OnAttributeRegen += UpdateAttribute;
        ManaManager.OnMaxAttributeChange += UpdateAttribute;
    }

    private void OnDisable()
    {
        ManaManager.OnAttributeConsume -= UpdateAttribute;
        ManaManager.OnAttributeRegen -= UpdateAttribute;
        ManaManager.OnMaxAttributeChange -= UpdateAttribute;
    }
}


