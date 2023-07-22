using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StatePattern.StateMachine;
using System.Linq;
using StatePattern.Enemy.States;

namespace StatePattern.Enemy
{
    public class DashManStateMachine : GenericStateMachine<DashManController, DashManStates>
    {
        public DashManStateMachine(DashManController Owner) : base(Owner)
        {
            States.Add(DashManStates.Idle, new DashManIdleState(this));
            States.Add(DashManStates.Patrolling, new DashManPatrollingState(this));
            States.Add(DashManStates.Following, new DashManFollowingState(this));
            States.Add(DashManStates.Shooting, new DashManShootingState(this));

            SetStateOwners(States.Values.ToArray());
        }

        public void ChangeState(DashManStates newStateType) => ChangeState(States[newStateType]);
    }

    public enum DashManStates
    {
        Idle,
        Rotating,
        Patrolling,
        Following,
        Shooting
    }
}