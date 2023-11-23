using StatePattern.Main;
using StatePattern.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy
{
    public class TeleportingState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public TeleportingState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            if(typeof(T) == typeof(BossController)){
                var player = GameService.Instance.PlayerService.GetPlayer();
                TeleportToRandomPosition(player.Position, Owner.Data.RangeTeleporting);
            }else{
                TeleportToRandomPosition(Owner.Position, Owner.Data.RangeTeleporting);
            }
            stateMachine.ChangeState(States.IDLE); 
        }

        public void Update() { }

        public void OnStateExit() { }

        private void TeleportToRandomPosition(Vector3 position, float radius) => Owner.Agent.Warp(GetRandomNavMeshPoint(position, radius));

        private Vector3 GetRandomNavMeshPoint(Vector3 position, float radius){
            Vector3 randomDirection = Random.insideUnitSphere * radius + position;
            if (NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, radius, NavMesh.AllAreas))
                return hit.position;
            else
                return randomDirection;
        }
    }
}