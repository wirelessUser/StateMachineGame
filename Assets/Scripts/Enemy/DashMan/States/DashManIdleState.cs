using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy.States
{
    public class DashManIdleState : IState<DashManController>
    {
        public DashManController Owner { get; set; }
        private DashManStateMachine stateMachine;
        private float timer;

        public DashManIdleState(DashManStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            timer = Owner.Data.IdleTime;
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                stateMachine.ChangeState(DashManStates.Patrolling);
        }

        public void OnStateExit()
        {
            timer = 0;
        }
    }
}
