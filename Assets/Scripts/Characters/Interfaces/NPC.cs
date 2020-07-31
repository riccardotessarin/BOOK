using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters.Interfaces{
    public abstract class NonPlayableCharacters : Character
    {
        [SerializeField] protected float baseAttackRadius;

        public string secondType;
        [SerializeField] protected PlayableCharacter target;
        //[SerializeField] protected bool hasTarget;
        [SerializeField] protected float detectionRadius;
        [SerializeField] protected float baseDamageMultiplicator;
        [SerializeField] protected float BaseDamage{
            get=> basePower*baseDamageMultiplicator;
        }

        protected virtual void Awaker(){
            gameObject.layer = 9; //NPC layer
            type= "kinean";
            isDeath=false;
            target= null;
            isAttacking=false;
            
        }
        protected virtual void Starter(){
            currentHp=hp;
        }
        void Awake(){
            Awaker();
        }

        // Start is called before the first frame update
        void Start()
        {
            Starter();
            
        }

        // Update is called once per frame
        void Update()
        {
            
            if(DetectionZone()){
                //Debug.Log(secondType+" "+type+"detecting "+target.type);
                transform.LookAt(target.transform);
                if (BaseAttackZone())
                    BaseAttack();
            }
            
        }
        protected override void Death(){
            isDeath=true;
        }
        protected override void BaseAttack(){
            Debug.Log(secondType+" "+type+"attacking "+target.type );
        }
        protected virtual bool BaseAttackZone(){
            if ( target){
                int layermask =1 <<8; //layer 8
                Collider[] hitcolliders =  Physics.OverlapSphere(transform.position,baseAttackRadius, layermask);
                foreach(var collider in hitcolliders){
                    if (collider.gameObject == target.gameObject)
                        return true;
                }
            }
            return false;
        }
        protected virtual bool DetectionZone(){
            int layermask =1<<8;
            Collider[] hitcolliders = Physics.OverlapSphere(transform.position,detectionRadius, layermask);
            if (hitcolliders.Length!=0){
                if (!target){
                    target=hitcolliders[0].GetComponent<PlayableCharacter>();
                    return true;
                }
                else{
                    foreach(var collider in hitcolliders){
                        if (collider.gameObject == target.gameObject)
                            return true;
                    }
                    target=null;
                    return false;
                }
            }
            else
                target=null;
                return false;
        }
        

        void OnDrawGizmosSelected(){
            Gizmos.color= Color.red;
            Gizmos.DrawWireSphere(transform.position,baseAttackRadius);
            Gizmos.color=Color.yellow;
            Gizmos.DrawWireSphere(transform.position,detectionRadius);
        }
    }
}
