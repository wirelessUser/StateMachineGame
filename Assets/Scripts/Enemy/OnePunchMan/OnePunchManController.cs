using UnityEngine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        private bool isIdle;
        private bool isRotating;
        private bool isShooting;
        private float idleTimer;
        private float targetRotation;

        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            InitializeVariables();
        }

        private void InitializeVariables()
        {
            isIdle = true;
            isRotating = false;
            isShooting = false;
            idleTimer = enemyScriptableObject.IdleTime;
        }

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;

            if(isIdle)
            {
                idleTimer -= Time.deltaTime;
                if(idleTimer <= 0)
                {
                    isIdle = false;
                    isRotating = true;
                    targetRotation = (Rotation.eulerAngles.y + 180) % 360;
                }
            }

            if(isRotating)
            {
                SetRotaion(CalculateRotate());
                if(IsRotationComplete())
                {
                    isIdle = true;
                    isRotating = false;
                    ResetTimer();
                }
            }

        }

        private void ResetTimer() => idleTimer = enemyScriptableObject.IdleTime;

        private Vector3 CalculateRotate() => Vector3.up * Mathf.MoveTowardsAngle(Rotation.eulerAngles.y, targetRotation, enemyScriptableObject.RotationSpeed * Time.deltaTime);

        private bool IsRotationComplete() => Mathf.Abs(Mathf.Abs(Rotation.eulerAngles.y) - Mathf.Abs(targetRotation)) < Data.RotationThreshold;

        public override void PlayerEnteredRange()
        {
            base.PlayerEnteredRange();
            isIdle = false;
            isRotating = false;
            isShooting = true;
        }

        public override void PlayerExitedRange() 
        {
            isIdle = true;
            isRotating = false;
            isShooting = false;
        }
    }
}