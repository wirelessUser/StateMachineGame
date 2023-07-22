using StatePattern.StateMachine;
using UnityEngine;

namespace StatePattern.Enemy.States
{
    public class OnePunchManRotatingState : IState<OnePunchManController>
    {
        public OnePunchManController Owner { get; set; }
        private OnePunchManStateMachine stateMachine;
        private float targetRotation;

        public OnePunchManRotatingState(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;


        public void OnStateEnter()
        {
            targetRotation = (Owner.Rotation.eulerAngles.y + 180) % 360;
        }

        public void Update()
        {
            Owner.SetRotaion( Vector3.up* Mathf.MoveTowardsAngle(Owner.Rotation.eulerAngles.y, targetRotation, Owner.Data.RotationSpeed * Time.deltaTime));
            if (IsRotationComplete())
                stateMachine.ChangeState(OnePunchManStates.Idle);
        }

        public void OnStateExit()
        {
            targetRotation = 0;
        }

        private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Owner.Data.RotationThreshold;
    }
}