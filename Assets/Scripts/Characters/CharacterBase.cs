using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [field: SerializeField] public Health health;

    private void Start()
    {
        health = gameObject.GetComponent<Health>();
    }
}
