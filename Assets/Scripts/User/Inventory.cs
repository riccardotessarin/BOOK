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

namespace User {
	public class Inventory : MonoBehaviour {
		private readonly List<Book> _books = new List<Book>();
		private readonly List<Plant> _plants = new List<Plant>();

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

			Book book =new GameObject().AddComponent<Fireball>();
			book.name="FireballBook";
			Plant plant =new GameObject().AddComponent<Genea>();
			plant.name="Genea1";
			Book book1=new GameObject().AddComponent<FireColumn>();
			book1.name="FireColumnBook";
			Book book2=new GameObject().AddComponent<IceStalagmite>();
			book2.name="IceStalagmiteBook";
			Plant plant1= new GameObject().AddComponent<Rayaza>();
			plant1.name="Rayaza1";
			Plant plant2= new GameObject().AddComponent<Ryua>();
			plant2.name="Ryua1";
			Plant plant3= new GameObject().AddComponent<Genea>();
			plant3.name="Genea2";

			if (TryAddConsumableToInventory(book)) {
				Debug.Log("Fireball added to inventory!");
			}
			if (TryAddConsumableToInventory(plant)) {
				Debug.Log("Genea added to inventory!");
			}
			if (TryAddConsumableToInventory(book1)){
				Debug.Log("FireColumn added to inventory");
			}
			if(TryAddConsumableToInventory(book2)){
				Debug.Log("IceSTalagmite added to Inventory");
			}
			if(TryAddConsumableToInventory(plant1)){
				Debug.Log("Rayaza added to Inventory");
			}
			if(TryAddConsumableToInventory(plant2)){
				Debug.Log("Ryua added to Inventory");
			}
			if(TryAddConsumableToInventory(plant3)){
				Debug.Log("Genea added to Inventory");
			}
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