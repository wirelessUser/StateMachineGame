using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy.States
{
    public class RobotIdleState : IState<RobotController>
    {
        public RobotController Owner { get; set; }
        private RobotStateMachine stateMachine;
        private float timer;

        public RobotIdleState(RobotStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            timer = Owner.Data.IdleTime;
        }

        public void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
                stateMachine.ChangeState(RobotStates.Patrolling);
        }

        public void OnStateExit()
        {
            timer = 0;
        }
    }
}