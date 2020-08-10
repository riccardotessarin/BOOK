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
        public const int PCLAYERMASK =1<<8; //layer 8
        [SerializeField] protected float BaseDamage{
            get=> basePower*baseDamageMultiplicator;
        }

        protected virtual void Awaker(){
            gameObject.layer = 9; //NPC layer
            type= "kinean";
            IsDeath=false;
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
        protected virtual void Updater(){
            
        }
        void FixedUpdate(){
            if(Poisoned){
                StartCoroutine(PoisonDamage());
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsDeath){
                Updater();
            }
            else{
                gameObject.GetComponent<Renderer>().material.color=Color.black;
            }
            
            
        }
        protected override void Death(){
            IsDeath=true;
        }
        protected override void BaseAttack(){
            if(!isAttacking)
                StartCoroutine(BaseAttackDamage());

        }
        protected virtual bool BaseAttackZone(){
            if ( target){
                
                Collider[] hitcolliders =  Physics.OverlapSphere(transform.position,baseAttackRadius, PCLAYERMASK);
                foreach(var collider in hitcolliders){
                    if (collider.gameObject == target.gameObject){
                        transform.LookAt(target.transform);
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
        protected virtual bool DetectionZone(){
            
            Collider[] hitcolliders = Physics.OverlapSphere(transform.position,detectionRadius, PCLAYERMASK);
            if (hitcolliders.Length!=0){
                if (!target){
                    target=hitcolliders[0].GetComponent<PlayableCharacter>();
                    transform.LookAt(target.transform);
                    return true;
                }
                else{
                    foreach(var collider in hitcolliders){
                        if (collider.gameObject == target.gameObject){
                            transform.LookAt(target.transform);
                            return true;
                        }
                    }
                    target=null;
                    return false;
                }
            }
            else
                target=null;
                return false;
        }
        protected override void TakeDamage(float damage){
            if (damage< currentHp){
                currentHp-=damage;
                Debug.Log(gameObject.ToString()+" took damage");
            }
            else
                Death();
        }

         protected virtual IEnumerator BaseAttackDamage(){
            isAttacking=true;
            Color baseColor = gameObject.GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.color=Color.red;
            target.SendMessage("TakeDamage",BaseDamage,SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(speed/60f);
            gameObject.GetComponent<Renderer>().material.color=baseColor;
            isAttacking=false;
        }

        protected virtual void OnDrawGizmosSelected(){
            Gizmos.color= Color.red;
            Gizmos.DrawWireSphere(transform.position,baseAttackRadius);
            Gizmos.color=Color.yellow;
            Gizmos.DrawWireSphere(transform.position,detectionRadius);
        }
    }
}
