using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using StatePattern.Enemy;
using System;

public class OnePunchManStateMachine : MonoBehaviour
{

    private Dictionary<OnePunchManStates, IState> states = new Dictionary<OnePunchManStates, IState>();

    private  IState currentState;
    private OnePunchManController owner;

    public OnePunchManStateMachine(OnePunchManController owner)
    {
        this.owner = owner;
        InitializeStates();
        setOwnerToStates(owner);
    }

    private void InitializeStates()
    {
        states.Add(OnePunchManStates.Idle, new Idle(this));
        states.Add(OnePunchManStates.Rotating, new Rotating(this));
        states.Add(OnePunchManStates.Shooting, new Shooting(this));

    }

    private void setOwnerToStates(OnePunchManController owner)
    {
        foreach (IState state in states.Values)
        {
            state.owner = owner;
        }
    }

    private void ChangeState(IState newState)
    {
        if (currentState != null) currentState.OnStateExit();
        currentState = newState;
        currentState.OnStateEnter();

    }

    public void ChangeState(OnePunchManStates newState) => ChangeState(states[newState]);
}
