using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStatsManager))]
[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(DamageManager))]
[RequireComponent(typeof(ResistanceManager))]
[RequireComponent(typeof(RegenerationManager))]
public abstract class CharacterBase : MonoBehaviour
{
    [field: SerializeField] public CharacterStatsManager CharacterStatsManager { get; private set; }
    [field: SerializeField] public HealthManager HealthManager { get; private set; }
    [field: SerializeField] public DamageManager DamageManager { get; private set; }
    [field: SerializeField] public ResistanceManager ResistanceManager { get; private set; }
    [field: SerializeField] public RegenerationManager RegenerationManager { get; private set; }
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public GizmoDrawer GizmoDrawer { get; private set; }


    private void Awake()
    {
        CharacterStatsManager = GetComponent<CharacterStatsManager>();
        HealthManager = GetComponent<HealthManager>();
        DamageManager = GetComponent<DamageManager>();
        ResistanceManager = GetComponent<ResistanceManager>();
        RegenerationManager = GetComponent<RegenerationManager>();
    }
}
