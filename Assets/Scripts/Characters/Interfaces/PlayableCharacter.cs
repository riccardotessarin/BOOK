using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Consumables.Books;
using Consumables.Healables.Plants;
using MalusEBonus;
using Managers.UI;
using Characters.NPC;
using Consumables.Pages;
using Consumables.Healables.Plants.Drops;
using Consumables.Books.Drops;
using Networking.GameControllers;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine;
using User;
using Photon.Pun;

//using Books;
namespace Characters.Interfaces {
    public abstract class PlayableCharacter : Character {
        [SerializeField] private AudioListener audioListener;

        private PhotonView _photonView;

        public PhotonPlayer Player { get; set; }

        public bool IsAttacking {
            get => isAttacking;
        }

        [SerializeField] protected EnumUtility.CharacterType raceType;

        public EnumUtility.CharacterType RaceType {
            get => raceType;
        }

        [SerializeField] protected float stamina;
        [SerializeField] protected float currentStamina;
        Inventory inventory;
        [SerializeField] private bool powerMode; //se true sto usando armi, se false consumabili

        public bool PowerMode {
            get => powerMode;
            set => powerMode = value;
        }

        [SerializeField] protected Attack equippedAttack;

        public Attack EquippedAttack {
            get => equippedAttack;
            set => equippedAttack = value;
        }

        [SerializeField] protected Book equippedBook;

        public Book EquippedBook {
            get => equippedBook;
            set => equippedBook = value;
        }

        IList<Book> listBooks;

        public IList<Book> ListBooks {
            get => listBooks;
        }


        [SerializeField] protected Plant equippedPlant;

        public Plant EquippedPlant {
            get => equippedPlant;
            set => equippedPlant = value;
        }

        IList<Plant> listPlants;

        public IList<Plant> ListPlants {
            get => listPlants;
        }

        [SerializeField] float buffRadius;
        [SerializeField] bool traitor;
        [SerializeField] protected float baseAttackRange;


        [SerializeField] protected Camera camera;

        public Camera Camera => camera;

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

        public UIManager UIManager {
            get => uIManager;
        }

        [SerializeField] protected Sprite baseAttackSprite;
        [SerializeField] protected Sprite specialAttackSprite;

        public Sprite BaseAttackSprite {
            get => baseAttackSprite;
        }

        public Sprite SpecialAttackSprite {
            get => specialAttackSprite;
        }

        public const string INFINITY = "\u221E";

        protected string baseAttackDescription; //max  circa 132 caratteri
        protected string specialAttackDescription;

        public string BaseAttackDescription {
            get => baseAttackDescription;
        }

        public string SpecialAttackDescription {
            get => specialAttackDescription;
        }

        private UIController uIController;

        public UIController UIController {
            get => uIController;
        }

        [SerializeField] private float interactionDistance;
        [SerializeField] protected EnumUtility.AttackType weakness;
        public const float weaknessMultiplicator = 1.2f;
        [SerializeField] protected Sprite weaknessSprite;

        public Sprite WeaknessSprite {
            get => weaknessSprite;
        }

        [SerializeField] protected Sprite elementSprite;

        public Sprite ElementSprite {
            get => elementSprite;
        }

        [SerializeField] protected EnumUtility.AttackType elementType;
        [SerializeField] protected bool activateElementBonus;
        [SerializeField] protected float staminaConsumed;
        [SerializeField] protected bool staminaRecharging = false;
        [SerializeField] protected float staminaRecharged;
        [SerializeField] protected bool isMine;
        public bool IsMine=>isMine;

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
            _photonView = GetComponent<PhotonView>();
            isMine=_photonView.IsMine;
            if (isMine) {
                camera.enabled = true;
                audioListener.enabled = true;

                uIManager = new GameObject().AddComponent<UIManager>();
                uIManager.Player = this;
                uIManager = UIManager.Instance;
            }

            //Debug.Log("AwakerPC");
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
            gameObject.tag = "Player";

            IsDeath = false;
            buffRadius = 5;
            traitor = false;
            powerMode = true;
            equippedAttack = Attack.BaseAttack;

            isAttacking = false;
            basePower = 5;
            if (!malusManager && isMine) {
                malusManager = new GameObject().AddComponent<MalusManager>();
                malusManager.name = "Malus Manager";
                malusManager.player = this;
            }

            inventory = new GameObject().AddComponent<Inventory>();
            inventory.name = "Inventory";
            UpdateObjectsLists();

            interactionDistance = 3;
            looted = false;
            weakness = EnumUtility.AttackType.Nothing;
            activateElementBonus = false;
        }

