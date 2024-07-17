using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class HealthPop : MonoBehaviour, IPoolable
{
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

    public void Initialize(string textValue, Vector3 position)
    {
        textMeshPro.text = textValue;
        transform.position = position;

        transform.DOScale(scaleValue, scaleDuration).SetLoops(2, LoopType.Yoyo).SetEase(Ease.InOutSine);
        transform.DOMove(position + movementValue, movementDuration).SetEase(Ease.InOutSine);
        textMeshPro.DOFaceFade(0, alphaDuration).SetDelay(alphaDelay).OnComplete(() => { Release(); });
    }


    void Update()
    {

    }

    public void Fire()
    {
       
    }

    public void SetPoolManager()
    {
        
    }

    public void Release()
    {
       
    }


}
