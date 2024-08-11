using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManagerGUI : MonoBehaviour
{
    public CharacterStats characterStats;
    public CharacterStatsSlotGUI characterStatsSlotGUIPrefab;
    public List<CharacterStatsSlotGUI> CharacterStatsSlotGUIs { get; private set; }

    void Start()
    {
        SetCharacterStatsSlotGUIs();
    }

    public void SetCharacterStatsSlotGUIs()
    {
        foreach (KeyValuePair<CharacterStatType, Stat> kvp in characterStats.Stats)
        {
            // Instantiate characterStatsSlotGUIPrefab
            CharacterStatsSlotGUI characterStatsSlotGUIInstance = Instantiate(characterStatsSlotGUIPrefab);
            characterStatsSlotGUIInstance.gameObject.transform.SetParent(gameObject.transform);

            characterStatsSlotGUIInstance.SetCharacterStatsInfo(this,kvp.Key, kvp.Value);
        }
    }
}
