using StatePattern.Main;
using StatePattern.Player;
using StatePattern.StateMachine;

namespace StatePattern.Enemy.States
{
    public class RobotFollowingState : IState<RobotController>
    {
        public RobotController Owner { get; set; }
        private RobotStateMachine stateMachine;
        private PlayerController target;

        public RobotFollowingState(RobotStateMachine stateMachine) => this.stateMachine = stateMachine;

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
                stateMachine.ChangeState(RobotStates.Shooting);
            }
        }

        public void OnStateExit()
        {
            target = null;
            Owner.Agent.stoppingDistance = 1f;
        }
    }
}