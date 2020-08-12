using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;

namespace Characters.PC{
    public class Ryuyuki : PlayableCharacter
    {
        [SerializeField] protected Character lastTarget;
        [SerializeField] protected float specialAttackRadius;
        [SerializeField] protected float speedModifier;
        [SerializeField] protected float maxlastTargetDistance;
        
        
        
        // Start is called before the first frame update
        protected override void Awaker(){
            base.Awaker();
            type="ryuyuki";
            hp=50;
            stamina = 50;
            specialAttackRadius = 10;
            speed= 60;
            baseAttackRange=10;
            speedModifier=0.1f;
            maxlastTargetDistance=20;
            
            

        }
        protected override void Starter(){
            base.Starter();
        }
        /*protected override void BaseAttack(){
            
            
        }*/
        protected override void Updater(){
            base.Updater();
            if(lastTarget)
                DistanceCheckLastTarget();

            

        }
        protected override void SpecialAttack(){
            if(!isAttacking && lastTarget){
                if (currentHp<= specialAttackRecoil){
                    Debug.Log("cannot do special attack, life too low");
                }
                else{
                    isAttacking=true;
                    StartCoroutine(SpecialEffect());
                    lastTarget=null;
                    isAttacking=false;
                }
            }
        }
        protected override void RyuyukiBond(){
            Debug.Log(this.ToString()+": ryuyuki bond");
        }
        protected override void GeneeBond(){
            Debug.Log(this.ToString()+"genee bond");
        }
        protected override void RayazBond(){
            Debug.Log(this.ToString()+"rayaz bond");
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
            if (damage< currentHp){
                Debug.Log("taking damage");
                currentHp-=damage;
            }
            else{
                currentHp=0;
                Death();

            }
            
                
        }

        protected override IEnumerator BaseAttackDamage(){
            isAttacking=true;
            currentHp=-baseAttackRecoil;
            RaycastHit hit;
            if (Physics.Raycast(camera.transform.position,camera.transform.forward,out hit,baseAttackRange)){
                Character hitted=hit.collider.GetComponent<Character>();
                if(hitted){
                    lastTarget=hitted;
                    hitted.SendMessage("TakeDamage",basePower,SendMessageOptions.DontRequireReceiver);
                    
                }
            }
            yield return new WaitForSeconds(speed/120f);
            isAttacking=false;
        }

        protected override IEnumerator SpecialEffect(){
            currentHp=-specialAttackRecoil;
            Collider[] hitcolliders= Physics.OverlapSphere(lastTarget.transform.position,specialAttackRadius);
            foreach(var collider in hitcolliders){
                Character character= collider.GetComponent<Character>();
                if(character && character.gameObject!= this.gameObject){
                    character.ModifySpeed(speedModifier);                    
                }
            }
            
            yield return new WaitForSeconds(10);
            foreach(var collider in hitcolliders){
                Character character=collider.GetComponent<Character>();
                if(character && character.gameObject!=this.gameObject)
                    character.ModifySpeed(1/speedModifier);
            }

        }

        protected void DistanceCheckLastTarget(){
            float distance= Vector3.Distance(transform.position,lastTarget.transform.position);
            if (distance > maxlastTargetDistance)   
                lastTarget=null;   
        }

        protected override void OnDrawGizmosSelected(){
            base.OnDrawGizmosSelected();
            
            Gizmos.color=Color.cyan;
            if(lastTarget)
                Gizmos.DrawWireSphere(lastTarget.transform.position,specialAttackRadius);
        }

        


        
        
    }
}
