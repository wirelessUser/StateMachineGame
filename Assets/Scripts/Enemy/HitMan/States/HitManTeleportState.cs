using StatePattern.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy.States
{
    public class HitManTeleportState : IState<HitManController>
    {
        public HitManController Owner { get; set; }
        private HitManStateMachine stateMachine;

        public HitManTeleportState(HitManStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            TeleportToRandomPosition();
            stateMachine.ChangeState(HitManStates.Following);
        }

        public void Update()
        {

        }

        public void OnStateExit()
        {

        }

        private void TeleportToRandomPosition() => Owner.Agent.Warp(GetRandomNavMeshPoint());

        private Vector3 GetRandomNavMeshPoint()
        {
            Vector3 randomDirection = Random.insideUnitSphere * 10f + Owner.Position;
            
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 10f, NavMesh.AllAreas);

            return hit.position;
        }
    }
}