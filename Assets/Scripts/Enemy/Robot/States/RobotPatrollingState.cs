using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy.States
{
    public class RobotPatrollingState : IState<RobotController>
    {
        public RobotController Owner { get; set; }
        private RobotStateMachine stateMachine;
        private int currentPatrollingIndex = 0;
        private Vector3 targetPosition;

        public RobotPatrollingState(RobotStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            Owner.Agent.isStopped = false;
            targetPosition = GetTargetPosition();
            Owner.Agent.SetDestination(targetPosition);
        }

        public void Update()
        {
            if (Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance)
            {
                stateMachine.ChangeState(RobotStates.Idle);
            }
        }

        public void OnStateExit()
        {
            currentPatrollingIndex++;
        }

        private Vector3 GetTargetPosition()
        {
            if (currentPatrollingIndex >= Owner.Data.PatrollingPoints.Count)
                currentPatrollingIndex = 0;

            return Owner.Data.PatrollingPoints[currentPatrollingIndex];
        }
    }
}