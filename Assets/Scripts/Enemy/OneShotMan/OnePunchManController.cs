using UnityEngine;
using StatePattern.Enemy.Bullet;
using StatePattern.Main;

namespace StatePattern.Enemy
{
    public class OnePunchManController : EnemyController
    {
        public OnePunchManController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            enemyView.SetController(this);
        }

        public override void UpdateEnemy()
        {
            if (currentState == EnemyState.DEACTIVE)
                return;
        }

        public override void PlayerEnteredRange()
        {
            base.PlayerEnteredRange();
        }

        public override void PlayerExitedRange() { }
    }
}