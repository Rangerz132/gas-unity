using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class CharacterStatsSlotButtonGUI : MonoBehaviour, IPointerClickHandler
{
    [Header("Stat")]
    [SerializeField] private bool isIncreasing;
    public CharacterStatsSlotGUI characterStatsSlotGUI;
    public Stat stat;

    [Header("Tween ")]
    [SerializeField] private Vector3 initialScaleValue;
    [SerializeField] private float scaleValue;
    [SerializeField] private float scaleDuration;

    [SerializeField] private Image buttonImage;
    [SerializeField] private Color disableTintColor;
    [SerializeField] private Color enableTintColor;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isIncreasing)
        {
            stat.baseValue++;
            AnimateButton();
        }
        else
        {
            if (stat.Value > 0)
            {
                stat.baseValue--;
                AnimateButton();
            }
        }

        characterStatsSlotGUI.SetStatValue();
        characterStatsSlotGUI.SetButtonInteractivity();
    }

    public void DisableButton()
    {
        buttonImage.color = disableTintColor;
    }

    public void EnableButton()
    {
        buttonImage.color = enableTintColor;
    }

    private void AnimateButton()
    {
        transform.DOKill();
        transform.localScale = initialScaleValue;
        transform.DOScale(scaleValue, scaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }
}
