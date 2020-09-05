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


        [SerializeField] private int randomIdle;
        [SerializeField] private int randomAttack;
        [SerializeField] private Animator animator;
         

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
            animator=this.GetComponent<Animator>();

        }

        protected override void Starter() {
            base.Starter();
            if(drop){
                dropPage=HasDrop(0.8f);
                SetDropLoot();
            }
            SetIdleStance();
            
        }

        protected override void Updater() {
            base.Updater();
            if (DetectionZone()) {
                //Debug.Log(secondType+" "+type+"detecting "+target.type);
                transform.LookAt(target.transform);
                animator.SetBool("HasTarget",true);
                SetHorizontalDistance();
                if (BaseAttackZone()){
                    animator.SetBool("InRange",true);
                    BaseAttack();
                }
                else{
                    animator.SetBool("InRange",false);
                }
            }
            else{
                animator.SetBool("HasTarget",false);
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
        private void SetIdleStance(){
            randomIdle=Random.Range(0,4);
            animator.SetInteger("RandomIdle",randomIdle);
        }
        private void SetHorizontalDistance(){
            float distance=this.transform.position.x;
            animator.SetFloat("DistanceHorizontal",distance);
        }

        private void SetAttackMove(){
            randomAttack=Random.Range(0,5);
            animator.SetInteger("RandomAttack",randomAttack);
        }

        protected override IEnumerator BaseAttackDamage() {
            isAttacking = true;
            animator.SetBool("IsAttacking",isAttacking);
            SetAttackMove();
            
            yield return new WaitForSeconds(speed/60f);
            target.SendMessage("TakeDamage", baseDamage, SendMessageOptions.DontRequireReceiver);
            
            isAttacking = false;
            animator.SetBool("IsAttacking",isAttacking);
        }

        
        


        // Update is called once per frame
    }
}