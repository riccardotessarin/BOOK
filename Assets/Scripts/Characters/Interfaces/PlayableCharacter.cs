using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using User;
using System;
using System.Linq;
using UnityEngine.UI;
using Consumables.Books;
using Consumables.Books.Abilities;
using Consumables.Healables.Plants;
using MalusEBonus;
using Managers;

//using Books;
namespace Characters.Interfaces {
    public abstract class PlayableCharacter : Character {
        [SerializeField] protected float stamina;
        [SerializeField] protected float currentStamina;
        Inventory inventory;
        [SerializeField] private bool powerMode; //se true sto usando armi, se false consumabili
        public bool PowerMode{get=>powerMode; set=>powerMode=value;}
        [SerializeField] protected Attack equippedAttack;
        
        [SerializeField] protected Book equippedBook;
        IList<Book> listBooks;
        public IList<Book> ListBooks{get=>listBooks;}

        
        
        [SerializeField] protected Plant equippedPlant;
        IList<Plant> listPlants;
        public IList<Plant> ListPlants{get=>listPlants;}
        [SerializeField] float buffRadius;
        [SerializeField] bool traitor;
        [SerializeField] protected float baseAttackRange;


        [SerializeField] protected Camera camera;
        [SerializeField] protected Dictionary<string, bool> bondCheckDict = new Dictionary<string, bool>();
        [SerializeField] protected Dictionary<string, bool> testDict = new Dictionary<string, bool>();
        [SerializeField] protected float baseAttackRecoil;
        [SerializeField] protected float specialAttackRecoil;
        protected Damage baseDamage;
        private Dictionary<string, Action> bondDic = new Dictionary<string, Action>();
        private Dictionary<string, Action> reverseDic = new Dictionary<string, Action>();
        private Dictionary<Attack, Action> attackDic = new Dictionary<Attack, Action>();


        public MalusManager malusManager;
        [SerializeField] protected UIManager uIManager;
        public UIManager UIManager{get=>uIManager;}
        [SerializeField] protected Sprite baseAttackSprite;
        [SerializeField] protected Sprite specialAttackSprite;

        public Sprite BaseAttackSprite{get=>baseAttackSprite;}
        public Sprite SpecialAttackSprite{get=>specialAttackSprite;}

        public const string INFINITY="\u221E";
        
        protected string baseAttackDescription; //max  circa 132 caratteri
        protected string specialAttackDescription;
        public string BaseAttackDescription{get=>baseAttackDescription;}
        public string SpecialAttackDescription{get=>specialAttackDescription;}
        


        // enum with the type of Attack equipped
        public enum Attack {
            BaseAttack, //0
            SpecialAttack, //1
            Book //2
        }

        void Awake() {
            Awaker();
        }

        // Start is called before the first frame update
        void Start() {
            Starter();
        }

        //method used in the Awake
        protected virtual void Awaker() {
            //Debug.Log("Awaker");
            bondDic.Add("ryuyuki", RyuyukiBond);
            bondDic.Add("genee", GeneeBond);
            bondDic.Add("rayaz", RayazBond);
            reverseDic.Add("ryuyuki", ReverseRyuyukiBond);
            reverseDic.Add("genee", ReverseGeneeBond);
            reverseDic.Add("rayaz", ReverseRayazBond);
            bondCheckDict["ryuyuki"] = false;
            bondCheckDict["genee"] = false;
            bondCheckDict["rayaz"] = false;
            testDict["ryuyuki"] = false;
            testDict["genee"] = false;
            testDict["rayaz"] = false;
            attackDic.Add(Attack.BaseAttack, BaseAttack);
            attackDic.Add(Attack.SpecialAttack, SpecialAttack);
            attackDic.Add(Attack.Book, BookAttack);
            gameObject.layer = 8; //PC layer
            IsDeath = false;
            buffRadius = 5;
            traitor = false;
            powerMode = true;
            equippedAttack = Attack.BaseAttack;
            
            isAttacking = false;
            basePower = 5;
            if(!malusManager){
                malusManager=new GameObject().AddComponent<MalusManager>();
                malusManager.name="Malus Manager";
                malusManager.player=this;
            }
            inventory= new GameObject().AddComponent<Inventory>();
            inventory.name="Inventory";
            UpdateObjectsLists();
            
            uIManager=GameObject.FindWithTag("UIManager").GetComponent<UIManager>();
            
            
        }

