using UnityEngine;
using System.Collections;

namespace Consumables.Books.Drops {
	public abstract class BookDrop : MonoBehaviour {

		[SerializeField] public GameObject bookDrop3DModel;

		public abstract string DropName { get; }
		public abstract string DropDescription { get; }
		public abstract string DropElement { get; }
		public abstract string DropRarity { get; }
		public abstract EnumUtility.PageType DropPageType { get; }
		public abstract int DropCharges { get; }

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