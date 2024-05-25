using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    [field: SerializeField] public AbilityManager AbilityManager { get; private set; }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AbilityManager.Abilities[0].Use(gameObject);
        }
    }
}
