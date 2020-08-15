using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using User;
using System;
using System.Linq;
using UnityEngine.UI;
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
        [SerializeField] protected Image HealthBar;
        [SerializeField] protected Image StaminaBar;
        protected Damage baseDamage;
        
        

        private  Dictionary<string, Action> malusDic = new Dictionary<string,Action>();
        private  Dictionary<string, Action> reverseDic = new Dictionary<string,Action>();
        private Dictionary<Attack, Action> attackDic = new Dictionary<Attack,Action>();
        
        
        // enum with the type of Attack equipped
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
        //method used in the Awake
        protected virtual void Awaker(){
            //Debug.Log("Awaker");
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
            
        }
        //method used in the Start
        protected virtual void Starter(){
            //Debug.Log("Starter");
            currentHp = hp;
            FillBar(1,"health");
            currentStamina = stamina;
            FillBar(1,"stamina");
            baseAttackRecoil=hp*3/100;
            specialAttackRecoil=hp*15/100;
            
            currentSpeed=speed;

        }
        //method used in Update
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
        /*Method used to start specialAttackCoroutine*/
        protected abstract void SpecialAttack();
        protected void BookAttack(){
            //TODO
        }
        //Method used to activate malus and bonus with the other races
        protected abstract void RyuyukiBond();
        protected abstract void GeneeBond();
        protected abstract void RayazBond();
        protected abstract void ReverseRyuyukiBond();
        protected abstract void ReverseGeneeBond();
        protected abstract void ReverseRayazBond();
        //Method used to check if a bond it is needed to activate or
        // to deactivate if the race is out of range
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

        //static method that reset value of a given dictionary
        public static void ResetDictionary<K,V>(Dictionary<K,V> dict){
            
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

        //method that invoke the right fuction in base of the equippedAttack
        public  void Attacker(){
            attackDic[equippedAttack].DynamicInvoke();
        }

        //Coroutine to make base attack and check if hitted
        protected abstract IEnumerator BaseAttackDamage();
        //Coroutine to activate special ability
        protected abstract IEnumerator SpecialEffect();

        //method used to set UI health and stamina bars
        protected void FillBar(float value, string type){
            switch(type){
                case "health":
                    HealthBar.fillAmount=value;
                    break;
                case "stamina":
                    StaminaBar.fillAmount=value;
                    break;
                default:
                    break;
            }
            
        }
        

        





    }
}
