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
            CreateStateMachine();
            stateMachine.ChangeState(HitManStates.Idle);
        }

        public void CreateStateMachine()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;
            stateMachine = new HitManStateMachine(this);
        }

        public override void UpdateEnemy() => stateMachine.Update();

        public override void PlayerEnteredRange()
        {
            base.PlayerEnteredRange();
            stateMachine.ChangeState(HitManStates.Following);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(HitManStates.Idle);
    }
}