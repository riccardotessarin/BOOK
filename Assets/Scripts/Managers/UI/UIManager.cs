using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Characters.Interfaces;
using Consumables.Books;
using Consumables.Healables.Plants.Drops;
using Consumables.Books.Drops;
using System.Linq;
using Photon.Pun;

namespace Managers.UI{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [SerializeField] private Image healthBar;
        [SerializeField] private Image staminaBar;
        [SerializeField] private PlayableCharacter player;
        public PlayableCharacter Player{get=>player; set{if(!player){player=value;}}}
        [SerializeField] private Image equippedPrimaryObjImage;
        [SerializeField] private Image equippedSecondaryObjImage;
        [SerializeField] private Image inGameObjectMenu;
        [SerializeField] private Sprite voidSprite;
        public Sprite VoidSPrite{get=>voidSprite;}
        [SerializeField]private Image centerObject;
        [SerializeField]private Image rightObject;
        [SerializeField]private Image leftObject;
        [SerializeField]private bool attackOrPlant; //true power mode, false otherwise
        [SerializeField]private Text chargeTextMenu;
        [SerializeField]private Text chargeTextGame;
        [SerializeField]private string chargeString;
        [SerializeField]private Text descriptionText;
        [SerializeField]private Text interactionText;
        private string descriptionAttack;
        private string descriptionPlant;
        
        private Sprite[] plantArray=new Sprite[3];
        private Sprite[] attackArray= new Sprite[3];
        private string interactionString;
        [SerializeField] private Image statusArea;
        [SerializeField] private Image bonusPrefab;
        [SerializeField] private Image malusPrefab;
        private Dictionary<string,Image> statusImageDict=new Dictionary<string, Image>();

        [SerializeField] private Transform canvas;
        
        
        
        
        ///<summary>
        ///set health and stamina UI bar
        ///</summary>
        public void FillBar(float value, string type) {
            if(healthBar && staminaBar){
                switch (type) {
                    case "health":
                        healthBar.fillAmount = value;
                        break;
                    case "stamina":
                        staminaBar.fillAmount = value;
                        break;
                    default:
                        break;
                }
            }
        }

       

        void Awake(){
            if (Instance == null || ReferenceEquals(this, Instance)) {
                Instance = this;
            } else {
                Destroy(this);
            }

            gameObject.tag="UIManager";
            voidSprite=Resources.Load<Sprite>("Images/voidSprite");
            bonusPrefab=Resources.Load<GameObject>("Prefabs/UI/Bonus").GetComponent<Image>();
            malusPrefab=Resources.Load<GameObject>("Prefabs/UI/Malus").GetComponent<Image>();
        
            
            

        }

        

        

