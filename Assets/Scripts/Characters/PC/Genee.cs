using System;
using Assets.Scripts.Consumables.Books;
using Characters.Interfaces;
using UnityEngine;

namespace Characters.PC
{
	public class Genee : MonoBehaviour, IPlayableCharacter {
        public string Name => "Genee";
        public int Health { get; private set; } = 5;
        public int CurrentHealth { get; private set; }
        public int Stamina { get; private set; } = 2;
        public int Speed { get; private set; } = 3;

        private void Start() { }

        public void TakeDamage(int damage) {
            throw new NotImplementedException();
        }

        public int Attack() {
            // Ranged attack
            return 0;
        }

        public override string ToString() {
            return name;
        }

        public void UseSelectedBook(IBook book) {
            throw new NotImplementedException();
        }

        public void UseSpecialAttack() {
            throw new NotImplementedException();
        }
    }
}