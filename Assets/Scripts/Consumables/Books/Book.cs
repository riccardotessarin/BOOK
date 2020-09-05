using Consumables.Pages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using User;

namespace Consumables.Books {
    public abstract class Book : IBook {
        protected Transform container;
        protected Sprite bookIcon;
        protected GameObject bookVFX;
        
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract EnumUtility.AttackType Element { get; }
        public abstract string Rarity { get; }
        public abstract EnumUtility.PageType PageType { get; }

        public abstract int Charges { get; }

        public int CurrentCharges { get; protected set; }

        public Sprite BookIcon {
            get => bookIcon;
        }

        public Book(Transform container) {
            this.container = container;
            Awaker();
        }

        protected virtual void Awaker() {
            CurrentCharges = Charges;
        }
        
        public bool AddCharge(IPage page) {
            if (CurrentCharges < Charges && PageType == page.Type) {
                CurrentCharges++;
                Debug.Log("Charge added");
                return true;
            } else {
                Debug.Log("Max charges reached");
                return false;
            }
        }
        
        public void RemoveCharge() {
            CurrentCharges--;
            if (CurrentCharges == 0) {
                Inventory.Instance.TryRemoveConsumableFromInventory(this);
            }
        }

        public abstract void UseConsumable();

        /*private void Start() {
            CurrentCharges = Charges;
        }*/
    }
}