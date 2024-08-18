using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationEffect", menuName = "Abilities/Effect/Animation/Animation Boolean", order = 0)]
public class AnimationBooleanEffect : EffectStrategy
{
    [SerializeField] private string animationName;
    [SerializeField] private bool animationValue;

    public override void StartEffect(AbilityData data, Action finished)
    {
        CharacterBase character = data.User.GetComponent<CharacterBase>();
        character.Animator.SetBool(animationName, animationValue);
        finished();
    }
}
