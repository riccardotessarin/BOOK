﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Characters.Interfaces;
using Consumables.Books;
using Consumables.Healables.Plants;
using System.Linq;
namespace Managers.UI{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Image healthBar;
        [SerializeField] private Image staminaBar;
        [SerializeField] private PlayableCharacter player;
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
        private Vector3 statusVector;
        
        


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
            gameObject.tag="UIManager";
            player=GameObject.FindWithTag("Player").GetComponent<PlayableCharacter>();
            centerObject=inGameObjectMenu.transform.GetChild(0).GetComponent<Image>();
            leftObject=inGameObjectMenu.transform.GetChild(1).GetComponent<Image>();
            rightObject=inGameObjectMenu.transform.GetChild(2).GetComponent<Image>();
            descriptionText=inGameObjectMenu.transform.GetChild(3).GetChild(0).GetComponent<Text>();
            chargeTextGame=equippedPrimaryObjImage.transform.GetChild(0).GetComponent<Text>();
            chargeTextMenu=centerObject.transform.GetChild(0).GetComponent<Text>();
            voidSprite=Resources.Load<Sprite>("Images/voidSprite");
            bonusPrefab=Resources.Load<GameObject>("Prefabs/UI/Bonus").GetComponent<Image>();
            malusPrefab=Resources.Load<GameObject>("Prefabs/UI/Malus").GetComponent<Image>();
            statusVector= new Vector3(bonusPrefab.rectTransform.position.x,bonusPrefab.rectTransform.position.y,bonusPrefab.rectTransform.rect.width);
            equippedSecondaryObjImage.sprite=voidSprite;
            for(int i=0;i<3;i++){
                plantArray[i]=voidSprite;
            }
            attackOrPlant=true;
            interactionString="";
            

        }

        

        

        // Start is called before the first frame update
        void Start()
        {
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

        public void AddBook(Book book){
            if(player.EquippedAttack==PlayableCharacter.Attack.BaseAttack){
                if(player.PowerMode){
                    leftObject.sprite=book.BookIcon;
                }
                attackArray[0]=book.BookIcon;
            }
            else if(player.EquippedAttack==PlayableCharacter.Attack.SpecialAttack){
                if(player.ListBooks.Count==1){
                    if(player.PowerMode){
                        rightObject.sprite=book.BookIcon;
                    }
                    attackArray[2]=book.BookIcon;
                }
            }
            else if(player.EquippedAttack==PlayableCharacter.Attack.Book){
                int count=player.ListBooks.Count;
                int index=player.ListBooks.IndexOf(player.EquippedBook);
                if(count==2){
                    if(player.PowerMode){
                        rightObject.sprite=book.BookIcon;
                    }
                    attackArray[2]=book.BookIcon;
                }
                else if(count==3){
                    if(index==1){
                        if(player.PowerMode){
                            rightObject.sprite=book.BookIcon;
                        }
                        attackArray[2]=book.BookIcon;
                    }
                }
            }
        }

       public void AddPlant(Plant plant){
           int count=player.ListPlants.Count;
           if(count==1){
               if(!player.PowerMode){
                   centerObject.sprite=plant.PlantIcon;
                   ChangeDescriptionText(plant.Description);
               }
               plantArray[1]=plant.PlantIcon;
               descriptionPlant=plant.Description;
           }
           
            else if(count==2){
                if(!player.PowerMode){
                    rightObject.sprite=plant.PlantIcon;
                }
                plantArray[2]=plant.PlantIcon;
            }
            else if(count>2){
                int index=player.ListPlants.IndexOf(player.EquippedPlant);
                if(index==0){
                    if(!player.PowerMode){
                        leftObject.sprite=plant.PlantIcon;
                    }
                    plantArray[0]=plant.PlantIcon;
                }
                else if(index==count-2){
                    if(!player.PowerMode){
                        rightObject.sprite=plant.PlantIcon;
                    }
                    plantArray[2]=plant.PlantIcon;
                }
            }
        } 
        
        public void AddBonusImage(Sprite sprite, bool bonus,string name){
            Image image;
            if(bonus){
                image=Instantiate(bonusPrefab,bonusPrefab.rectTransform);
            }
            else{
                image=Instantiate(malusPrefab,malusPrefab.rectTransform);
            }
            image.rectTransform.SetParent(statusArea.rectTransform);
            image.rectTransform.GetChild(0).GetComponent<Image>().sprite=sprite;
            statusImageDict[name]=image;
        }
        public void RemoveBonusImage(string name){
            Image image=statusImageDict[name];
            if(!statusImageDict.Remove(name)){
                Debug.Log("Error in removing image");
            }
            Destroy(image.gameObject);
        }

        private void SetStatusBar(){
            float pos=statusVector.x;
            foreach(var key in statusImageDict.Keys){
                statusImageDict[key].rectTransform.position=new Vector3(pos,statusVector.y,0);
                pos=pos-statusVector.z;
            }
        }
    }
}
