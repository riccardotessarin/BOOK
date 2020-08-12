using Characters.Interfaces;

namespace StateMachine.Actions.Interfaces {
    public interface IAction {
        PlayableCharacter Player{get;set;}
        void Execute();
    }
}