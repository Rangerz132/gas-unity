using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }

    private void Start()
    {
        Health = gameObject.GetComponent<Health>();
    }
}
