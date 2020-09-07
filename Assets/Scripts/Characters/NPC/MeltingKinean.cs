using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
using Consumables.Pages;
using Consumables.Pages.Abilities;
using Consumables;
using User;



namespace Characters.NPC {
    public class MeltingKinean : NonPlayableCharacters {
        
        [SerializeField]private bool dropPage; //true, drop is a page // false drop is a health stabilizer
        public bool DropPage{get=>dropPage;}
        [SerializeField]private bool drop; //true, drop something //false drop nothing
        public bool Drop{get=>drop;}


        [SerializeField] private int randomIdle;
        
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
            speed = 120;
            drop=HasDrop(0.6f);
            animator=GetComponent<Animator>();
            
        }

        protected override void Starter() {
            base.Starter();
            if(drop){
                dropPage=HasDrop(0.8f);

            }
            SetIdleStance();
            animator.SetFloat("Speed",currentSpeed/speed);
        }

        protected override void Updater() {
            base.Updater();
            if(!isAttacking){
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
        }
        protected bool HasDrop(float diffVal){
            float val=Random.Range(0.1f,1.0f);
            return (val<=diffVal);
        }
        protected override void SetDropLoot(){}
        
        public Page GetDropPage(){
            Page page= Inventory.pageList[Random.Range(0,Inventory.pageList.Count)];
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
        
        

        protected override IEnumerator BaseAttackDamage() {
            isAttacking = true;
            animator.SetBool("IsAttacking",isAttacking);
            
            
            yield return new WaitForSeconds(60f/currentSpeed);
            target.SendMessage("TakeDamage", baseDamage, SendMessageOptions.DontRequireReceiver);
            
            isAttacking = false;
            animator.SetBool("IsAttacking",isAttacking);
        }

        protected override IEnumerator TakingDamage(Damage damage){
            isAttacking=true;
           
            Debug.Log("melting damage taken");
            
            
            Debug.Log("Low Damage");
            //animator.Play("anim_warrior_qr_1_Get_Hit_1",-1,0);
            //animator.SetBool("BigHit",false);
            //animator.SetBool("TakingDamage",takingDamage);
            animator.SetTrigger("Hitted");
            yield return new WaitForSeconds(0.1f);
            
            if (damage.DamageRec < currentHp) {
                Debug.Log("Calculating Damage");
                currentHp -= damage.DamageRec;
                Debug.Log(gameObject.ToString() + " took damage");
            } else{
                Debug.Log("Death");
                Death();

            }
            
            isAttacking=false;
            
        }

        protected override void Death(){
            base.Death();
            animator.SetTrigger("IsDeath");
            
        }
        
        protected override void ModifySpeed(float modifier){
            base.ModifySpeed(modifier);
            var value=currentSpeed/speed;
            animator.SetFloat("Speed",value);
        }


        // Update is called once per frame
    }
}