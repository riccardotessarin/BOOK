using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Consumables.Books.Abilities{
    public class FireColumn : Book
    {
        public override string Name => "Fire Column";
		public override string Description => "Set a trap that create a column of fire";
		public override string Element => "Fire";
		public override string Rarity => "Common";
		public override EnumUtility.PageType PageType => EnumUtility.PageType.FireColumn;
		public override int Charges => 3;

		private void Awake(){
            bookIcon=Resources.Load<Sprite>("Images/InfernoCommonBook");
        }

        public override void UseConsumable(){}
		
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
