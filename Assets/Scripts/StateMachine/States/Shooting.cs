
using StatePattern.Enemy;
using UnityEngine;
using StatePattern.Main;
using StatePattern.Player;

public class Shooting : IState
{
    public OnePunchManController owner { get; set; }

    public OnePunchManStateMachine stateMachine;
    public float targetRotation;
    private PlayerController target;
    private float shootTimer;

    public Shooting(OnePunchManStateMachine stateMachine) => this.stateMachine = stateMachine;

    public void OnStateEnter()
    {
        SetTarget();
        shootTimer = 0;
    }

    public void Update()
    {
        Quaternion desiredRotation = CalculateRotationTowardsPlayer();
        owner.SetRotation(RotateTowards(desiredRotation));

        if (IsRotationComplete(desiredRotation))
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0)
            {
                ResetTimer();
                owner.Shoot();
            }
        }
    }

    public void OnStateExit() => target = null;

    private void SetTarget() => target = GameService.Instance.PlayerService.GetPlayer();

    private Quaternion CalculateRotationTowardsPlayer()
    {
        Vector3 directionToPlayer = target.Position - owner.Position;
        directionToPlayer.y = 0f;
        return Quaternion.LookRotation(directionToPlayer, Vector3.up);
    }

    private Quaternion RotateTowards(Quaternion desiredRotation) => Quaternion.LerpUnclamped(owner.Rotation, desiredRotation, owner.Data.RotationSpeed / 30 * Time.deltaTime);

    private bool IsRotationComplete(Quaternion desiredRotation) => Quaternion.Angle(owner.Rotation, desiredRotation) < owner.Data.RotationThreshold;

    private void ResetTimer() => shootTimer = owner.Data.RateOfFire;
}

