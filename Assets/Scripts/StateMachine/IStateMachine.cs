using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StatePattern.StateMachine
{
    public interface IStateMachine
    {
        public void ChangeState(States newState);
    }
}