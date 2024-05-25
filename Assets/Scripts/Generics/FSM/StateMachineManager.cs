using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachineManager<EState, TEntity> where EState : Enum
{
    public Dictionary<EState, BaseState<EState>> states = new Dictionary<EState, BaseState<EState>>();
    public BaseState<EState> PreviousState { get; private set; }
    public BaseState<EState> CurrentState { get; private set; }

    public TEntity Entity { get; private set; }

    public StateMachineManager(TEntity entity)
    {
        Entity = entity;
    }

    public void Initialize(EState stateKey)
    {
        PreviousState = states[stateKey];
        CurrentState = states[stateKey];
        CurrentState.EnterState();
    }

    public void UpdateState()
    {
        CurrentState.UpdateState();
    }

    public void FixedUpdate()
    {
        CurrentState.FixedUpdateState();
    }

    public void ChangeState(EState stateKey)
    {
        CurrentState.ExitState();
        PreviousState = CurrentState;
        CurrentState = states[stateKey];
        CurrentState.EnterState();
    }
}