        // Start is called before the first frame update
        void Start()
        {
            /*var players = GameObject.FindGameObjectsWithTag("Player");
            Debug.Log(players.Count());
            player = players.FirstOrDefault(p => p.GetComponent<PhotonView>().IsMine).GetComponent<PlayableCharacter>();*/
            canvas=GameObject.FindGameObjectWithTag("Canvas").transform;
            healthBar=canvas.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
            staminaBar=canvas.GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>();
            equippedPrimaryObjImage=canvas.GetChild(1).GetChild(0).GetComponent<Image>();
            equippedSecondaryObjImage=canvas.GetChild(3).GetChild(0).GetComponent<Image>();
            inGameObjectMenu=canvas.GetChild(2).GetComponent<Image>();
            interactionText=canvas.GetChild(4).GetComponent<Text>();
            statusArea=canvas.GetChild(5).GetComponent<Image>();
            centerObject=inGameObjectMenu.transform.GetChild(0).GetComponent<Image>();
            leftObject=inGameObjectMenu.transform.GetChild(1).GetComponent<Image>();
            rightObject=inGameObjectMenu.transform.GetChild(2).GetComponent<Image>();
            descriptionText=inGameObjectMenu.transform.GetChild(3).GetChild(0).GetComponent<Text>();
            chargeTextGame=equippedPrimaryObjImage.transform.GetChild(0).GetComponent<Text>();
            chargeTextMenu=centerObject.transform.GetChild(0).GetComponent<Text>();
            
            equippedSecondaryObjImage.sprite=voidSprite;
            for(int i=0;i<3;i++){
                plantArray[i]=voidSprite;
            }
            attackOrPlant=true;
            interactionString="";
            equippedPrimaryObjImage.sprite=player.BaseAttackSprite;
            chargeString="X"+PlayableCharacter.INFINITY;
            chargeTextGame.text=chargeString;
            centerObject.sprite=player.BaseAttackSprite;
            descriptionAttack=player.BaseAttackDescription;
            descriptionText.text=descriptionAttack;
            chargeTextMenu.text=chargeString;
            attackArray[1]=player.BaseAttackSprite;
            rightObject.sprite=player.SpecialAttackSprite;
            attackArray[2]= player.SpecialAttackSprite;
            int count=player.ListBooks.Count;
            if(count==0){
                leftObject.sprite=player.SpecialAttackSprite;
                attackArray[0]=player.SpecialAttackSprite;
            }
            else{
                leftObject.sprite=player.ListBooks[count-1].BookIcon;
                attackArray[0]=player.ListBooks[count-1].BookIcon;
            }

            count=player.ListPlants.Count;
            if(count!=0){
                equippedSecondaryObjImage.sprite=player.ListPlants[0].PlantIcon;
                descriptionPlant=player.ListPlants[0].Description;
                plantArray[1]=player.ListPlants[0].PlantIcon;
                if(count>1){
                    plantArray[2]=player.ListPlants[1].PlantIcon;
                }
                if(count>2){
                    plantArray[0]=player.ListPlants[count-1].PlantIcon;
                }
            }
            Debug.Log("start");
        }

        // Update is called once per frame
        void Update()
        {
            
            equippedPrimaryObjImage.sprite=centerObject.sprite;
            chargeTextGame.text=chargeTextMenu.text;
            attackOrPlant=player.PowerMode;
            equippedPrimaryObjImage.sprite=centerObject.sprite;
            if(attackOrPlant){
                equippedSecondaryObjImage.sprite=plantArray[1];
            }
            else{
                equippedSecondaryObjImage.sprite=attackArray[1];
            }
            interactionText.text=interactionString;
            SetStatusBar();


        }

        ///<summary>
        ///if bool true activate inventory menu
        ///else deactivate it
        ///</summary>
        public void ActivateMenu(bool on){
            inGameObjectMenu.gameObject.SetActive(on);
        }
        public void ScrollDownMenu(Sprite rightSprite){
            leftObject.sprite=centerObject.sprite;
            centerObject.sprite=rightObject.sprite;
            rightObject.sprite=rightSprite;
            //equippedAttackImage.texture=centerObject.texture;
            
        }
        public void ScrollUpMenu(Sprite leftSprite){
            rightObject.sprite=centerObject.sprite;
            centerObject.sprite=leftObject.sprite;
            leftObject.sprite=leftSprite;
            //equippedAttackImage.texture=centerObject.texture;
        }
        ///<summary>
        ///if power is true change from plant menu to attack menu
        ///otherwise change from attack menu to plant menu
        ///</summary>
        public void SwitchMode(bool power){
            if (power){ //entra in power mode
                plantArray[1]=centerObject.sprite;
                plantArray[2]=rightObject.sprite;
                plantArray[0]=leftObject.sprite;
                descriptionPlant=descriptionText.text;
                centerObject.sprite=attackArray[1];
                chargeTextMenu.text=chargeString;
                rightObject.sprite=attackArray[2];
                leftObject.sprite=attackArray[0];
                descriptionText.text=descriptionAttack;
                
            }
            else{
                attackArray[1]=centerObject.sprite;
                attackArray[2]=rightObject.sprite;
                attackArray[0]=leftObject.sprite;
                descriptionAttack=descriptionText.text;
                chargeTextMenu.text="";
                centerObject.sprite=plantArray[1];
                rightObject.sprite=plantArray[2];
                leftObject.sprite=plantArray[0];
                descriptionText.text=descriptionPlant;
            }

        }

