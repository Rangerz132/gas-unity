using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : BaseAttributeBar<HealthManager>
{
    private void OnEnable()
    {
        HealthManager.OnTakeDamage += UpdateAttribute;
        HealthManager.OnHeal += UpdateAttribute;
        HealthManager.OnMaxAttributeChange += UpdateAttribute;
    }

    private void OnDisable()
    {
        HealthManager.OnTakeDamage -= UpdateAttribute;
        HealthManager.OnHeal -= UpdateAttribute;
        HealthManager.OnMaxAttributeChange -= UpdateAttribute;
    }
}
