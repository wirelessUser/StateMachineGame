using StatePattern.Enemy.Bullet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.Enemy
{
    public class HitManController : EnemyController
    {
        private HitManStateMachine stateMachine;

        public HitManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
            stateMachine.ChangeState(HitManStates.Idle);
        }

        public override void CreateStateMachine() => stateMachine = new HitManStateMachine(this);

        public override void UpdateEnemy() => stateMachine.Update();

        public override void PlayerEnteredRange() => stateMachine.ChangeState(HitManStates.Following);

        public override void PlayerExitedRange() => stateMachine.ChangeState(HitManStates.Idle);

        public void Shoot()
        {
            enemyView.PlayShootingEffect();
            BulletController bullet = new BulletController(enemyView.transform, enemyScriptableObject.BulletData);
            stateMachine.ChangeState(HitManStates.Teleporting);
        }
    }
}