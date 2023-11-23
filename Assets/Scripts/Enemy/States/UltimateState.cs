using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class UltimateState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;

        public UltimateState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter(){ }

        public void Update() { }

        public void OnStateExit() { }
    }
}