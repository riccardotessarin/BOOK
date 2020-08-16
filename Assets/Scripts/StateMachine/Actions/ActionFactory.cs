using System;
using System.Collections.Generic;
using StateMachine.Actions.Interfaces;

namespace StateMachine.Actions {
    public static class ActionFactory {
        private static readonly Dictionary<Type, IAction> Actions = new Dictionary<Type, IAction>();

        public static IAction GetActionOfType<T>() where T : IAction, new() {
            if (!Actions.TryGetValue(typeof(T), out var action)){
                Actions[typeof(T)] = Activator.CreateInstance(typeof(T)) as IAction;
                action = Actions[typeof(T)];
            }

            return action;
        }
    }
}