        //method used in the Start
        protected virtual void Starter() {
            //Debug.Log("Starter");
            baseAttackSprite = Resources.Load<Sprite>($"Images/{this.type}BaseAttack");
            specialAttackSprite = Resources.Load<Sprite>($"Images/{this.type}SpecialAttack");
            currentHp = hp;
            currentStamina = stamina;
            
                uIController = new UIController();
                uIController.player = this;
            if(isMine){
                uIController.uIManager = UIManager.Instance;

                uIManager.FillBar(1, "health");
                

                uIManager.FillBar(1, "stamina");
            }
            baseAttackRecoil = hp * 3 / 100;
            specialAttackRecoil = hp * 15 / 100;
            staminaConsumed = stamina * 10 / 100;
            staminaRecharged = stamina * 5 / 100;
            currentBasePower = basePower;
            currentSpeed = speed;
            if (listPlants.Count() != 0) {
                equippedPlant = listPlants[0];
            }
        }

        //method used in Update
        protected virtual void Updater() {
            MalusCheck();
            UpdateObjectsLists();
            if (gameObject.GetComponent<FirstPersonController>())
                InteractionTextRayCast();
            ResetStamina();
        }

        void Update() {
            Updater();
        }

        void FixedUpdate() {
            if (Poisoned)
                StartCoroutine(PoisonDamage());
        }

