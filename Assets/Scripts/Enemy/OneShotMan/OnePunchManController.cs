using UnityEngine;
using StatePattern.Enemy.Bullet;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        private OnePunchManStateMachine stateMachine;

        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            stateMachine.ChangeState(OnePunchManStates.Idle);
        }

        public override void CreateStateMachine() => stateMachine = new OnePunchManStateMachine(this);

        public override void UpdateEnemy() => stateMachine.Update();

        public override void PlayerEnteredRange() => stateMachine.ChangeState(OnePunchManStates.Shooting);

        public override void PlayerExitedRange() => stateMachine.ChangeState(OnePunchManStates.Idle);

        public void Shoot()
        {
            enemyView.PlayShootingEffect();
            BulletController bullet = new BulletController(enemyView.transform, enemyScriptableObject.BulletData);
        }
    }
}