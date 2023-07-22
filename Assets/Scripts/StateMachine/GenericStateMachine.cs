using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.StateMachine
{
    public class GenericStateMachine<T, TEnum> where TEnum: System.Enum
    {
        protected T Owner;
        protected IState<T> currentState;
        protected Dictionary<TEnum, IState<T>> States = new Dictionary<TEnum, IState<T>>();

        public GenericStateMachine(T Owner) => this.Owner = Owner;

        protected void SetStateOwners(IState<T>[] statesToInitialize)
        {
            foreach (IState<T> state in statesToInitialize)
            {
                state.Owner = Owner;
            }
        }

        public void Update()
        {
            currentState?.Update();
        }

        protected void ChangeState(IState<T> newState)
        {
            Debug.Log($"Entering {newState} State");
            currentState?.OnStateExit();
            currentState = newState;
            currentState?.OnStateEnter();
        }
    }
}