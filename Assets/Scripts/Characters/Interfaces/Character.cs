
using UnityEngine;
namespace Characters.Interfaces{
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected int Hp{get; set;}
        [SerializeField] protected int CurrentHp {get; set;}
        [SerializeField] public string Type {get; protected set;}
        [SerializeField] protected int Speed {get; set;}
        [SerializeField] protected int BasePower {get; set;}
        [SerializeField] protected bool IsDeath {get;set;}


        
        public virtual void  TakeDamage(int damage){
            if (damage<CurrentHp)
                CurrentHp=-damage;
            else 
                this.Death();     
        }
        protected abstract void Death();
        protected abstract int BaseAttack();
        public override string ToString(){
            return name;
        }

         
        


    }
}
