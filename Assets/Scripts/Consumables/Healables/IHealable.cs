using UnityEngine;
using System.Collections;

namespace Consumables.Healables {
	public interface IHealable : IConsumable {
		float HealthPercentage { get; }
	}
}
