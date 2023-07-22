using StatePattern.Enemy.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class DashManController : EnemyController
    {
        private DashManStateMachine stateMachine;

        public DashManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            stateMachine.ChangeState(DashManStates.Idle);
        }

        public override void CreateStateMachine() => stateMachine = new DashManStateMachine(this);

        public override void UpdateEnemy() => stateMachine.Update();

        public override void PlayerEnteredRange() => stateMachine.ChangeState(DashManStates.Following);

        public override void PlayerExitedRange() => stateMachine.ChangeState(DashManStates.Idle);

        public void Shoot()
        {
            enemyView.PlayShootingEffect();
            BulletController bullet = new BulletController(enemyView.transform, enemyScriptableObject.BulletData);
        }
    }
}