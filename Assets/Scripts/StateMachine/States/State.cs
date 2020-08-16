using System.Collections.Generic;
using StateMachine.Actions.Interfaces;
using Characters.Interfaces;

namespace StateMachine.States {
    public abstract class State {
        protected IEnumerable<IAction> actions = null;
        protected abstract IEnumerable<IAction> Actions { get; }
        public PlayableCharacter player;

        public void Execute() {
            foreach (var action in Actions){
                action.Player=player;
                action.Execute();
            }
        }
    }
}