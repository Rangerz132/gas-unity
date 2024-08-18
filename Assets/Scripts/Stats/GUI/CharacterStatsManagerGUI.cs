using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatsManagerGUI : MonoBehaviour
{
    public CharacterStatsManager characterStatsManager;
    public CharacterStatsSlotGUI characterStatsSlotGUIPrefab;
    public List<CharacterStatsSlotGUI> CharacterStatsSlotGUIs { get; private set; }

    void Start()
    {
        SetCharacterStatsSlotGUIs();
    }

    public void SetCharacterStatsSlotGUIs()
    {
        foreach (KeyValuePair<CharacterStatType, CharacterStat> kvp in characterStatsManager.CharacterStats)
        {
            CharacterStatsSlotGUI characterStatsSlotGUIInstance = Instantiate(characterStatsSlotGUIPrefab);
            characterStatsSlotGUIInstance.gameObject.transform.SetParent(gameObject.transform);
            characterStatsSlotGUIInstance.SetCharacterStatsInfo(kvp.Key, kvp.Value);
        }
    }
}