        //method used in the Start
        protected virtual void Starter() {
            //Debug.Log("Starter");
            baseAttackSprite=Resources.Load<Sprite>($"Images/{this.type}BaseAttack");
            specialAttackSprite=Resources.Load<Sprite>($"Images/{this.type}SpecialAttack");
            currentHp = hp;
            
            uIManager.FillBar(1, "health");
            currentStamina = stamina;
            
            uIManager.FillBar(1, "stamina");
            baseAttackRecoil = hp * 3 / 100;
            specialAttackRecoil = hp * 15 / 100;
            currentBasePower=basePower;
            currentSpeed = speed;
            if(listPlants.Count()!=0){
                equippedPlant=listPlants[0];
            }
            
        }

        //method used in Update
        protected virtual void Updater() {
            MalusCheck();
            UpdateObjectsLists();

            
        }

        void Update() {
            Updater();
        }

        void FixedUpdate() {
            if (Poisoned)
                StartCoroutine(PoisonDamage());
        }

        protected override void Death() {
            IsDeath = true;
            Debug.Log("DEATH");
        }

        protected override void BaseAttack() {
            if (!isAttacking) {
                if (currentHp <= baseAttackRecoil) {
                    Debug.Log("cannot use base attack, not much life left");
                } else
                    StartCoroutine(BaseAttackDamage());
            }
        }

        /*Method used to start specialAttackCoroutine*/
        protected abstract void SpecialAttack();

        protected void BookAttack() {
            int count=listBooks.Count();
            int index=listBooks.IndexOf(equippedBook);
            equippedBook.UseConsumable();
            UIManager.ChangeChargeText(equippedBook.CurrentCharges.ToString());
            if(equippedBook.CurrentCharges==0){
                CheckChargeBook(index,count);
            }
        }

        private void CheckChargeBook(int index,int count){
            if(count==1){
                equippedAttack=Attack.BaseAttack;
                equippedBook=null;
                UIManager.DestroyCurrentObject(specialAttackSprite);
                UIManager.ChangeChargeText(INFINITY);
                UIManager.ChangeDescriptionText(BaseAttackDescription);
            }
            else if(count==2){
                if(index==0){
                    equippedBook=listBooks[0];
                    UIManager.DestroyCurrentObject(baseAttackSprite);
                    UIManager.ChangeChargeText(equippedBook.CurrentCharges.ToString());
                    UIManager.ChangeDescriptionText(equippedBook.Description);
                }
                else{
                    equippedAttack=Attack.BaseAttack;
                    equippedBook=null;
                    UIManager.DestroyCurrentObject(specialAttackSprite);
                    UIManager.ChangeChargeText(INFINITY);
                    UIManager.ChangeDescriptionText(baseAttackDescription);
                }
                    
            }
            else if (count>2){
                if(index==0){
                    equippedBook=listBooks[0];
                    UIManager.DestroyCurrentObject(listBooks[1].BookIcon);
                    UIManager.ChangeChargeText(equippedBook.CurrentCharges.ToString());
                    UIManager.ChangeDescriptionText(equippedBook.Description);
                }
                else if(index==count-1){
                    equippedAttack=Attack.BaseAttack;
                    equippedBook=null;
                    UIManager.DestroyCurrentObject(specialAttackSprite);
                    UIManager.ChangeChargeText(INFINITY);
                    UIManager.ChangeDescriptionText(baseAttackDescription);
                }
                else{
                    equippedBook=listBooks[index];
                    if(index==count-2){
                        UIManager.DestroyCurrentObject(baseAttackSprite);
                    }
                    else{
                        UIManager.DestroyCurrentObject(listBooks[index+2].BookIcon);
                    }
                    UIManager.ChangeChargeText(equippedBook.CurrentCharges.ToString());
                    UIManager.ChangeDescriptionText(equippedBook.Description);
                }
            }
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
        void MalusCheck() {
            ResetDictionary<string, bool>(testDict);
            //Debug.Log(bondCheckDict["rayaz"]);
            int layerMask = 1 << 8;
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, buffRadius, layerMask);
            foreach (var colliders in hitColliders) {
                if (colliders.gameObject != this.gameObject) {
                    var pc = colliders.GetComponent<PlayableCharacter>();

                    //malusDic[pc.ToString()].DynamicInvoke();
                    testDict[pc.ToString()] = true;
                }
            }

            foreach (var race in testDict.Keys.ToList()) {
                //Debug.Log(testDict[race]+" "+race+" "+bondCheckDict[race]);
                if (testDict[race] == false && bondCheckDict[race] == true) {
                    reverseDic[race].DynamicInvoke();
                    bondCheckDict[race] = false;
                } else if (testDict[race] == true && bondCheckDict[race] == false) {
                    bondDic[race].DynamicInvoke();
                    bondCheckDict[race] = true;
                }
            }
        }

        //static method that reset value of a given dictionary
        public static void ResetDictionary<K, V>(Dictionary<K, V> dict) {
            foreach (var item in dict.Keys.ToList()) {
                dict[item] = default;
            }
        }

