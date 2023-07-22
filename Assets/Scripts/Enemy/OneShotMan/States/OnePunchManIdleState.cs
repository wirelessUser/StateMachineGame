using UnityEngine;
using StatePattern.StateMachine;

namespace StatePattern.Enemy.States
{
    public class OnePunchManIdleState : IState<OnePunchManController>
    {
        public OnePunchManController Owner { get; set; }
        private OnePunchManStateMachine stateMachine;
        private float timer;

        public OnePunchManIdleState(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            timer = Owner.Data.IdleTime;
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                stateMachine.ChangeState(OnePunchManStates.Rotating);
        }

        public void OnStateExit()
        {
            timer = 0;
        }
    }
}