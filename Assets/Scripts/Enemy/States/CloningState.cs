using StatePattern.Main;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class CloningState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public CloningState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            CreateAClone();
            CreateAClone();
        }

        public void Update() { }

        public void OnStateExit() { }

        private void CreateAClone()
        {
            RobotController clonedRobot = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as RobotController;
            clonedRobot.SetCloneCount((Owner as RobotController).CloneCountLeft - 1);
            clonedRobot.Teleport();
            clonedRobot.ChangeColor(EnemyColorType.Clone);
            GameService.Instance.EnemyService.AddEnemy(clonedRobot);
        }
    }
}