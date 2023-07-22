using StatePattern.Main;
using StatePattern.Player;
using StatePattern.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Enemy.States
{
    public class RobotShootingState : IState<RobotController>
    {
        public RobotController Owner { get; set; }
        private RobotStateMachine stateMachine;
        private PlayerController target;
        private float shootTimer;

        public RobotShootingState(RobotStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            target = GameService.Instance.PlayerService.GetPlayer();
            shootTimer = 0;
        }

        public void Update()
        {
            Quaternion desiredRotation = GetDesiredRotation();
            Owner.SetRotaion(Quaternion.LerpUnclamped(Owner.Rotation, desiredRotation, Owner.Data.RotationSpeed / 30 * Time.deltaTime));

            if (IsRotationComplete(desiredRotation))
            {
                shootTimer -= Time.deltaTime;
                if (shootTimer <= 0)
                {
                    Owner.Shoot();
                    shootTimer = Owner.Data.RateOfFire;
                }
            }
        }

        private Quaternion GetDesiredRotation()
        {
            Vector3 directionToPlayer = target.Position - Owner.Position;
            directionToPlayer.y = 0f;
            return Quaternion.LookRotation(directionToPlayer, Vector3.up);
        }

        private bool IsRotationComplete(Quaternion desiredRotation) => Quaternion.Angle(Owner.Rotation, desiredRotation) < Owner.Data.RotationThreshold;

        public void OnStateExit()
        {
            target = null;
        }
    }
}