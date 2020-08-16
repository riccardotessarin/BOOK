using UnityEngine;
using System.Collections;
using Characters.Interfaces;


namespace Consumables.Books.Abilities {
	public class Fireball : Book {

		public override string Name => "Fireball";
		public override string Description => "Throw a ball of fire";
		public override string Element => "Fire";
		public override string Rarity => "Rare";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.Fireball;
		public override int Charges => 3;

		[SerializeField] private GameObject FireballBookIcon;
		[SerializeField] private Sprite fireballSprite = Resources.Load<Sprite>("Sprites/FireballSprite");
		[SerializeField] private GameObject fireballVFX;
		[SerializeField] private int fireballSpeed;


		
		public override void UseConsumable() {
			// Define the behavior of the ability
			fireballVFX = Instantiate(fireballVFX, transform, true);
			Rigidbody rigidbody = fireballVFX.GetComponent<Rigidbody>();
			rigidbody.velocity = transform.forward * fireballSpeed;

			RemoveCharge();     // Remove charge after the ability is used
		}

		// Start is called before the first frame update
		void Start() {

		}

		// Update is called once per frame
		void Update() {

		}
	}
}
