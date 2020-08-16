using System;
using System.Collections.Generic;
using Books;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;

namespace User {
    public class Inventory : MonoBehaviour {
        private readonly IEnumerable<IBook> _books = new List<IBook>();

        public IBook SelectedBook { get; private set; }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.Q)){
                //TODO: add function
            }
        }
    }
}