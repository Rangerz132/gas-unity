using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthPop : MonoBehaviour, IPoolable
{
    [SerializeField] private TextMeshPro textMeshPro;
    [SerializeField] private float movementSpeed;

    void Update()
    {
        float targetY = transform.position.y + (Time.deltaTime * movementSpeed);
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    public void Initialize(string textValue, Vector3 position)
    {
        textMeshPro.text = textValue;
        transform.position = position;
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
