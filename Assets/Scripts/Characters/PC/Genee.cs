using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
namespace Characters.PC{
    public class Genee : PlayableCharacter
    {
        [SerializeField] bool invicible;
        
        // Start is called before the first frame update
        protected override void Awaker(){
            base.Awaker();
            type="genee";
            hp=50;
            stamina = 50;
            
            speed= 60;
            baseAttackRange=10;
            invicible=false;
            
            

        }
        protected override void Starter(){
            base.Starter();
        }
        /*protected override void BaseAttack(){
            
        }*/
        protected override void SpecialAttack(){
            if(!isAttacking){
                isAttacking=true;
                StartCoroutine(SpecialEffect());
                isAttacking=false;
            }
        }
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
            if(!invicible){
                if (damage< currentHp)
                    currentHp-=damage;
                else{
                    currentHp=0;
                    Death();

                }
            }
        }

        protected override IEnumerator BaseAttackDamage(){
            isAttacking=true;
            RaycastHit hit;
            if(Physics.Raycast(camera.transform.position,camera.transform.forward,out hit,baseAttackRange)){
                Character hitted=hit.collider.GetComponent<Character>();
                if(hitted){
                    
                    hitted.SendMessage("TakeDamage",basePower,SendMessageOptions.DontRequireReceiver);
                }
            }
            yield return new WaitForSeconds(speed/120f);
            isAttacking=false;
        }
        protected override IEnumerator SpecialEffect(){
            
            invicible=true;
            yield return new WaitForSeconds(15);
            invicible=false;

        }
        
        
    }
}
