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
		private readonly List<IBook> _books = new List<IBook>();
		private readonly List<IPlant> _plants = new List<IPlant>();

		public IList<IBook> Books{get=>_books;}
		public IList<IPlant> Plants{get=>_plants;}

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

			Book book = gameObject.AddComponent<Fireball>();
			Plant plant = gameObject.AddComponent<Genea>();

			if (TryAddConsumableToInventory(book)) {
				Debug.Log("Fireball added to inventory!");
			}
			if (TryAddConsumableToInventory(plant)) {
				Debug.Log("Genea added to inventory!");
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

			if (consumable is IBook book) {
				if (_books.Count < 3) {
					_books.Add(book);
					// TODO: Display "Book added to inventory"
					success = true;
				} else {
					// TODO: Display "Maximum book capacity reached"
				}
			} else if (consumable is IPlant plant) {
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

			if (consumable is IBook book) {
				if (_books.Count > 0) {
					_books.Remove(book);
					// TODO: Display "Book destroyed"
					success = true;
				} else {
					Debug.Log("Book list error");
				}
			} else if (consumable is IPlant plant) {
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