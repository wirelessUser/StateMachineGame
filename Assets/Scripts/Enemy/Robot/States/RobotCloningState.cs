using StatePattern.Main;
using StatePattern.StateMachine;
using UnityEngine;
using UnityEngine.AI;

namespace StatePattern.Enemy.States
{
    public class RobotCloningState : IState<RobotController>
    {
        public RobotController Owner { get; set; }
        private RobotStateMachine stateMachine;

        public RobotCloningState(RobotStateMachine stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter()
        {
            Clone();
        }

        public void Update()
        {

        }

        public void OnStateExit()
        {

        }

        private void Clone()
        {
            RobotController clonedRobot1 = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as RobotController;
            RobotController clonedRobot2 = GameService.Instance.EnemyService.CreateEnemy(Owner.Data) as RobotController;
            clonedRobot1.SetCloneCount(Owner.CloneCount - 1);
            clonedRobot2.SetCloneCount(Owner.CloneCount - 1);
            clonedRobot1.Teleport();
            clonedRobot2.Teleport();
        }
    }
}