using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public enum HealthPopType
{
    Heal,
    Damage,
}

public class HealthPop : MonoBehaviour
{
    [field: SerializeField] public HealthPopType Type { get; private set; }
    [SerializeField] private TextMeshPro textMeshPro;

    [Header("Tween Movement")]
    [SerializeField] private Vector3 movementValue;
    [SerializeField] private float movementDuration;

    [Header("Tween Scale")]
    [SerializeField] private Vector3 scaleValue;
    [SerializeField] private float scaleDuration;

    [Header("Tween Alpha")]
    [SerializeField] private float alphaDelay;
    [SerializeField] private float alphaDuration;


    private Vector3 initialPosition;

    public void Initialize(string textValue, Vector3 position)
    {
        textMeshPro.text = textValue;
        transform.position = position;
        initialPosition = position;

        transform.DOScale(scaleValue, scaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
        textMeshPro.DOFade(0, alphaDuration).SetDelay(alphaDelay);
        transform.DOMove(position + movementValue, movementDuration).SetEase(Ease.InOutSine).OnComplete(() => { Reset(); GetComponent<PooledObject>().ReturnToPool(); });
    }

    private void Reset()
    {
        // Reset color
        Color color = textMeshPro.color;
        color.a = 1;
        textMeshPro.color = color;

        // Reset position
        transform.position = initialPosition;
    }
}
