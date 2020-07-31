using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;

namespace Characters.NPC{
    public class MeltingKinean : NonPlayableCharacters
    {
        // Start is called before the first frame update
        protected override void Awaker(){
            base.Awaker();
            hp=20;
            secondType="melting";
            baseAttackRadius=2.5f;
            detectionRadius=10;
            basePower=2;
            speed=30;
            baseDamageMultiplicator=2;

            
        }
        protected override void Starter(){
            base.Starter();
        }
        protected override void TakeDamage(float damage){
            if (damage< currentHp)
                currentHp-=damage;
            else
                Death();
        }
        protected override void BaseAttack(){
            if(!isAttacking)
                StartCoroutine(BaseAttackDamage());

        }
        IEnumerator BaseAttackDamage(){
            isAttacking=true;
            this.GetComponent<Material>().color=Color.red;
            target.SendMessage("TakeDamage",BaseDamage,SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(speed/60f);
            this.GetComponent<Material>().color=Color.white;
            isAttacking=false;



        }


        // Update is called once per frame
        
    }
}