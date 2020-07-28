using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using User;
//using Books;
namespace Characters.Interfaces{
    public class PlayableCharacter : Character
    {
        [SerializeField] protected int stamina {get;set;}
        [SerializeField] protected int currentStamina {get;set;}
        [SerializeField] Inventory inventory;
        [SerializeField] protected bool powerMode{get; set;}
        [SerializeField] protected Attack equippedAttack {get;set;}
        
        // [SerializeField] Book EquippedBook;
        public enum Attack{
            BaseAttack,
            SpecialAttack,
            Book

        }
        
        // Start is called before the first frame update
        void Start()
        {
            CurrentHp = Hp;
            currentStamina = stamina;
            IsDeath = false;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        protected override void Death(){
            IsDeath=true;
        }
        protected override  int BaseAttack(){return 0;}
        protected virtual void SpecialAttack(){}
        protected void BookAttack(){}
        protected virtual void RyuyukiBond(){}
        protected virtual void GeneeBond(){}
        protected virtual void RayazBond(){}
        void MalusCheck(){}
        protected void UseEquipAttack(){
            switch(equippedAttack){
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


    }
}
