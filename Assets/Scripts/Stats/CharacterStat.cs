using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum CharacterStatType
{
    Constitution,
    Endurance,
    Strength,
    Dexterity,
    Intelligence,
    Luck,
    Charisma,
    Wisdom
}

[Serializable]
public class CharacterStat 
{
    public CharacterStatType statType;
    public Stat stat;
    public List<DerivedCharacterStatEffect> derivedStatEffects = new List<DerivedCharacterStatEffect>();
}
