using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CharacterStatType
{
    Vitality,
    Endurance,
    Strength,
    Dexterity,
    Intelligence,
    Luck,
    Charisma
}

[Serializable]
public class CharacterStatInfo
{
    public CharacterStatType type;
    public Stat stat;
}

public class CharacterStats : MonoBehaviour
{
    [SerializeField] private List<CharacterStatInfo> statInfos;

    public Dictionary<CharacterStatType, Stat> Stats { get; private set; }

    void Start()
    {
        Stats = new Dictionary<CharacterStatType, Stat>();

        foreach (CharacterStatInfo info in statInfos)
        {
            if (!Stats.ContainsKey(info.type))
            {
                Stats.Add(info.type, info.stat);
            }
        }
    }

    public float GetStatValue(CharacterStatType statType)
    {
        if (Stats.TryGetValue(statType, out Stat stat))
        {
            return stat.Value;
        }
        else {
            // Add new Stat
            Stat newStat = new Stat();
            CharacterStatInfo characterStatInfo = new CharacterStatInfo();
            characterStatInfo.type = statType;
            characterStatInfo.stat = newStat;
            Stats.Add(characterStatInfo.type, characterStatInfo.stat);
            return newStat.Value;
        }
    }
}
