using System;
using System.Collections.Generic;
using System.Linq;
using Consumables;
using Consumables.Books;
using Consumables.Books.Abilities;
using Consumables.Healables.Plants;
using Consumables.Healables.Plants.PlantTypes;
using Consumables.Pages;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
using Consumables.Books.Drops;
using Consumables.Pages.Abilities;

namespace User {
	public class Inventory : MonoBehaviour {
		public Transform booksContainer;
		public Transform plantsContainer;

		private readonly List<Book> _books = new List<Book>();
		private readonly List<Plant> _plants = new List<Plant>();

		public static Dictionary<EnumUtility.AttackType, Type[]> BookDropDictionary =
			new Dictionary<EnumUtility.AttackType, Type[]> {
				{ EnumUtility.AttackType.Inferno, new[]{typeof(FirePillarDrop), typeof(FireballDrop)} },
				{ EnumUtility.AttackType.Niflheim, new[]{ typeof(IceStalagmiteDrop), typeof(BodyFreezeDrop)} },
				{ EnumUtility.AttackType.Basilisk, new[]{ typeof(BlindingCloudDrop), typeof(VenomousNeedleDrop) } },
				{ EnumUtility.AttackType.Neptunian, new[]{ typeof(WaterShieldDrop), typeof(SurgingTideDrop) } },
				{ EnumUtility.AttackType.Raijin, new[]{typeof(LightningSpeedDrop), typeof(ElectricalDischargeDrop)} }
			};

		public static List<Page> pageList = 
			new List<Page>{new BlindingCloudPage(), new BodyFreezePage(), new ElectricalDischargePage(),
			new FireballPage(),new FirePillarPage(), new IceStalagmitePage(), new LightningSpeedPage(),
			new SurgingTidePage(), new VenomousNeedlePage(), new WaterShieldPage()};


		public IList<Book> Books{get=>_books;}
		public IList<Plant> Plants{get=>_plants;}

		public static Inventory Instance { get; private set; }

		// Remember to handle null SelectedBook when the last charge is used and book is deleted
		// Something like this inside an action:
		/*
		SelectedBook ?? SelectedBook : (PCBasicAbility);
		 */
		public IBook SelectedBook { get; private set; }

		private void Awake() {
			if (Instance == null) {
				Instance = this;
			} else {
				Destroy(this);
			}

			/*
			foreach (KeyValuePair<EnumUtility.AttackType, EnumUtility.PageType[]> kvp in BookDropDictionary) {
				Debug.Log("Key = " + kvp.Key + "Value = " + kvp.Value);
			}
			*/

			Book book = new Fireball(booksContainer);
			Plant plant = new Genea(plantsContainer);
			Book book1= new FirePillar(booksContainer);
			Book book2=new IceStalagmite(booksContainer);
			Plant plant1= new Rayaza(plantsContainer);
			Plant plant2= new Ryua(plantsContainer);
			Plant plant3 = new Genea(plantsContainer);

			if (TryAddConsumableToInventory(book)) {
				Debug.Log("Fireball added to inventory!");
			}
			if (TryAddConsumableToInventory(new WaterShield(booksContainer))) {
				Debug.Log("Water Shield added to inventory!");
			}
			if (TryAddConsumableToInventory(new VenomousNeedle(booksContainer))) {
				Debug.Log("Water Shield added to inventory!");
			}
			/*if (TryAddConsumableToInventory(book2)) {
				Debug.Log("Fire Pillar added to inventory!");
			}*/
			if (TryAddConsumableToInventory(plant)) {
				Debug.Log("Genea added to inventory!");
			}
			/*if (TryAddConsumableToInventory(book1)){
				Debug.Log("FireColumn added to inventory");
			}
			if(TryAddConsumableToInventory(book2)){
				Debug.Log("IceSTalagmite added to Inventory");
			}
			if(TryAddConsumableToInventory(plant1)){
				Debug.Log("Rayaza added to Inventory");
			}*/
			if (TryAddConsumableToInventory(plant2)){
				Debug.Log("Ryua added to Inventory");
			}
			if(TryAddConsumableToInventory(plant3)){
				Debug.Log("Genea added to Inventory");
			}
		}

		private void Start() {
			var gO = new GameObject() { name = "BooksContainer" };
			booksContainer = gO.transform;
			var gO2 = new GameObject() { name = "PlantsContainer" };
			plantsContainer = gO2.transform;
		}

		/// <summary>
		/// Try to add a consumable to the inventory, if it returns false then it is either an instant-use consumable
		/// or the item list for this consumable is full
		/// </summary>
		/// <param name="consumable">Consumable item to be added to the inventory</param>
		/// <returns>Returns true if item is handled correctly</returns>
		public bool TryAddConsumableToInventory(IConsumable consumable) {
			bool success = false;

			if (consumable is Book book) {
				if (_books.Count < 3) {
					_books.Add(book);
					// TODO: Display "Book added to inventory"
					success = true;
				} else {
					// TODO: Display "Maximum book capacity reached"
				}
			} else if (consumable is Plant plant) {
				if (_plants.Count < 10) {
					_plants.Add(plant);
					// TODO: Display "plant added to inventory"
					success = true;
				} else {
					// TODO: Display "Maximum plant capacity reached"
				}
			} else if (consumable is IPage page) {
				var compatibleBook = _books.FirstOrDefault(bookNotMax => bookNotMax.AddCharge(page));
				success = compatibleBook != null;
			}
			Debug.Log(consumable.Name+" added: "+success.ToString());
			return success;
		}

		/// <summary>
		/// Try to remove a consumable from the inventory, if it returns false then this function has been wrongly used on an 
		/// instant-use consumable or an error occured.
		/// </summary>
		/// <param name="consumable">Consumable item to be removed from the inventory</param>
		/// <returns>Returns true if item is handled correctly</returns>
		public bool TryRemoveConsumableFromInventory(IConsumable consumable) {
			bool success = false;

			if (consumable is Book book) {
				if (_books.Count > 0) {
					_books.Remove(book);
					// TODO: Display "Book destroyed"
					success = true;
				} else {
					Debug.Log("Book list error");
				}
			} else if (consumable is Plant plant) {
				if (_plants.Count > 0) {
					_plants.Remove(plant);
					// TODO: Display "plant has been eaten"
					success = true;
				} else {
					Debug.Log("Plant list error");
				}
			}

			return success;
		}

		private void Update() { }
	}
}