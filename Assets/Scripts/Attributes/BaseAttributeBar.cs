using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class BaseAttributeBar<T> : MonoBehaviour where T : BaseAttributeManager
{
    [SerializeField] protected T attributeManager;
    [SerializeField] protected TextMeshProUGUI attributeText;
    [SerializeField] protected Image attributeBar;
    [SerializeField] protected string attributeName;

    void Start()
    {
        UpdateAttribute();
    }

    /// <summary>
    /// Update the attribute bar
    /// </summary>
    public void UpdateAttribute()
    {
        float fillAmount = attributeManager.CurrentAttribute / attributeManager.MaxAttribute;
        attributeBar.fillAmount = fillAmount;

        attributeText.text = $"{Mathf.Ceil(attributeManager.CurrentAttribute)} / {attributeManager.MaxAttribute} {attributeName}";
    }
}


