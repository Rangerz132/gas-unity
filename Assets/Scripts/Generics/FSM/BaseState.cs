using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class BaseState<EState> where EState : Enum
{
    protected float elapsedTime;

    public BaseState(EState stateKey)
    {
        StateKey = stateKey;
    }

    public EState StateKey { get; private set; }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void FixedUpdateState();
    public abstract void ExitState();
}