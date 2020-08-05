using UnityEngine;
using System.Collections;

namespace Consumables.Healables {
	public interface IHealable : IConsumable {
		int HealthPercentage { get; }
	}
}
