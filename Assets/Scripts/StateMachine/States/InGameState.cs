
using System.Collections.Generic;

using StateMachine.Actions;
using StateMachine.Actions.Implementations;
using StateMachine.Actions.Interfaces;
namespace StateMachine.States{
    public class InGameState : State
    {
        protected override IEnumerable<IAction> Actions => actions ?? (actions = new List<IAction>(){
            ActionFactory.GetActionOfType<InGameInput>()
        });
    }
}
