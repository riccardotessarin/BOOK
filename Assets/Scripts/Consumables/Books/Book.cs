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
		
		public int CurrentCharges { get; protected set;}



		[SerializeField] protected Sprite bookIcon;
		public Sprite BookIcon { get => bookIcon; }

		[SerializeField] public GameObject bookDrop3DModel;
		

		[SerializeField] public GameObject bookVFX;

		protected virtual void Awaker(){
			CurrentCharges=Charges;
		}
		private void Awake(){
			Awaker();
		}
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
				Destroy(this.gameObject);
			}
		}

		public abstract void UseConsumable();

		/*private void Start() {
			CurrentCharges = Charges;
		}*/
	}
}