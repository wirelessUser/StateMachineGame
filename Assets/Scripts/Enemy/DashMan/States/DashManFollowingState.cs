using StatePattern.Main;
using StatePattern.Player;
using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy.States
{
    public class DashManFollowingState : IState<DashManController>
    {
        public DashManController Owner { get; set; }
        private DashManStateMachine stateMachine;
        private PlayerController target;

        public DashManFollowingState(DashManStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            target = GameService.Instance.PlayerService.GetPlayer();
            Owner.Agent.stoppingDistance = Owner.Data.PlayerStoppingDistance;
        }

        public void Update()
        {
            Owner.Agent.SetDestination(target.Position);
            if (Owner.Agent.remainingDistance <= Owner.Agent.stoppingDistance)
            {
                Owner.Agent.isStopped = true;
                Owner.Agent.ResetPath();
                stateMachine.ChangeState(DashManStates.Shooting);
            }
        }

        public void OnStateExit()
        {
            target = null;
            Owner.Agent.stoppingDistance = 1f;
        }
    }
}