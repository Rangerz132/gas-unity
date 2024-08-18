using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBase : MonoBehaviour
{
    [field: SerializeField] public CharacterStatsManager CharacterStatsManager { get; private set; }
    [field: SerializeField] public DamageManager DamageManager { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public GizmoDrawer GizmoDrawer { get; private set; }

    private void Start()
    {
        Health = gameObject.GetComponent<Health>();
    }
}
