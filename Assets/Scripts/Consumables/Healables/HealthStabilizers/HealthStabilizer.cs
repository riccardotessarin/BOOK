using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Consumables.Healables.HealthStabilizers {
	public class HealthStabilizer : MonoBehaviour, IHealable {
		[SerializeField] private GameObject healthStabilizer3DModel;

		public string Name => "Health Stabilizer";

		public string Description => "A pill made of condensed blood. You can use it to instantly restore 10% of your health.";

		public int HealthPercentage => 10;

		public void UseConsumable() {
			// TODO: Trigger health restore function
			Destroy(this.gameObject);
		}

		#region UnityMethods

		private void Awake() {
			healthStabilizer3DModel = Resources.Load<GameObject>("");
		}

		private void Start() {
			
		}

		private void Update() {
			
		}
		#endregion
	}
}
