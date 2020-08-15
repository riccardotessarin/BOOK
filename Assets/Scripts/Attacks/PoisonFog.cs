using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
namespace Attacks{
    public class PoisonFog : MonoBehaviour
    {
        // Start is called before the first frame update
        public Character Caster{get;set;}
        public Character.Damage Damage{get;set;}
        bool dealingDamage = false;
        void Update(){
            if(!dealingDamage)
                StartCoroutine(FogDamage());
        }
        IEnumerator FogDamage(){
            dealingDamage=true;
            Collider[] hitcolliders = Physics.OverlapBox(transform.position,transform.localScale*20/2,Quaternion.identity);
            foreach (var collider in hitcolliders){
                Character character= collider.GetComponent<Character>();
                if(character && character.gameObject!= Caster.gameObject)
                    character.SendMessage("TakeDamage",Damage,SendMessageOptions.DontRequireReceiver);
            }
            yield return new WaitForSeconds(3);
            dealingDamage=false;

        }

        void OnDrawGizmosSelected(){
            Gizmos.color=Color.magenta;
            Gizmos.DrawWireCube(transform.position,transform.localScale*20);
        }
        
    }
}
