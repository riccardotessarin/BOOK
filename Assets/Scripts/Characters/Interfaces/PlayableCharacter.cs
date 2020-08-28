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
using Managers.UI;
using Consumables;
using Characters.NPC;
using Consumables.Pages;
using Consumables.Healables.Plants.Drops;
using Consumables.Books.Drops;

//using Books;
namespace Characters.Interfaces {
    public abstract class PlayableCharacter : Character {
        [SerializeField] protected float stamina;
        [SerializeField] protected float currentStamina;
        Inventory inventory;
        [SerializeField] private bool powerMode; //se true sto usando armi, se false consumabili
        public bool PowerMode{get=>powerMode; set=>powerMode=value;}
        [SerializeField] protected Attack equippedAttack;
        public Attack EquippedAttack{get=>equippedAttack;set=>equippedAttack=value;}
        [SerializeField] protected Book equippedBook;
        public Book EquippedBook{get=>equippedBook;set=>equippedBook=value;}
        IList<Book> listBooks;
        public IList<Book> ListBooks{get=>listBooks;}

        
        
        [SerializeField] protected Plant equippedPlant;
        public Plant EquippedPlant{get=>equippedPlant;set=>equippedPlant=value;}
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
        private UIController uIController;
        public UIController UIController{get=>uIController;}

        [SerializeField]private float interactionDistance;
        [SerializeField]private bool isLooting;
        [SerializeField]protected EnumUtility.AttackType weakness;
        public const float weaknessMultiplicator=1.2f;

        
        


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
            isLooting=false;
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
            uIController= new UIController();
            uIController.player=this;
            uIController.uIManager=this.uIManager;
            interactionDistance=3;    
            looted=false;
            weakness=EnumUtility.AttackType.Nothing;
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
            InteractionTextRayCast();

            
        }

        void Update() {
            Updater();
        }

