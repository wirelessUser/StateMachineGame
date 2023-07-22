using UnityEngine;

namespace StatePattern.StateMachine
{
    public interface IState<T>
    {
        public T Owner { get; set; }
        public void OnStateEnter();
        public void Update();
        public void OnStateExit();
    }
}