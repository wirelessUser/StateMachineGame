using StatePattern.Enemy.States;
using StatePattern.StateMachine;
using System.Linq;

namespace StatePattern.Enemy
{
    public class OnePunchManStateMachine : GenericStateMachine<OnePunchManController, OnePunchManStates>
    {
        public OnePunchManStateMachine(OnePunchManController Owner) : base(Owner)
        {
            States.Add(OnePunchManStates.Idle, new OnePunchManIdleState(this));
            States.Add(OnePunchManStates.Rotating, new OnePunchManRotatingState(this));
            States.Add(OnePunchManStates.Shooting, new OnePunchManShootingState(this));

            SetStateOwners(States.Values.ToArray());
        }

        public void ChangeState(OnePunchManStates newStateType) => ChangeState(States[newStateType]);
    }

    public enum OnePunchManStates
    {
        Idle,
        Rotating,
        Shooting
    }
}