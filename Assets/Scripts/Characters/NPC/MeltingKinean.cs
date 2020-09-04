using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
using Consumables.Pages;
using Consumables.Pages.Abilities;
using Consumables;



namespace Characters.NPC {
    public class MeltingKinean : NonPlayableCharacters {
        private EnumUtility.PageType pageDrop;
        [SerializeField]private bool dropPage; //true, drop is a page // false drop is a health stabilizer
        public bool DropPage{get=>dropPage;}
        [SerializeField]private bool drop; //true, drop something //false drop nothing
        public bool Drop{get=>drop;}
         

        //private HealthStabilizer stabDrop;
        
        // Start is called before the first frame update
        protected override void Awaker() {
            base.Awaker();
            hp = 20;
            secondType = "melting";
            baseAttackRadius = 2.5f;
            detectionRadius = 10;
            basePower = 2;
            speed = 30;
            drop=HasDrop(0.6f);
            

        }

        protected override void Starter() {
            base.Starter();
            if(drop){
                dropPage=HasDrop(0.8f);
                SetDropLoot();
            }
            
        }

        protected override void Updater() {
            base.Updater();
            if (DetectionZone()) {
                //Debug.Log(secondType+" "+type+"detecting "+target.type);
                transform.LookAt(target.transform);
                if (BaseAttackZone())
                    BaseAttack();
            }
        }
        protected bool HasDrop(float diffVal){
            float val=Random.Range(0.1f,1.0f);
            return (val<=diffVal);
        }

        protected override void SetDropLoot(){
            if(dropPage){
                pageDrop= (EnumUtility.PageType)Random.Range(0,3);
            }
            else{
                //health stabilizer
            }
        }
        public Page GetDropPage(){
            Page page= new FireballPage();
            return page;
        }

        //public HealthStabilizer GetDropHS(){}

        


        // Update is called once per frame
    }
}