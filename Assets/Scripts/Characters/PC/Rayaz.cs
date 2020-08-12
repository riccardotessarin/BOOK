using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
using Attack;
namespace Characters.PC{
    public class Rayaz : PlayableCharacter
    {
        [SerializeField] float specialAttackRadius;
        [SerializeField] Character targetEnemy;
        [SerializeField] float fogDamage;
        [SerializeField] float fogDuration;
        [SerializeField] PoisonFog prefabFog;
        
        // Start is called before the first frame update
        protected override void Awaker(){
            base.Awaker();
            type="rayaz";
            hp=50;
            stamina = 50;
            specialAttackRadius=10;
            speed= 60;
            baseAttackRange=10;
            targetEnemy=null;
            fogDamage=2;
            fogDuration=10;
            
            

        }
        protected override void Starter(){
            base.Starter();
        }
        /*protected override void BaseAttack(){
            
        }*/
        protected override void Updater(){
            base.Updater();
            if (targetEnemy && targetEnemy.IsDeath){
                StartCoroutine(PFog());
                targetEnemy=null;
            }
            
        }
        protected override void SpecialAttack(){
            if(!isAttacking && !targetEnemy){
                if(currentHp<=specialAttackRecoil){
                    Debug.Log("cannot do special attack, life too low");
                }
                else
                    StartCoroutine(SpecialEffect());
                
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
            if (damage< currentHp)
                currentHp-=damage;
            else{
                currentHp=0;
                Death();

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
            //aggiungere effetto attira mob
            isAttacking=true;
            currentHp=-specialAttackRecoil;
            RaycastHit hit;
            if(Physics.Raycast(camera.transform.position,camera.transform.forward,out hit,baseAttackRange)){
                Character hitted=hit.collider.GetComponent<Character>();
                if(hitted){
                    targetEnemy=hitted;
                    hitted.Poisoned=true;
                }
            }
            yield return new WaitForSeconds(speed/120f);
            isAttacking=false;
        }
        protected IEnumerator PFog(){
           //instanziare prefab poisonfog e poi distruggerlo
           PoisonFog fog= Instantiate<PoisonFog>(prefabFog,targetEnemy.transform);
           fog.Caster=this;
           fog.Damage=fogDamage;
           yield return new WaitForSeconds(fogDuration);
           Destroy(fog.gameObject);
        }

    }
}
