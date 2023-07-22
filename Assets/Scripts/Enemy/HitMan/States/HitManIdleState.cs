using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy.States
{
    public class HitManIdleState : IState<HitManController>
    {
        public HitManController Owner { get; set; }
        private HitManStateMachine stateMachine;
        private float timer;

        public HitManIdleState(HitManStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            timer = Owner.Data.IdleTime;
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                stateMachine.ChangeState(HitManStates.Patrolling);
        }

        public void OnStateExit()
        {
            timer = 0;
        }
    }
}