using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatePattern.Enemy;
public class Idle : IState
{
    public OnePunchManController owner { get; set; }

    public OnePunchManStateMachine stateMachine;

    private float timer;
   
    public Idle(OnePunchManStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }
    public void OnStateEnter()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            stateMachine.ChangeState(OnePunchManStates.Rotating);
        }

    }

    public void OnStateExit()
    {
       
    }

    public void Update()
    {
    }


}
