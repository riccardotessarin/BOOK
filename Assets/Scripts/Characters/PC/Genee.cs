using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
namespace Characters.PC{
    public class Genee : PlayableCharacter
    {
        
        // Start is called before the first frame update
        protected override void Awaker(){
            base.Awaker();
            type="genee";
            hp=50;
            stamina = 50;
            
            speed= 60;
            
            

        }
        protected override void Starter(){
            base.Starter();
        }
        protected override void BaseAttack(){
            
        }
        protected override void SpecialAttack(){}
        protected override void RyuyukiBond(){
            Debug.Log(this.ToString()+"ryuyuki bond");
        }
        protected override void GeneeBond(){
            Debug.Log(this.ToString()+"genee bond");
        }
        protected override void RayazBond(){
            Debug.Log(this.ToString()+"rayazbond");
        }
        protected override void TakeDamage(float damage){
            if (damage< currentHp)
                currentHp-=damage;
            else
                Death();
        }
        
    }
}