        protected virtual void OnDrawGizmosSelected() {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, buffRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, camera.transform.forward * baseAttackRange);
        }

        //method that invoke the right fuction in base of the equippedAttack
        public void Attacker() {
            attackDic[equippedAttack].DynamicInvoke();
        }

        //Coroutine to make base attack and check if hitted
        protected abstract IEnumerator BaseAttackDamage();

        //Coroutine to activate special ability
        protected abstract IEnumerator SpecialEffect();

        //method used to set UI health and stamina bars
        

        protected virtual void ModifyStaminaMax(float modifier){
            stamina*=modifier;
            if(currentStamina>stamina)
                currentStamina=stamina;
            Debug.Log(ToString()+" maxStamina modified: "+stamina);
        }

        public void UseEquippedConsumable(){
            if(equippedPlant!=null){
                int index=listPlants.IndexOf(equippedPlant);
                int count=listPlants.Count();
                equippedPlant.UseConsumable();
                CheckPlantInventory(index,count);
            }
        }
        private void CheckPlantInventory(int index,int count){
            if(count==1){
                equippedPlant=null;
                UIManager.DestroyCurrentObject(UIManager.VoidSPrite);
                UIManager.ChangeDescriptionText("");
                powerMode=!powerMode;
                UIManager.SwitchMode(powerMode);
            }
            else if(count==2){
                equippedPlant=listPlants[0];
                if(index==0){
                    UIManager.DestroyCurrentObject(UIManager.VoidSPrite);
                }
                else if(index==1){
                    UIManager.DestroyCurrentObject(equippedPlant.PlantIcon);
                    UIManager.ScrollDownMenu(UIManager.VoidSPrite);
                }
                UIManager.ChangeDescriptionText(equippedPlant.Description);
            }
            else if(count==3){
                if (index==0 ||index==count-1){
                    equippedPlant=listPlants[0];
                    UIManager.ScrollUpMenu(equippedPlant.PlantIcon);
                    UIManager.ScrollUpMenu(UIManager.VoidSPrite);
                }
                else{
                    equippedPlant=listPlants[index];
                    UIManager.DestroyCurrentObject(UIManager.VoidSPrite);
                }
                UIManager.ChangeDescriptionText(equippedPlant.Description);
            }
            else if(count>3){
                if(index==0 || index==count-1){
                    equippedPlant=ListPlants[0];
                    UIManager.DestroyCurrentObject(ListPlants[1].PlantIcon);
                }
                else{
                    equippedPlant=ListPlants[index];
                    if(index==count-2){
                        UIManager.DestroyCurrentObject(ListPlants[0].PlantIcon);
                    }
                    else{
                        UIManager.DestroyCurrentObject(ListPlants[index+1].PlantIcon);
                    }
                }
                UIManager.ChangeDescriptionText(equippedPlant.Description);
            }
        }

        protected void UpdateObjectsLists(){
            listBooks= inventory.Books;
            listPlants=inventory.Plants;
        }


        public void ScrollUpInventory(){
            if(PowerMode){
                int count= listBooks.Count();
                if(equippedAttack==Attack.BaseAttack){
                    if(count!=0){
                        equippedAttack=Attack.Book;
                        equippedBook=listBooks[count-1];
                        if(count==1){
                            UIManager.ScrollUpMenu(SpecialAttackSprite);
                        }
                        else{
                            UIManager.ScrollUpMenu(listBooks[count-2].BookIcon);
                        }
                        UIManager.ChangeChargeText(equippedBook.CurrentCharges.ToString());
                        UIManager.ChangeDescriptionText(equippedBook.Description);

                    }
                    else{
                        equippedAttack=Attack.SpecialAttack;
                        UIManager.ScrollUpMenu(BaseAttackSprite);
                        UIManager.ChangeDescriptionText(specialAttackDescription);
                    }
                }
                else if(equippedAttack==Attack.SpecialAttack){
                    equippedAttack=Attack.BaseAttack;
                    if(count==0){
                        UIManager.ScrollUpMenu(SpecialAttackSprite);
                    }
                    else{
                        UIManager.ScrollUpMenu(listBooks[count-1].BookIcon);
                    }
                    UIManager.ChangeDescriptionText(baseAttackDescription);
                }
                else if (equippedAttack==Attack.Book){
                    int index=listBooks.IndexOf(equippedBook);
                    if (index==0){
                        equippedAttack=Attack.SpecialAttack;
                        equippedBook=null;
                        UIManager.ScrollUpMenu(BaseAttackSprite);
                        UIManager.ChangeChargeText(INFINITY);
                        UIManager.ChangeDescriptionText(specialAttackDescription);
                    }
                    else{
                        equippedBook=listBooks[index-1];
                        if(index==1){
                            UIManager.ScrollUpMenu(SpecialAttackSprite);
                        }
                        else{
                            UIManager.ScrollUpMenu(listBooks[index-2].BookIcon);
                        }
                        UIManager.ChangeChargeText(equippedBook.CurrentCharges.ToString());
                        UIManager.ChangeDescriptionText(equippedBook.Description);
                    }
                }


            }
            else{
                int count=listPlants.Count();
                if(count>1){
                    int index=listPlants.IndexOf(equippedPlant);
                    if(index==0){
                        equippedPlant=listPlants[count-1];
                        if(count==2){
                            UIManager.ScrollUpMenu(equippedPlant.PlantIcon);
                            UIManager.ScrollUpMenu(listPlants[0].PlantIcon);
                        }
                        else{
                            UIManager.ScrollUpMenu(listPlants[count-2].PlantIcon);
                        }
                        UIManager.ChangeDescriptionText(equippedPlant.Description);

                    }
                    else{
                        
                        equippedPlant=listPlants[index-1];
                        if(count==2){
                            UIManager.ScrollUpMenu(UIManager.VoidSPrite);
                        }
                        else if (index<2){
                            UIManager.ScrollUpMenu(listPlants[count-1].PlantIcon);
                        }
                        else{
                            
                            UIManager.ScrollUpMenu(listPlants[index-2].PlantIcon);
                        }
                        UIManager.ChangeDescriptionText(equippedPlant.Description);
                    }
                }

            }
        }

