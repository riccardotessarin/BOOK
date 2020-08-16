using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using User;
using System;

//using Books;
namespace Characters.Interfaces {
    public abstract class PlayableCharacter : Character {
        [SerializeField] protected int stamina;
        [SerializeField] protected int currentStamina;
        Inventory inventory;
        [SerializeField] protected bool powerMode; //se true sto usando armi, se false consumabili
        [SerializeField] protected Attack equippedAttack;
        [SerializeField] float buffRadius;
        [SerializeField] bool traitor;

        Dictionary<string, Action> malusDic = new Dictionary<string, Action>();


        // [SerializeField] Book EquippedBook;
        public enum Attack {
            BaseAttack,
            SpecialAttack,
            Book
        }

        void Awake() {
            Awaker();
        }

        // Start is called before the first frame update
        void Start() {
            Starter();
        }

        protected virtual void Awaker() {
            malusDic.Add("ryuyuki", RyuyukiBond);
            malusDic.Add("genee", GeneeBond);
            malusDic.Add("rayaz", RayazBond);
            gameObject.layer = 8; //PC layer
            isDeath = false;
            buffRadius = 5;
            traitor = false;
            powerMode = true;
            equippedAttack = Attack.BaseAttack;
            isAttacking = false;
        }

        protected virtual void Starter() {
            currentHp = hp;
            currentStamina = stamina;
        }

        // Update is called once per frame
        void Update() {
            MalusCheck();
        }

        protected override void Death() {
            isDeath = true;
            Debug.Log("DEATH");
        }

        protected override void BaseAttack() { }
        protected abstract void SpecialAttack();
        protected void BookAttack() { }
        protected abstract void RyuyukiBond();
        protected abstract void GeneeBond();
        protected abstract void RayazBond();

        void MalusCheck() {
            Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, buffRadius);
            foreach (var colliders in hitColliders){
                if (this.gameObject != colliders.gameObject){
                    var pc = colliders.GetComponent<PlayableCharacter>();
                    if (pc){
                        malusDic[pc.ToString()].DynamicInvoke();
                    }
                }
            }

            Array.Clear(hitColliders, 0, hitColliders.Length);
        }

        protected void UseEquipAttack() {
            switch (equippedAttack){
                case Attack.BaseAttack:
                    BaseAttack();
                    break;
                case Attack.SpecialAttack:
                    SpecialAttack();
                    break;
                case Attack.Book:
                    BookAttack();
                    break;
                default:
                    break;
            }
        }


        void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, buffRadius);
        }
    }
}