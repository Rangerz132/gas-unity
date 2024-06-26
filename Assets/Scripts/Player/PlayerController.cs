using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterBase
{
    [field: SerializeField] public AbilityManager AbilityManager { get; private set; }

    void Start()
    {
        // Get components
        AbilityManager = gameObject.GetComponent<AbilityManager>();
    }

    void Update()
    {
        // Use ability 01
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AbilityManager.Abilities[0].Use(gameObject);
        }

        // Use ability 02
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AbilityManager.Abilities[1].Use(gameObject);
        }

        // Use ability 03
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AbilityManager.Abilities[2].Use(gameObject);
        }

        // Use ability 04
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AbilityManager.Abilities[3].Use(gameObject);
        }

        // Use ability 05
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AbilityManager.Abilities[4].Use(gameObject);
        }
    }
}
