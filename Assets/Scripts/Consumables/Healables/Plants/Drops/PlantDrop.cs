﻿using UnityEngine;
using System.Collections;

namespace Consumables.Healables.Plants.Drops {
	public abstract class PlantDrop : MonoBehaviour {

		[SerializeField] public GameObject plantDrop3DModel;

		public abstract string Name { get; }
		public abstract string Description { get; }
		public int HealthPercentage => 50;
		public abstract EnumUtility.CharacterType Type { get; }

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