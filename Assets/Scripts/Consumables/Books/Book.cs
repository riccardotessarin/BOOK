using Consumables.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using User;

namespace Consumables.Books {
	public abstract class Book : MonoBehaviour, IBook {
		public abstract string Name { get; }
		public abstract string Description { get; }
		public abstract string Element { get; }
		public abstract string Rarity { get; }
		public abstract EnumUtility.PageType PageType { get; }

		public abstract int Charges { get; }
		public int CurrentCharges { get; private set; }

		[SerializeField] public GameObject bookDrop3DModel;
		[SerializeField] public Texture bookIcon;
		public Texture BookIcon { get => bookIcon; }
		[SerializeField] public GameObject bookVFX;

		public bool AddCharge(IPage page) {
			if (CurrentCharges < Charges && PageType == page.Type) {
				CurrentCharges++;
				Debug.Log("Charge added");
				return true;
			} else {
				Debug.Log("Max charges reached");
				return false;
			}
		}

		public void RemoveCharge() {
			CurrentCharges--;
			if (CurrentCharges == 0) {
				Inventory.Instance.TryRemoveConsumableFromInventory(this);
				Destroy(this);
			}
		}

		public abstract void UseConsumable();

		private void Start() {
			CurrentCharges = Charges;
		}
	}
}