using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy.States
{
    public class DashManPatrollingState : IState<DashManController>
    {
        public DashManController Owner { get; set; }
        private DashManStateMachine stateMachine;
        private int currentPatrollingIndex = 0;
        private Vector3 targetPosition;

        public DashManPatrollingState(DashManStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            Owner.Agent.isStopped = false;
            targetPosition = GetTargetPosition();
            Owner.Agent.SetDestination(targetPosition);
        }

        public void Update()
        {
            if(Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance)
            {
                stateMachine.ChangeState(DashManStates.Idle);
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