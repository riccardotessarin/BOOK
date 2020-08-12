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
                if(currentHp<=specialAttackRecoil){
                    Debug.Log("cannot do special attack, life too low");
                }
                else{
                isAttacking=true;
                StartCoroutine(SpecialEffect());
                isAttacking=false;
                }
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
        protected override void ReverseRyuyukiBond(){
            Debug.Log(this.ToString()+": reverse ryuyuki bond");
        }
        protected override void ReverseGeneeBond(){
            Debug.Log(this.ToString()+": reverse genee bond");
        }
        protected override void ReverseRayazBond(){
            Debug.Log(this.ToString()+": reverse rayaz bond");
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
            currentHp=-baseAttackRecoil;
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
            currentHp=-specialAttackRecoil;
            invicible=true;
            yield return new WaitForSeconds(15);
            invicible=false;

        }
        
        
    }
}
