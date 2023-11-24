using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatePattern.StateMachine;

namespace StatePattern.Enemy
{
    public class UltimateState<T> : IState where T : EnemyController
    {
        public EnemyController Owner { get; set; }
        private GenericStateMachine<T> stateMachine;
        private List<Tuple<States, float>> ultimateStatesList;
        public UltimateState(GenericStateMachine<T> stateMachine) => this.stateMachine = stateMachine;

        public void OnStateEnter(){ 
            ultimateStatesList = new();
            AddUltimateStates();
            ExecuteUltimateStates();
        }

        private void AddUltimateStates(){
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
            ultimateStatesList.Add(new Tuple<States, float>(States.TELEPORTING, 1.5f));
        }

        private async void ExecuteUltimateStates(){
            foreach(var state in ultimateStatesList){
                stateMachine.ChangeState(state.Item1);
                await Task.Delay((int)(state.Item2 * 1000));
            }
        }

        public void Update() { }

        public void OnStateExit() { }
    }
}