        public void ScrollDownInventory(){
            if(PowerMode){
                int count= listBooks.Count();
                if(equippedAttack==Attack.BaseAttack){
                    equippedAttack=Attack.SpecialAttack;
                    if(count==0){
                        UIManager.ScrollDownMenu(BaseAttackSprite);
                    }
                    else{
                        UIManager.ScrollDownMenu(listBooks[0].BookIcon);
                    }
                    UIManager.ChangeDescriptionText(specialAttackDescription);
                }
                else if (equippedAttack==Attack.SpecialAttack){
                    if(count!=0){
                        equippedAttack=Attack.Book;
                        equippedBook=listBooks[0];
                        if(count==1){
                            UIManager.ScrollDownMenu(BaseAttackSprite);
                        }
                        else{
                            UIManager.ScrollDownMenu(listBooks[1].BookIcon);
                        }
                        UIManager.ChangeChargeText(equippedBook.CurrentCharges.ToString());
                        UIManager.ChangeDescriptionText(equippedBook.Description);
                    }
                    else{
                        equippedAttack=Attack.BaseAttack;
                        UIManager.ScrollDownMenu(SpecialAttackSprite);
                        UIManager.ChangeDescriptionText(baseAttackDescription);
                    }
                }
                else if(equippedAttack==Attack.Book){
                    int index=listBooks.IndexOf(equippedBook);
                    if(index==count-1){
                        equippedAttack=Attack.BaseAttack;
                        equippedBook=null;
                        UIManager.ScrollDownMenu(SpecialAttackSprite);
                        UIManager.ChangeChargeText(INFINITY);
                        UIManager.ChangeDescriptionText(baseAttackDescription);
                    }
                    else{
                        equippedBook=listBooks[index+1];
                        if( index==count-2){
                            UIManager.ScrollDownMenu(BaseAttackSprite);
                        }
                        else{
                            UIManager.ScrollDownMenu(listBooks[index+2].BookIcon);
                        }
                        UIManager.ChangeChargeText(equippedBook.CurrentCharges.ToString());
                        UIManager.ChangeDescriptionText(equippedBook.Description);

                    }
                }
            }
            else{
                int count=listPlants.Count();
                if(count>1){
                    int index=listPlants.IndexOf(equippedPlant);
                    if(index==count-1){
                        equippedPlant=listPlants[0];
                        if(count==2){
                            UIManager.ScrollDownMenu(equippedPlant.PlantIcon);
                            
                        }
                        UIManager.ScrollDownMenu(listPlants[1].PlantIcon);
                        UIManager.ChangeDescriptionText(equippedPlant.Description);
                    }
                    else{
                        equippedPlant=listPlants[index+1];
                        if(count==2){
                            UIManager.ScrollDownMenu(UIManager.VoidSPrite);
                        }
                        else if(index==count-2){
                            UIManager.ScrollDownMenu(listPlants[0].PlantIcon);
                        }
                        else{
                            UIManager.ScrollDownMenu(listPlants[index+2].PlantIcon);
                        }
                        UIManager.ChangeDescriptionText(equippedPlant.Description);
                    }
                }
            }
        }



        

        




    }
}