using StatePattern.StateMachine;
using StatePattern.Enemy.States;
using System.Linq;

namespace StatePattern.Enemy
{
    public class RobotStateMachine : GenericStateMachine<RobotController, RobotStates>
    {
        public RobotStateMachine(RobotController Owner) : base(Owner)
        {
            States.Add(RobotStates.Idle, new RobotIdleState(this));
            States.Add(RobotStates.Patrolling, new RobotPatrollingState(this));
            States.Add(RobotStates.Following, new RobotFollowingState(this));
            States.Add(RobotStates.Shooting, new RobotShootingState(this));
            States.Add(RobotStates.Teleporting, new RobotTeleportingState(this));
            States.Add(RobotStates.Cloning, new RobotCloningState(this));

            SetStateOwners(States.Values.ToArray());
        }

        public void ChangeState(RobotStates newStateType) => ChangeState(States[newStateType]);
    }

    public enum RobotStates
    {
        Idle,
        Patrolling,
        Following,
        Shooting,
        Teleporting,
        Cloning
    }
}