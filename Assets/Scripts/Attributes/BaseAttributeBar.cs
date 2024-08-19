using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseAttributeBar<T> : MonoBehaviour 
{
    [SerializeField] protected T attributeManager;
    [SerializeField] protected TextMeshProUGUI attributeText;
    [SerializeField] protected Image attributeBar;
    [SerializeField] protected string attributetName;


    public T GetT(T genericAttribute)
    {
        return genericAttribute;
    }
    void Start()
    {
        UpdateAttribute();
    }

    /// <summary>
    /// Update the Healt Bar
    /// </summary>
    public void UpdateAttribute()
    {
        //float fillAmount = attributeManager.CurrentHealth / attributeManager.MaxHealth;
        //attributeBar.fillAmount = fillAmount;

        //attributeText.text = $"{attributeManager.CurrentHealth} / {attributeManager.MaxHealth} {attributetName}";
    }
}