        void FixedUpdate() {
            if (Poisoned)
                StartCoroutine(PoisonDamage());
            
            
        }
        protected void InteractionTextRayCast(){
            RaycastHit hit;
            if(Physics.Raycast(transform.position,camera.transform.forward,out hit,interactionDistance)){
                
                uIController.InteractionTextControl(hit,traitor);
            }
            else{
                
                uIController.ResetInteractionText();
            }
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
            uIController.CheckChargeBook(index,count);
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
            Gizmos.color=Color.gray;
            Gizmos.DrawRay(transform.position,camera.transform.forward*interactionDistance);
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
                uIController.CheckPlantInventory(index,count);
            }
        }
        /*private void CheckPlantInventory(int index,int count){
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
        }*/

        protected void UpdateObjectsLists(){
            listBooks= inventory.Books;
            listPlants=inventory.Plants;
        }
        public Book GetDropBook(){
            int count=listBooks.Count();
            if(count!=0){
                return listBooks[UnityEngine.Random.Range(0,count)];
            }
            else
                return null;
        }
        public Plant GetDropPlant(){
            int count=listPlants.Count();
            if(count!=0){
                return listPlants[UnityEngine.Random.Range(0,count)];
            }
            else
                return null;
        }


        public void LootAction(){
            RaycastHit hit;
            if( !isAttacking && !isLooting && Physics.Raycast(transform.position,camera.transform.forward,out hit,interactionDistance) ){
                Character hitted=hit.collider.GetComponent<Character>();
                if(hitted && hitted.IsDeath && !hitted.Looted){
                    if(hitted is PlayableCharacter && traitor){
                        StartCoroutine(Loot(hitted,typeof(PlayableCharacter)));
                    }
                    else if(hitted is MeltingKinean && hitted.GetComponent<MeltingKinean>().Drop){
                        StartCoroutine(Loot(hitted,typeof(MeltingKinean)));
                    }
                    else if(hitted is CyborgKinean){
                        StartCoroutine(Loot(hitted,typeof(CyborgKinean)));
                    }
                }
            }
        }
        private IEnumerator Loot(Character dead, Type deadType){
            isAttacking=true;
            isLooting=true;
            dead.Looted=true;
            if(deadType==typeof(PlayableCharacter)){
                PlayableCharacter deadPC=dead.GetComponent<PlayableCharacter>();
                Book dropBook=deadPC.GetDropBook();
                Plant dropPlant=deadPC.GetDropPlant();
                bool plantAdded=inventory.TryAddConsumableToInventory(dropPlant);
                bool bookAdded=inventory.TryAddConsumableToInventory(dropBook);
                if(!plantAdded && !bookAdded){
                    deadPC.Looted=false;
                }
                else{
                    Debug.Log("Looting Traitor");
                    if(bookAdded){
                        uIManager.AddBook(dropBook);
                    }
                    if(plantAdded){
                        UIManager.AddPlant(dropPlant);
                        if(listPlants.Count()==1){
                            equippedPlant=dropPlant;
                        }
                    }
                }
            }
            else if(deadType==typeof(MeltingKinean)){
                MeltingKinean deadNPC=dead.GetComponent<MeltingKinean>();
                if(deadNPC.DropPage){
                    Page page=deadNPC.GetDropPage();
                    bool affectEquippedBook= (equippedBook.CurrentCharges<equippedBook.Charges && equippedBook.PageType==page.Type);
                    if(!inventory.TryAddConsumableToInventory(page)){
                        deadNPC.Looted=false;
                    }
                    else{
                        if(affectEquippedBook){
                            uIManager.ChangeChargeText(equippedBook.CurrentCharges.ToString());
                        }
                    }
                }
            }
            else if(deadType==typeof(CyborgKinean)){
                CyborgKinean deadNPC=dead.GetComponent<CyborgKinean>();
                Book dropBook=deadNPC.GetDrop();
                if(!inventory.TryAddConsumableToInventory(dropBook)){
                    deadNPC.Looted=false;
                }
                else{
                    uIManager.AddBook(dropBook);
                }
            }
            yield return new WaitForSeconds(0.5f);
            isAttacking=false;
            isLooting=false;
            

        }

        public void InteractAction(){
            RaycastHit hit;
            if(!isAttacking && !isLooting &&  Physics.Raycast(transform.position,camera.transform.forward,out hit,interactionDistance)){
                if(hit.collider.GetComponent<PlantDrop>()){
                    StartCoroutine(InteractPlant(hit.collider.GetComponent<Plant>()));
                }
                else if(hit.collider.GetComponent<PlayableCharacter>()){
                    StartCoroutine(ReviveTeamMember(hit.collider.GetComponent<PlayableCharacter>()));
                }
            }
        }

        private IEnumerator InteractPlant(Plant interacted){
            isAttacking=true;
            isLooting=true;
            if(!inventory.TryAddConsumableToInventory(interacted)){
                //failed interaction
            }
            else{
                uIManager.AddPlant(interacted);
            }
            yield return new WaitForSeconds(0.5f);
            isAttacking=false;
            isLooting=false;
        }

        private IEnumerator ReviveTeamMember(PlayableCharacter revived){
            isAttacking=true;
            isLooting=true;
            yield return new WaitForSecondsRealtime(3f);
            revived.SendMessage("Revive",SendMessageOptions.DontRequireReceiver);
            isLooting=false;
            isAttacking=false;

        }

        private void Revive(){
            Debug.Log($"{gameObject.ToString()} revived");
            currentHp=hp/6;
            isDeath=false;
        }

        protected void ModifyRecoil(float modifier){
            baseAttackRecoil*=modifier;
            specialAttackRecoil*=modifier;
            Debug.Log(ToString()+" recoil Modified: "+baseAttackRecoil.ToString()+" "+specialAttackRecoil.ToString());
        }

        protected abstract void ModifyWeakness(float modifier);

        protected virtual void ModifyElementalPower(float modifier){}






        

        




    }
}