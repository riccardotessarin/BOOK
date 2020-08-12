using System.Collections;
using UnityEngine;
namespace Characters.Interfaces{
    public abstract class Character : MonoBehaviour
    {

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
        public bool Poisoned{
            get=>poisoned;
            set=>poisoned=value;
        }
        
        

        
        



        
        protected abstract void TakeDamage(float damage); 
        protected abstract void Death();
        protected abstract void BaseAttack();
        public override string ToString(){
            return type;
        }
        public virtual void ModifySpeed(float modifier){
            currentSpeed*=modifier;
            Debug.Log(ToString()+" speed modified");
        }
        protected virtual IEnumerator PoisonDamage(){
            Poisoned=false;
            
            gameObject.GetComponent<Renderer>().material.color=Color.magenta;
            TakeDamage(3);
            
            yield return new WaitForSeconds(3);
            
            Poisoned=true;
        }
        

         
        


    }
}
