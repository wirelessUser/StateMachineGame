
using StatePattern.Enemy;
using UnityEngine;


public class Rotating : IState
{
    public OnePunchManController owner { get; set; }

    public OnePunchManStateMachine stateMachine;


    private float targetRotation;

    public Rotating(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;

    public void OnStateEnter() => targetRotation = (owner.Rotation.eulerAngles.y + 180) % 360;

    public void Update()
    {
        owner.SetRotation(CalculateRotation());
        if (IsRotationComplete())
            stateMachine.ChangeState(OnePunchManStates.Idle);
    }

    public void OnStateExit() => targetRotation = 0;

    private Vector3 CalculateRotation() => Vector3.up * Mathf.MoveTowardsAngle(owner.Rotation.eulerAngles.y, targetRotation, owner.Data.RotationSpeed * Time.deltaTime);

    private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(owner.Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < owner.Data.RotationThreshold;
}
