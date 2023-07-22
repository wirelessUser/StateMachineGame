using StatePattern.StateMachine;
using StatePattern.Enemy.States;
using System.Linq;

namespace StatePattern.Enemy
{
    public class HitManStateMachine : GenericStateMachine<HitManController, HitManStates>
    {
        public HitManStateMachine(HitManController Owner) : base(Owner)
        {
            States.Add(HitManStates.Idle, new HitManIdleState(this));
            States.Add(HitManStates.Patrolling, new HitManPatrollingState(this));
            States.Add(HitManStates.Following, new HitManFollowingState(this));
            States.Add(HitManStates.Shooting, new HitManShootingState(this));
            States.Add(HitManStates.Teleporting, new HitManTeleportState(this));

            SetStateOwners(States.Values.ToArray());
        }

        public void ChangeState(HitManStates newStateType) => ChangeState(States[newStateType]);
    }

    public enum HitManStates
    {
        Idle,
        Patrolling,
        Following,
        Shooting,
        Teleporting
    }
}