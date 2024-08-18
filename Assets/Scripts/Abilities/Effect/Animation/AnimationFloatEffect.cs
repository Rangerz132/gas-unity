using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationEffect", menuName = "Abilities/Effect/Animation/Animation Float", order = 0)]
public class AnimationFloatEffect : EffectStrategy
{
    [SerializeField] private string animationName;
    [SerializeField] private float animationValue;

    public override void StartEffect(AbilityData data, Action finished)
    {
        CharacterBase character = data.User.GetComponent<CharacterBase>();
        character.Animator.SetFloat(animationName, animationValue);
        finished();
    }
}
