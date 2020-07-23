using System.Collections.Generic;
using StateMachine.Actions.Interfaces;

namespace StateMachine.States {
    public abstract class State {
        protected IEnumerable<IAction> actions = null;
        protected abstract IEnumerable<IAction> Actions { get; }

        public void Execute() {
            foreach (var action in Actions){
                action.Execute();
            }
        }
    }
}