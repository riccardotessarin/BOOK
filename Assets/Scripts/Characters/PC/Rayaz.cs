using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
namespace Characters.PC{
    public class Rayaz : PlayableCharacter
    {
        
        // Start is called before the first frame update
        protected override void Awaker(){
            base.Awaker();
            type="rayaz";
            hp=50;
            stamina = 50;
            
            speed= 60;
            baseAttackRange=10;
            
            

        }
        protected override void Starter(){
            base.Starter();
        }
        /*protected override void BaseAttack(){
            
        }*/
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
            else{
                currentHp=0;
                Death();

            }
        }
        protected override IEnumerator BaseAttackDamage(){
            isAttacking=true;
            RaycastHit hit;
            Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out hit,baseAttackRange);
            Character hitted=hit.collider.GetComponent<Character>();
            if(hitted){
                
                hitted.SendMessage("TakeDamage",baseAttackPower,SendMessageOptions.DontRequireReceiver);
            }
            yield return new WaitForSeconds(speed/120f);
            isAttacking=false;
        }
        protected override IEnumerator SpecialAttackDamage(){
            yield return new WaitForSeconds(speed/120f);
        }
        
    }
}
