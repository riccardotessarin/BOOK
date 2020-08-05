using StateMachine.Actions;
using StateMachine.Actions.Implementations;
using StateMachine.Actions.Interfaces;
using StateMachine.States;
using System.Collections.Generic;

namespace StateMachine.States {
	public class GameState : State {

		protected override IEnumerable<IAction> Actions =>
				actions ?? (actions = new List<IAction>() {
			ActionFactory.GetActionOfType<InGameInputsAction>()
			});
	}
}
