﻿using UnityEngine;
using System.Collections;
using User;

namespace Consumables.Books.Drops {
	public abstract class BookDrop : MonoBehaviour {

		[SerializeField] public GameObject bookDrop3DModel;

		public abstract string Name { get; }
		public abstract string Description { get; }
		public abstract EnumUtility.AttackType Element { get; }
		public abstract string Rarity { get; }
		public abstract EnumUtility.PageType PageType { get; }
		public abstract int Charges { get; }

		protected Sprite bookIcon;
		public Sprite BookIcon { get => bookIcon; }

		/// <summary>
		/// Try to pick up the drop and add it to the player's inventory.
		/// The gameObject is destroyed only if the player has space in his inventory.
		/// </summary>
		public abstract bool PickDrop(Inventory inventory);

		#region UnityMethods

		private void Awake() {
			Awaker();
		}

		protected virtual void Awaker() { }

		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
		#endregion
	}
}