using System.Collections;
using UnityEngine;
using Attacks;

namespace Characters.Interfaces {
    public abstract class Character : MonoBehaviour {
        [SerializeField] protected float hp;
        [SerializeField] protected float currentHp;
        public string type;

        [SerializeField] protected float speed;
        [SerializeField] protected float currentSpeed;
        [SerializeField] protected int basePower;
        [SerializeField] protected int currentBasePower;
        [SerializeField] public bool IsDeath{get;protected set;}
        [SerializeField] protected bool isAttacking;
        [SerializeField] bool poisoned;
        public const int PCLAYERMASK = 1<<8; //layer 8
        public const int NPCLAYERMASK = 1<<9; // layer 9 
        public bool Poisoned{
            get=>poisoned;
            set=>poisoned=value;
        }
        

        
        public struct Damage{
            public Damage(float damage, AttackType attackType){
                DamageRec=damage;
                AttackType=attackType;
            }
            public float DamageRec{get;}
            public AttackType AttackType{get;}
        }

        /*Method used to deal damage to the character instance,
        it is used by both PC and NPC, it is protected so use with
        SendMessage method like this:
        hitted.SendMessage("TakeDamage",baseDamage,SendMessageOptions.DontRequireReceiver);*/
        protected abstract void TakeDamage(Damage damage); 
        /*Method used when the character hp reach 0,
        (not implemented yet), activate the event linked to the death of the character instance*/

        protected abstract void Death();
        /*Method that activate baseAttack Coroutine, 
        PCs use this woth a particular input,
        NPCs use this when an enemy enter in their damagezone*/
        protected abstract void BaseAttack();

        public override string ToString() {
            return type;
        }

        /*Method used for modify speed of character instance,
        protected so same as TakeDamage*/
        protected virtual void ModifySpeed(float modifier){
            currentSpeed*=modifier;
            Debug.Log(ToString()+" speed modified");
        }

        /*Coroutine used for calculating in time damage caused by poisoned status
        */
        protected virtual IEnumerator PoisonDamage(){
            Poisoned=false;
            
            gameObject.GetComponent<Renderer>().material.color=Color.magenta;
            TakeDamage(new Damage(3,AttackType.Basilisk));
            
            yield return new WaitForSeconds(3);
            
            Poisoned=true;
        }
    }
}