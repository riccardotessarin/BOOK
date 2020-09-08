using UnityEngine;
using System.Collections;
using User;

namespace Consumables.Healables.Plants.Drops {
	public abstract class PlantDrop : MonoBehaviour {

		public abstract string Name { get; }
		public abstract string Description { get; }
		public int HealthPercentage => 50;
		public abstract EnumUtility.CharacterType Type { get; }

		protected Sprite plantIcon;
		public Sprite PlantIcon { get => plantIcon; }

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