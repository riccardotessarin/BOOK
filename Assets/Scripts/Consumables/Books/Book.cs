using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Consumables.Books
{
	public abstract class Book : MonoBehaviour, IBook {
		public abstract string Name { get; }
		public abstract string Element { get; }
		public abstract string Rarity { get; }
		public abstract string Description { get; }
		public abstract int Charges { get; }
		public int CurrentCharges { get; private set; }

		public void AddCharge() { }
		public void RemoveCharge() { }
		public abstract int UseBook();

		private void Start() {
			CurrentCharges = Charges;
		}
	}
}