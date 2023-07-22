using StatePattern.Enemy.Bullet;

namespace StatePattern.Enemy
{
    public class RobotController : EnemyController
    {
        private RobotStateMachine stateMachine;
        public int CloneCount { get; private set; }

        public RobotController(EnemyScriptableObject enemyScriptableObject) : base(enemyScriptableObject)
        {
            SetCloneCount(2);
            enemyView.SetController(this);
            stateMachine.ChangeState(RobotStates.Idle);
        }

        public void SetCloneCount(int cloneCountToSet) => CloneCount = cloneCountToSet;

        public override void CreateStateMachine() => stateMachine = new RobotStateMachine(this);

        public override void UpdateEnemy() => stateMachine.Update();

        public override void PlayerEnteredRange() => stateMachine.ChangeState(RobotStates.Following);

        public override void PlayerExitedRange() => stateMachine.ChangeState(RobotStates.Idle);

        public override void Die()
        {
            if(CloneCount > 0)
                stateMachine.ChangeState(RobotStates.Cloning);
            base.Die();
        }

        public void Shoot()
        {
            enemyView.PlayShootingEffect();
            BulletController bullet = new BulletController(enemyView.transform, enemyScriptableObject.BulletData);
        }

        public void Teleport() => stateMachine.ChangeState(RobotStates.Teleporting);

    }
}