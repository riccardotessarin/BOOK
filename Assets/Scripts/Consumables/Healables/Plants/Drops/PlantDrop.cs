﻿using UnityEngine;
using System.Collections;
using User;

namespace Consumables.Healables.Plants.Drops {
	public abstract class PlantDrop : MonoBehaviour {

		[SerializeField] public GameObject plantDrop3DModel;

		public abstract string Name { get; }
		public abstract string Description { get; }
		public int HealthPercentage => 50;
		public abstract EnumUtility.CharacterType Type { get; }
		
		/// <summary>
		/// Try to pick up the drop and add it to the player's inventory.
		/// The gameObject is destroyed only if the player has space in his inventory.
		/// </summary>
		public abstract bool PickDrop(Inventory inventory);

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