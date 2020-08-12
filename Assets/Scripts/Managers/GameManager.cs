using UnityEngine;
using Characters.PC;
using Characters.Interfaces;
using StateMachine.States;
namespace Manager{
    public class GameManager : MonoBehaviour {
        [SerializeField] private PlayableCharacter player;
        private State state;
        // Start is called before the first frame update
        private void Start() {
            state= new InGameState();
            state.player=player;
         }

        // Update is called once per frame
        private void Update() {
            state.Execute();

        }
}
}