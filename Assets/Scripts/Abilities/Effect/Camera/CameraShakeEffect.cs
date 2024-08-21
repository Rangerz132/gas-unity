using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraShakeEffect", menuName = "Abilities/Effect/Camera/Camera Shake", order = 0)]
public class CameraShakeEffect : EffectStrategy
{
    [SerializeField] private float amplitudeGain;
    [SerializeField] private float attackTime;
    [SerializeField] private float sustainTime;
    [SerializeField] private float decayTime;
    [SerializeField] private float duration;
    [SerializeField] private NoiseSettings noiseSettings;
    private CinemachineImpulseSource impulseSource;

    public override void StartEffect(AbilityData data, Action finished)
    {
        impulseSource = CameraManager.Instance.GetComponent<CinemachineImpulseSource>();

        if (impulseSource != null)
        {
            ShakeCamera();
        }
    }

    private void ShakeCamera()
    {
        impulseSource.m_ImpulseDefinition.m_AmplitudeGain = amplitudeGain;
        impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_AttackTime = attackTime;
        impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_SustainTime = sustainTime;
        impulseSource.m_ImpulseDefinition.m_TimeEnvelope.m_DecayTime = decayTime;
        impulseSource.m_ImpulseDefinition.m_ImpulseDuration = duration;
        impulseSource.m_ImpulseDefinition.m_RawSignal = noiseSettings;
   
        impulseSource.GenerateImpulse();
    }
}