        protected void InteractionTextRayCast() {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, camera.transform.forward, out hit, interactionDistance)) {
                uIController.InteractionTextControl(hit, traitor);
            } else {
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
                } else {
                    _photonView.RPC("RPC_BasicAttack", RpcTarget.All, null);
                    UseStamina(staminaConsumed);
                }
            }
        }

        ///<summary>
        ///Method used to start specialAttackCoroutine
        ///</summary>
        protected abstract void SpecialAttack();

        protected void BookAttack() {
            if (!isAttacking) {
                if (equippedBook.Element != weakness) {
                    UseStamina(staminaConsumed);
                    int count = listBooks.Count();
                    int index = listBooks.IndexOf(equippedBook);
                    StartCoroutine(UseBook());
                    UseStamina(staminaConsumed);
                    uIController.CheckChargeBook(index, count);
                } else {
                    Debug.Log("Cannot use book because of weakness");
                }
            }
        }

        protected IEnumerator UseBook() {
            isAttacking = true;
            equippedBook.UseConsumable();
            yield return new WaitForSeconds(speed / 120f);
            isAttacking = false;
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
                    if (!pc.IsDeath) {
                        //malusDic[pc.ToString()].DynamicInvoke();
                        testDict[pc.ToString()] = true;
                    }
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

        ///<summary>
        ///static method that reset value of a given dictionary
        ///</summary>
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
            Gizmos.color = Color.gray;
            Gizmos.DrawRay(transform.position, camera.transform.forward * interactionDistance);
        }

        ///<summary>
        ///method that invoke the right fuction in base of the equippedAttack
        ///</summary>
        public void Attacker() {
            if (currentStamina >= staminaConsumed)
                attackDic[equippedAttack].DynamicInvoke();
        }

        //Coroutine to make base attack and check if hitted
        protected abstract IEnumerator BaseAttackDamage();

        //Coroutine to activate special ability
        protected abstract IEnumerator SpecialEffect();


        protected virtual void ModifyStaminaMax(float modifier) {
            stamina *= modifier;
            if (currentStamina > stamina)
                currentStamina = stamina;
            Debug.Log(ToString() + " maxStamina modified: " + stamina);
        }

        public void UseEquippedConsumable() {
            if (equippedPlant != null) {
                int index = listPlants.IndexOf(equippedPlant);
                int count = listPlants.Count();
                equippedPlant.UseConsumable();
                uIController.CheckPlantInventory(index, count);
            }
        }


        protected void UpdateObjectsLists() {
            listBooks = inventory.Books;
            listPlants = inventory.Plants;
        }

        public Book GetDropBook() {
            int count = listBooks.Count();
            if (count != 0) {
                return listBooks[UnityEngine.Random.Range(0, count)];
            } else
                return null;
        }

        public Plant GetDropPlant() {
            int count = listPlants.Count();
            if (count != 0) {
                return listPlants[UnityEngine.Random.Range(0, count)];
            } else
                return null;
        }

        ///<summary>
        /// method that makes player loot
        ///</summary>
        public void LootAction() {
            RaycastHit hit;
            if (!isAttacking && Physics.Raycast(transform.position, camera.transform.forward, out hit, interactionDistance)) {
                Character hitted = hit.collider.GetComponent<Character>();
                if (hitted && hitted.IsDeath && !hitted.Looted) {
                    if (hitted is PlayableCharacter && traitor) {
                        StartCoroutine(Loot(hitted, typeof(PlayableCharacter)));
                    } else if (hitted is MeltingKinean && hitted.GetComponent<MeltingKinean>().Drop) {
                        StartCoroutine(Loot(hitted, typeof(MeltingKinean)));
                    } else if (hitted is CyborgKinean) {
                        StartCoroutine(Loot(hitted, typeof(CyborgKinean)));
                    }
                }
            }
        }

        private IEnumerator Loot(Character dead, Type deadType) {
            isAttacking = true;
            dead.Looted = true;
            if (deadType == typeof(PlayableCharacter)) {
                PlayableCharacter deadPC = dead.GetComponent<PlayableCharacter>();
                Book dropBook = deadPC.GetDropBook();
                Plant dropPlant = deadPC.GetDropPlant();
                bool plantAdded = inventory.TryAddConsumableToInventory(dropPlant);
                bool bookAdded = inventory.TryAddConsumableToInventory(dropBook);
                if (!plantAdded && !bookAdded) {
                    deadPC.Looted = false;
                } else {
                    Debug.Log("Looting Traitor");
                    if (bookAdded && isMine) {
                        uIManager.AddBook(dropBook.BookIcon);
                    }

                    if (plantAdded) {
                        if(isMine)
                            UIManager.AddPlant(dropPlant.PlantIcon, dropPlant.Description);
                        if (listPlants.Count() == 1) {
                            equippedPlant = dropPlant;
                        }
                    }
                }
            } else if (deadType == typeof(MeltingKinean)) {
                MeltingKinean deadNPC = dead.GetComponent<MeltingKinean>();
                if (deadNPC.DropPage) {
                    Page page = deadNPC.GetDropPage();
                    bool affectEquippedBook = (equippedBook.CurrentCharges < equippedBook.Charges && equippedBook.PageType == page.Type);
                    if (!inventory.TryAddConsumableToInventory(page)) {
                        deadNPC.Looted = false;
                    } else {
                        if (affectEquippedBook && isMine) {
                            uIManager.ChangeChargeText(equippedBook.CurrentCharges.ToString());
                        }
                    }
                }
            } else if (deadType == typeof(CyborgKinean)) {
                CyborgKinean deadNPC = dead.GetComponent<CyborgKinean>();
                BookDrop dropBook = deadNPC.GetDrop();
                Sprite booksprite = dropBook.BookIcon;
                if (!dropBook.PickDrop(inventory)) {
                    deadNPC.Looted = false;
                } else if(isMine) {
                    uIManager.AddBook(booksprite);
                }
            }

            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
        }

        ///<summary>
        ///method that makes player interact with interagible object or revive team member
        ///</summary>
        public void InteractAction() {
            RaycastHit hit;
            if (!isAttacking && Physics.Raycast(transform.position, camera.transform.forward, out hit, interactionDistance)) {
                if (hit.collider.GetComponent<PlantDrop>()) {
                    StartCoroutine(InteractPlant(hit.collider.GetComponent<PlantDrop>()));
                } else if (hit.collider.GetComponent<PlayableCharacter>()) {
                    StartCoroutine(ReviveTeamMember(hit.collider.GetComponent<PlayableCharacter>()));
                }
            }
        }

        private IEnumerator InteractPlant(PlantDrop interacted) {
            isAttacking = true;
            Sprite plantSprite = interacted.PlantIcon;
            string description = interacted.Description;
            if (!interacted.PickDrop(inventory)) {
                //failed interaction
            } else if (isMine){
                uIManager.AddPlant(plantSprite, description);
            }

            yield return new WaitForSeconds(0.5f);
            isAttacking = false;
        }

        private IEnumerator ReviveTeamMember(PlayableCharacter revived) {
            isAttacking = true;

            yield return new WaitForSecondsRealtime(3f);
            revived.SendMessage("Revive", SendMessageOptions.DontRequireReceiver);

            isAttacking = false;
        }

        private void Revive() {
            Debug.Log($"{gameObject.ToString()} revived");
            currentHp = hp / 6;
            isDeath = false;
        }

        protected void ModifyRecoil(float modifier) {
            baseAttackRecoil *= modifier;
            specialAttackRecoil *= modifier;
            Debug.Log(ToString() + " recoil Modified: " + baseAttackRecoil.ToString() + " " + specialAttackRecoil.ToString());
        }

        protected abstract void ModifyWeakness(float modifier);

        protected virtual void ModifyElementalPower(float modifier) {
            if (modifier > 1) {
                activateElementBonus = true;
            } else {
                activateElementBonus = false;
            }

            Debug.Log(ToString() + "activate element bonus");
        }

        protected virtual void UseStamina(float staminaUsed) {
            currentStamina -= staminaUsed;
            if(isMine)
                uIManager.FillBar(currentStamina / stamina, "stamina");
        }

        protected virtual void ResetStamina() {
            if (!staminaRecharging && currentStamina < stamina) {
                StartCoroutine(StaminaRecharge());
            }
        }

        protected IEnumerator StaminaRecharge() {
            staminaRecharging = true;
            currentStamina = currentStamina + staminaRecharged > stamina ? stamina : currentStamina + staminaRecharged;
            if(isMine)
                uIManager.FillBar(currentStamina / stamina, "stamina");
            yield return new WaitForSeconds(120f / speed);
            staminaRecharging = false;
        }

        protected override void RecoverHP(float hpRecovered) {
            base.RecoverHP(hpRecovered);
            if(isMine)
                uIManager.FillBar(currentHp / hp, "health");
        }

#region RPC

        [PunRPC]
        private void RPC_BasicAttack() {
            StartCoroutine(BaseAttackDamage());
        }

#endregion
    }
}