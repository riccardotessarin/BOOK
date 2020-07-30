using UnityEngine;
using System.Collections;
using Assets.Scripts.Consumables.Books;
using Characters.Interfaces;

public class Fireball : Book {

	public override string Name => "Fireball";
	public override string Element => "Fire";
	public override string Rarity => "Rare";
	public override string Description => "Throw a ball of fire";
	public override int Charges => 3;

	public override int UseBook() {
		// Define the behavior of the ability
		return 0;
	}

	// Start is called before the first frame update
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	private void OnTriggerEnter(Collider other) {
		if (other.gameObject.GetComponent<NPC>() != null) {

		}
	}
}
