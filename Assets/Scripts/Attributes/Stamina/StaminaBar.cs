using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StaminaBar : BaseAttributeBar<StaminaManager>
{
    private void OnEnable()
    {
        StaminaManager.OnAttributeConsume += UpdateAttribute;
        StaminaManager.OnAttributeRegen += UpdateAttribute;
        StaminaManager.OnMaxAttributeChange += UpdateAttribute;
    }

    private void OnDisable()
    {
        StaminaManager.OnAttributeConsume -= UpdateAttribute;
        StaminaManager.OnAttributeRegen -= UpdateAttribute;
        StaminaManager.OnMaxAttributeChange -= UpdateAttribute;
    }
}

