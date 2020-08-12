using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using User;
using System;
using System.Linq;
//using Books;
namespace Characters.Interfaces{
    public abstract class PlayableCharacter : Character
    {
        [SerializeField] protected int stamina;
        [SerializeField] protected int currentStamina;
        Inventory inventory;
        [SerializeField] protected bool powerMode; //se true sto usando armi, se false consumabili
        [SerializeField] protected Attack equippedAttack;
        [SerializeField] float buffRadius;
        [SerializeField] bool traitor;
        [SerializeField] protected float baseAttackRange;
        
        
        [SerializeField] protected Camera camera;
        [SerializeField] protected  Dictionary<string, bool> bondCheckDict = new Dictionary<string,bool>();
        [SerializeField] protected  Dictionary<string, bool> testDict = new Dictionary<string,bool>();
        [SerializeField] protected  float baseAttackRecoil;
        [SerializeField] protected float specialAttackRecoil;
        
        

        private  Dictionary<string, Action> malusDic = new Dictionary<string,Action>();
        private  Dictionary<string, Action> reverseDic = new Dictionary<string,Action>();
        private Dictionary<Attack, Action> attackDic = new Dictionary<Attack,Action>();
        
        
        // [SerializeField] Book EquippedBook;
        public enum Attack{
            BaseAttack,
            SpecialAttack,
            Book

        }
        void Awake(){
            Awaker();

        }
        // Start is called before the first frame update
        void Start()
        {
            Starter();
        }

        protected virtual void Awaker(){
            
            malusDic.Add("ryuyuki",RyuyukiBond);
            malusDic.Add("genee", GeneeBond);
            malusDic.Add("rayaz", RayazBond);
            reverseDic.Add("ryuyuki",ReverseRyuyukiBond);
            reverseDic.Add("genee", ReverseGeneeBond);
            reverseDic.Add("rayaz", ReverseRayazBond);
            bondCheckDict["ryuyuki"]=false;
            bondCheckDict["genee"]=false;
            bondCheckDict["rayaz"]=false;
            testDict["ryuyuki"]=false;
            testDict["genee"]=false;
            testDict["rayaz"]=false;
            attackDic.Add(Attack.BaseAttack,BaseAttack);
            attackDic.Add(Attack.SpecialAttack,SpecialAttack);
            attackDic.Add(Attack.Book,BookAttack);
            gameObject.layer=8; //PC layer
            IsDeath = false;
            buffRadius=5;
            traitor=false;
            powerMode=true;
            equippedAttack= Attack.BaseAttack;
            isAttacking=false;
            basePower=5;
            baseAttackRecoil=3/100*hp;
            specialAttackRecoil=15/100*hp;
        }
        protected virtual void Starter(){
            currentHp = hp;
            currentStamina = stamina;
            currentSpeed=speed;

        }
        protected virtual void Updater(){
            MalusCheck();
            
            /*if (Input.GetMouseButtonDown(0)){
                Attacker();
            }*/
        }

       
        void Update()
        {
            
            Updater();
        }
        void FixedUpdate(){
            if(Poisoned)
                StartCoroutine(PoisonDamage());
        }

        protected override void Death(){
            IsDeath=true;
            Debug.Log("DEATH");
        }
        protected override  void BaseAttack(){
            if(!isAttacking){
                if (currentHp<=baseAttackRecoil){
                    Debug.Log("cannot use base attack, not much life left");
                }
                else
                    StartCoroutine(BaseAttackDamage());
            }
        }
        protected abstract void SpecialAttack();
        protected void BookAttack(){}
        protected abstract void RyuyukiBond();
        protected abstract void GeneeBond();
        protected abstract void RayazBond();
        protected abstract void ReverseRyuyukiBond();
        protected abstract void ReverseGeneeBond();
        protected abstract void ReverseRayazBond();
        void MalusCheck(){
            ResetDictionary<string,bool>(testDict);
            //Debug.Log(bondCheckDict["rayaz"]);
            int layerMask = 1<<8;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position,buffRadius,layerMask);
            foreach (var colliders in hitColliders){
               if(colliders.gameObject!=this.gameObject){
                    var pc = colliders.GetComponent<PlayableCharacter>();

                //malusDic[pc.ToString()].DynamicInvoke();
                    testDict[pc.ToString()]=true;
               }
            }
            
            foreach(var race in testDict.Keys.ToList() ){
                //Debug.Log(testDict[race]+" "+race+" "+bondCheckDict[race]);
                if (testDict[race]==false && bondCheckDict[race]==true){
                    reverseDic[race].DynamicInvoke();
                    bondCheckDict[race]=false;
                }
                else if (testDict[race]==true && bondCheckDict[race]==false){
                    malusDic[race].DynamicInvoke();
                    bondCheckDict[race]=true;
                }
            }

            
        }
        void ResetDictionary<K,V>(Dictionary<K,V> dict){
            
            foreach (var item in dict.Keys.ToList())
            {
                dict[item]=default;
            }
            
        }

        
        
        
        

        
        protected virtual void OnDrawGizmosSelected(){
            Gizmos.color= Color.green;
            Gizmos.DrawWireSphere(transform.position,buffRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position,camera.transform.forward * baseAttackRange);
            
        }
        public  void Attacker(){
            attackDic[equippedAttack].DynamicInvoke();
        }

        protected abstract IEnumerator BaseAttackDamage();
        protected abstract IEnumerator SpecialEffect();
        

        





    }
}
