﻿using UnityEngine;
using System.Collections;

namespace Consumables.Books.Drops {
	public abstract class BookDrop : MonoBehaviour {

		[SerializeField] public GameObject bookDrop3DModel;

		public abstract string Name { get; }
		public abstract string Description { get; }
		public abstract string Element { get; }
		public abstract string Rarity { get; }
		public abstract EnumUtility.PageType PageType { get; }
		public abstract int Charges { get; }

		public abstract void PickDrop();

		#region UnityMethods
		// Use this for initialization
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
		#endregion
	}
}