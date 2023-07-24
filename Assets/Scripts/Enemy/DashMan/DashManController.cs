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
            CreateStateMachine();
            stateMachine.ChangeState(DashManStates.Idle);
        }

        public void CreateStateMachine() => stateMachine = new DashManStateMachine(this);

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;
            stateMachine.Update();
        }
        public override void PlayerEnteredRange()
        {
            base.PlayerEnteredRange();
            stateMachine.ChangeState(DashManStates.Following);
        }

        public override void PlayerExitedRange() => stateMachine.ChangeState(DashManStates.Idle);
    }
}