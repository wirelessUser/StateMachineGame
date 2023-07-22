using StatePattern.Main;
using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy.States
{
    public class HitManFollowingState : IState<HitManController>
    {
        public HitManController Owner { get; set; }
        private HitManStateMachine stateMachine;
        private PlayerController target;

        public HitManFollowingState(HitManStateMachine stateMachine) => this.stateMachine = stateMachine;

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
                stateMachine.ChangeState(HitManStates.Shooting);
            }
        }

        public void OnStateExit()
        {
            target = null;
            Owner.Agent.stoppingDistance = 1f;
        }
    }
}