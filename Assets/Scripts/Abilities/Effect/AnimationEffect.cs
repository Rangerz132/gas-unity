using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationEffect", menuName = "Abilities/Effect/Animation", order = 0)]
public class AnimationEffect : EffectStrategy
{
    [SerializeField] private string animationTrigger;
 
    public override void StartEffect(AbilityData data, Action finished)
    {
        CharacterBase character = data.User.GetComponent<CharacterBase>();
        character.Animator.SetTrigger(animationTrigger);
        finished();
    }
}