        public void ChangeChargeText(string text){
            chargeString="X"+text;
            chargeTextMenu.text=chargeString;
        }

        public void ChangeDescriptionText(string text){
            descriptionText.text=text;
        }

        public void DestroyCurrentObject(Sprite rightSprite){
            centerObject.sprite=rightObject.sprite;
            rightObject.sprite=rightSprite;
        }

        public void ChangeInteractionText(string text){
            interactionString=text;
        }
        ///<summary>
        ///add a book in UI inventory
        ///</summary>
        public void AddBook(Sprite book){
            if(player.EquippedAttack==PlayableCharacter.Attack.BaseAttack){
                if(player.PowerMode){
                    leftObject.sprite=book;
                }
                attackArray[0]=book;
            }
            else if(player.EquippedAttack==PlayableCharacter.Attack.SpecialAttack){
                if(player.ListBooks.Count==1){
                    if(player.PowerMode){
                        rightObject.sprite=book;
                    }
                    attackArray[2]=book;
                }
            }
            else if(player.EquippedAttack==PlayableCharacter.Attack.Book){
                int count=player.ListBooks.Count;
                int index=player.ListBooks.IndexOf(player.EquippedBook);
                if(count==2){
                    if(player.PowerMode){
                        rightObject.sprite=book;
                    }
                    attackArray[2]=book;
                }
                else if(count==3){
                    if(index==1){
                        if(player.PowerMode){
                            rightObject.sprite=book;
                        }
                        attackArray[2]=book;
                    }
                }
            }
        }
        ///<summary>
        ///add a plant in UI inventory
        ///</summary>
       public void AddPlant(Sprite plant, string description){
           int count=player.ListPlants.Count;
           if(count==1){
               if(!player.PowerMode){
                   centerObject.sprite=plant;
                   ChangeDescriptionText(description);
               }
               plantArray[1]=plant;
               descriptionPlant=description;
           }
           
            else if(count==2){
                if(!player.PowerMode){
                    rightObject.sprite=plant;
                }
                plantArray[2]=plant;
            }
            else if(count>2){
                int index=player.ListPlants.IndexOf(player.EquippedPlant);
                if(index==0){
                    if(!player.PowerMode){
                        leftObject.sprite=plant;
                    }
                    plantArray[0]=plant;
                }
                else if(index==count-2){
                    if(!player.PowerMode){
                        rightObject.sprite=plant;
                    }
                    plantArray[2]=plant;
                }
            }
        } 
        ///<summary>
        ///add an image in status zone
        ///sprite is the sprite of the image,
        ///bonus sets the background of the image,
        ///name identificates the image
        ///block sets the block bar over the image
        ///</summary>
        public void AddBonusImage(Sprite sprite, bool bonus,string name, bool block){
            Image image;
            if(bonus){
                image=Instantiate(bonusPrefab,new Vector3(190,0,0),new Quaternion(0,0,0,0));
            }
            else{
                image=Instantiate(malusPrefab,new Vector3(190,0,0),new Quaternion(0,0,0,0));
            }
            image.rectTransform.SetParent(statusArea.transform,false);
            
            image.rectTransform.GetChild(0).GetComponent<Image>().sprite=sprite;
            if(block){
                image.rectTransform.GetChild(0).GetChild(0).gameObject.SetActive(true);
            }
            statusImageDict[name]=image;
        }

        ///<summary>
        ///remove an image from status area
        ///name is the name of object to remove
        ///</summary>
        public void RemoveBonusImage(string name){
            Image image=statusImageDict[name];
            if(!statusImageDict.Remove(name)){
                Debug.Log("Error in removing image");
            }
            Destroy(image.gameObject);
        }

        private void SetStatusBar(){
            int i=0;
            foreach(var key in statusImageDict.Keys){
                statusImageDict[key].rectTransform.localPosition=new Vector3(190-49*i,0,0);
                i++;
            }
        }
    }
}
