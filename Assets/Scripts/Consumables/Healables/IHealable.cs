using UnityEngine;
using System.Collections;

public interface IHealable : IConsumable {
	int HealthPercentage { get; }
}
