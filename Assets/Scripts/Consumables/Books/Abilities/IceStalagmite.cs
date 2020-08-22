using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Consumables.Books.Abilities{
	public class IceStalagmite : Book
	{
		public override string Name => "Ice Stalagmite";
		public override string Description => "Set a trap that create a stalagmite of ice";
		public override string Element => "Ice";
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.IceStalagmite;
		public override int Charges => 3;

		[SerializeField] private GameObject iceStalagmitePrefab;

		protected override void Awaker(){
			base.Awaker();
			bookIcon=Resources.Load<Sprite>("Images/NifhleimCommonBook");
			//iceStalagmitePrefab = Resources.Load("Prefabs/Attacks/IceStalagmite") as GameObject;
		}

		public override void UseConsumable() {
			// TODO: Instatiate on the ground, where player is aiming
			//bookVFX = Instantiate(iceStalagmitePrefab, transform, true);

			RemoveCharge();     // Remove charge after the ability is used
		}

		// Start is called before the first frame update
		void Start()
		{
			
		}

		// Update is called once per frame
		void Update()
		{
			
		}
	}
}
