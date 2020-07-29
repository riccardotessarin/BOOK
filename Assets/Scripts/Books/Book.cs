using System;
using System.Collections;
using System.Collections.Generic;
using Books;
using UnityEngine;

namespace Books
{
	public class Book : MonoBehaviour, IBook
	{
		public string Name => "Fireball";
		public string Element => "Fire";
		public string Rarity => "Rare";
		public string Description => "Throw a ball of fire";
		public int Charges => 3;

		public void RemoveCharge()
		{
			// Remove a charge of this book's intance inside the user's inventory
			throw new NotImplementedException();
		}

		public void AddCharge()
		{
			// Add a charge of this book's intance inside the user's inventory due to a page drop
			throw new NotImplementedException();
		}

		public void UseBook()
		{
			// Define the behavior of the ability
			throw new NotImplementedException();
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