using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class CharacterStatsSlotDetailsGUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    private CharacterStat characterStat;
    public static Action<CharacterStat, Vector2> OnCharacterStatsSlotDetailsEnter;
    public static Action<Vector2> OnCharacterStatsSlotDetailsMove;
    public static Action OnCharacterStatsSlotDetailsExit;
    public Vector2 offset;

    public void SetCharacterStat(CharacterStat characterStat)
    {
        this.characterStat = characterStat;
    }

    public Vector2 GetMousePosition(PointerEventData eventData)
    {
        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponentInParent<Canvas>().transform as RectTransform,
            Input.mousePosition,
          null,
            out position);

        return position + offset;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnCharacterStatsSlotDetailsEnter?.Invoke(characterStat, GetMousePosition(eventData));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnCharacterStatsSlotDetailsExit?.Invoke();
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        OnCharacterStatsSlotDetailsMove?.Invoke(GetMousePosition(eventData));
    }
}
