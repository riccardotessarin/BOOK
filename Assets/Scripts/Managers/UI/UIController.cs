using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
using System.Linq;
using Consumables.Healables.Plants;
using Characters.NPC;
using Consumables.Healables.Plants.Drops;
namespace Managers.UI{
    public class UIController 
    {
        public UIManager uIManager;
        public PlayableCharacter player;
        

        ///<summary>
        ///scroll from left to right the inventory UI
        ///</summary>
        public void ScrollUpInventory(){
            if(player.PowerMode){
                int count= player.ListBooks.Count();
                if(player.EquippedAttack==PlayableCharacter.Attack.BaseAttack){
                    if(count!=0){
                        player.EquippedAttack=PlayableCharacter.Attack.Book;
                        player.EquippedBook=player.ListBooks[count-1];
                        
                        if(count==1){
                            uIManager.ScrollUpMenu(player.SpecialAttackSprite);
                        }
                        else{
                            uIManager.ScrollUpMenu(player.ListBooks[count-2].BookIcon);
                        }
                        uIManager.ChangeChargeText(player.EquippedBook.CurrentCharges.ToString());
                        uIManager.ChangeDescriptionText(player.EquippedBook.Description);
                        

                    }
                    else{
                        player.EquippedAttack=PlayableCharacter.Attack.SpecialAttack;
                        if(uIManager)
                        uIManager.ScrollUpMenu(player.BaseAttackSprite);
                        uIManager.ChangeDescriptionText(player.SpecialAttackDescription);
                    }
                }
                else if(player.EquippedAttack==PlayableCharacter.Attack.SpecialAttack){
                    player.EquippedAttack=PlayableCharacter.Attack.BaseAttack;
                    if(count==0){
                        uIManager.ScrollUpMenu(player.SpecialAttackSprite);
                    }
                    else{
                        uIManager.ScrollUpMenu(player.ListBooks[count-1].BookIcon);
                    }
                    uIManager.ChangeDescriptionText(player.BaseAttackDescription);
                }
                else if (player.EquippedAttack==PlayableCharacter.Attack.Book){
                    int index=player.ListBooks.IndexOf(player.EquippedBook);
                    if (index==0){
                        player.EquippedAttack=PlayableCharacter.Attack.SpecialAttack;
                        player.EquippedBook=null;
                        uIManager.ScrollUpMenu(player.BaseAttackSprite);
                        uIManager.ChangeChargeText(PlayableCharacter.INFINITY);
                        uIManager.ChangeDescriptionText(player.SpecialAttackDescription);
                    }
                    else{
                        player.EquippedBook=player.ListBooks[index-1];
                        if(index==1){
                            uIManager.ScrollUpMenu(player.SpecialAttackSprite);
                        }
                        else{
                            uIManager.ScrollUpMenu(player.ListBooks[index-2].BookIcon);
                        }
                        uIManager.ChangeChargeText(player.EquippedBook.CurrentCharges.ToString());
                        uIManager.ChangeDescriptionText(player.EquippedBook.Description);
                    }
                }


            }
            else{
                int count=player.ListPlants.Count();
                if(count>1){
                    int index=player.ListPlants.IndexOf(player.EquippedPlant);
                    if(index==0){
                        player.EquippedPlant=player.ListPlants[count-1];
                        if(count==2){
                            uIManager.ScrollUpMenu(player.EquippedPlant.PlantIcon);
                            uIManager.ScrollUpMenu(player.ListPlants[0].PlantIcon);
                        }
                        else{
                            uIManager.ScrollUpMenu(player.ListPlants[count-2].PlantIcon);
                        }
                        uIManager.ChangeDescriptionText(player.EquippedPlant.Description);

                    }
                    else{
                        
                        player.EquippedPlant=player.ListPlants[index-1];
                        if(count==2){
                            uIManager.ScrollUpMenu(uIManager.VoidSPrite);
                        }
                        else if (index<2){
                            uIManager.ScrollUpMenu(player.ListPlants[count-1].PlantIcon);
                        }
                        else{
                            
                            uIManager.ScrollUpMenu(player.ListPlants[index-2].PlantIcon);
                        }
                        uIManager.ChangeDescriptionText(player.EquippedPlant.Description);
                    }
                }

            }
        }
        ///<summary>
        ///scroll from right to left the inventory UI
        ///</summary>
        public void ScrollDownInventory(){
            if(player.PowerMode){
                int count= player.ListBooks.Count();
                if(player.EquippedAttack==PlayableCharacter.Attack.BaseAttack){
                    player.EquippedAttack=PlayableCharacter.Attack.SpecialAttack;
                    if(count==0){
                        uIManager.ScrollDownMenu(player.BaseAttackSprite);
                    }
                    else{
                        uIManager.ScrollDownMenu(player.ListBooks[0].BookIcon);
                    }
                    uIManager.ChangeDescriptionText(player.SpecialAttackDescription);
                }
                else if (player.EquippedAttack==PlayableCharacter.Attack.SpecialAttack){
                    if(count!=0){
                        player.EquippedAttack=PlayableCharacter.Attack.Book;
                        player.EquippedBook=player.ListBooks[0];
                        if(count==1){
                            uIManager.ScrollDownMenu(player.BaseAttackSprite);
                        }
                        else{
                            uIManager.ScrollDownMenu(player.ListBooks[1].BookIcon);
                        }
                        uIManager.ChangeChargeText(player.EquippedBook.CurrentCharges.ToString());
                        uIManager.ChangeDescriptionText(player.EquippedBook.Description);
                    }
                    else{
                        player.EquippedAttack=PlayableCharacter.Attack.BaseAttack;
                        uIManager.ScrollDownMenu(player.SpecialAttackSprite);
                        uIManager.ChangeDescriptionText(player.BaseAttackDescription);
                    }
                }
                else if(player.EquippedAttack==PlayableCharacter.Attack.Book){
                    int index=player.ListBooks.IndexOf(player.EquippedBook);
                    if(index==count-1){
                        player.EquippedAttack=PlayableCharacter.Attack.BaseAttack;
                        player.EquippedBook=null;
                        uIManager.ScrollDownMenu(player.SpecialAttackSprite);
                        uIManager.ChangeChargeText(PlayableCharacter.INFINITY);
                        uIManager.ChangeDescriptionText(player.BaseAttackDescription);
                    }
                    else{
                        player.EquippedBook=player.ListBooks[index+1];
                        if( index==count-2){
                            uIManager.ScrollDownMenu(player.BaseAttackSprite);
                        }
                        else{
                            uIManager.ScrollDownMenu(player.ListBooks[index+2].BookIcon);
                        }
                        uIManager.ChangeChargeText(player.EquippedBook.CurrentCharges.ToString());
                        uIManager.ChangeDescriptionText(player.EquippedBook.Description);

                    }
                }
            }
            else{
                int count=player.ListPlants.Count();
                if(count>1){
                    int index=player.ListPlants.IndexOf(player.EquippedPlant);
                    if(index==count-1){
                        player.EquippedPlant=player.ListPlants[0];
                        if(count==2){
                            uIManager.ScrollDownMenu(player.EquippedPlant.PlantIcon);
                            
                        }
                        uIManager.ScrollDownMenu(player.ListPlants[1].PlantIcon);
                        uIManager.ChangeDescriptionText(player.EquippedPlant.Description);
                    }
                    else{
                        player.EquippedPlant=player.ListPlants[index+1];
                        if(count==2){
                            uIManager.ScrollDownMenu(uIManager.VoidSPrite);
                        }
                        else if(index==count-2){
                            uIManager.ScrollDownMenu(player.ListPlants[0].PlantIcon);
                        }
                        else{
                            uIManager.ScrollDownMenu(player.ListPlants[index+2].PlantIcon);
                        }
                        uIManager.ChangeDescriptionText(player.EquippedPlant.Description);
                    }
                }
            }
        }


        ///<summary>
        ///control if the charge fo the current book is zero 
        ///and in case set UI inventory and player attack
        ///index is index of the book in the inventory before it was used,
        ///count is number of books in the inventory before the equipped book was used
        ///</summary>
        public void CheckChargeBook(int index,int count){
            uIManager.ChangeChargeText(player.EquippedBook.CurrentCharges.ToString());
            if(player.EquippedBook.CurrentCharges==0){
                if(count==1){
                    player.EquippedAttack=PlayableCharacter.Attack.BaseAttack;
                    player.EquippedBook=null;
                    uIManager.DestroyCurrentObject(player.SpecialAttackSprite);
                    uIManager.ChangeChargeText(PlayableCharacter.INFINITY);
                    uIManager.ChangeDescriptionText(player.BaseAttackDescription);
                }
                else if(count==2){
                    if(index==0){
                        player.EquippedBook=player.ListBooks[0];
                        uIManager.DestroyCurrentObject(player.BaseAttackSprite);
                        uIManager.ChangeChargeText(player.EquippedBook.CurrentCharges.ToString());
                        uIManager.ChangeDescriptionText(player.EquippedBook.Description);
                    }
                    else{
                        player.EquippedAttack=PlayableCharacter.Attack.BaseAttack;
                        player.EquippedBook=null;
                        uIManager.DestroyCurrentObject(player.SpecialAttackSprite);
                        uIManager.ChangeChargeText(PlayableCharacter.INFINITY);
                        uIManager.ChangeDescriptionText(player.BaseAttackDescription);
                    }
                        
                }
                else if (count>2){
                    if(index==0){
                        player.EquippedBook=player.ListBooks[0];
                        uIManager.DestroyCurrentObject(player.ListBooks[1].BookIcon);
                        uIManager.ChangeChargeText(player.EquippedBook.CurrentCharges.ToString());
                        uIManager.ChangeDescriptionText(player.EquippedBook.Description);
                    }
                    else if(index==count-1){
                        player.EquippedAttack=PlayableCharacter.Attack.BaseAttack;
                        player.EquippedBook=null;
                        uIManager.DestroyCurrentObject(player.SpecialAttackSprite);
                        uIManager.ChangeChargeText(PlayableCharacter.INFINITY);
                        uIManager.ChangeDescriptionText(player.BaseAttackDescription);
                    }
                    else{
                        player.EquippedBook=player.ListBooks[index];
                        if(index==count-2){
                            uIManager.DestroyCurrentObject(player.BaseAttackSprite);
                        }
                        else{
                            uIManager.DestroyCurrentObject(player.ListBooks[index+2].BookIcon);
                        }
                        uIManager.ChangeChargeText(player.EquippedBook.CurrentCharges.ToString());
                        uIManager.ChangeDescriptionText(player.EquippedBook.Description);
                    }
                }
            }
        }


        ///<summary>
        ///set inventory UI after and player equipped object
        /// after the equipped plant is used;
        /// index is index of equipped plant before it was used
        /// count is number of plants in inventory before equipped plant was used
        ///</summary>
        public void CheckPlantInventory(int index,int count){
            if(count==1){
                player.EquippedPlant=null;
                uIManager.DestroyCurrentObject(uIManager.VoidSPrite);
                uIManager.ChangeDescriptionText("");
                player.PowerMode=!(player.PowerMode);
                uIManager.SwitchMode(player.PowerMode);
            }
            else if(count==2){
                player.EquippedPlant=player.ListPlants[0];
                if(index==0){
                    uIManager.DestroyCurrentObject(uIManager.VoidSPrite);
                }
                else if(index==1){
                    uIManager.DestroyCurrentObject(player.EquippedPlant.PlantIcon);
                    uIManager.ScrollDownMenu(uIManager.VoidSPrite);
                }
                uIManager.ChangeDescriptionText(player.EquippedPlant.Description);
            }
            else if(count==3){
                if (index==0 ||index==count-1){
                    player.EquippedPlant=player.ListPlants[0];
                    uIManager.ScrollUpMenu(player.EquippedPlant.PlantIcon);
                    uIManager.ScrollUpMenu(uIManager.VoidSPrite);
                }
                else{
                    player.EquippedPlant=player.ListPlants[index];
                    uIManager.DestroyCurrentObject(uIManager.VoidSPrite);
                }
                uIManager.ChangeDescriptionText(player.EquippedPlant.Description);
            }
            else if(count>3){
                if(index==0 || index==count-1){
                    player.EquippedPlant=player.ListPlants[0];
                    uIManager.DestroyCurrentObject(player.ListPlants[1].PlantIcon);
                }
                else{
                    player.EquippedPlant=player.ListPlants[index];
                    if(index==count-2){
                        uIManager.DestroyCurrentObject(player.ListPlants[0].PlantIcon);
                    }
                    else{
                        uIManager.DestroyCurrentObject(player.ListPlants[index+1].PlantIcon);
                    }
                }
                uIManager.ChangeDescriptionText(player.EquippedPlant.Description);
            }
        }

        ///<summary>
        ///change interaction text
        ///hit is the object hitted by raycast 
        ///traitor is a bool that represent if player is traitor
        ///</summary>
        public  void InteractionTextControl(RaycastHit hit,bool traitor){
            if(hit.collider.GetComponent<Character>()){
                Character hitted=hit.collider.GetComponent<Character>();
                if(hitted.IsDeath && hitted.Looted==false){
                    if(hitted is PlayableCharacter){
                        if(!traitor){
                            //Debug.Log("revive");
                            uIManager.ChangeInteractionText("Press 'E' to revive");
                        }
                        else{
                            //Debug.Log("revive or loot");
                            uIManager.ChangeInteractionText("Press 'E' to revive \nOr\n Press 'X' to loot");
                        }
                    }
                    else if(hitted is NonPlayableCharacters){
                        //Debug.Log("loot");
                        if(hitted.GetComponent<MeltingKinean>() && !hitted.GetComponent<MeltingKinean>().Drop)
                            return;
                        uIManager.ChangeInteractionText("Press 'X' to loot");
                    }
                }
            }
            else if(hit.collider.GetComponent<PlantDrop>()){
                Debug.Log("interact");
                uIManager.ChangeInteractionText("Press 'E' to interact");
            }
        }

        public void ResetInteractionText(){
            if(player.IsMine)
                uIManager.ChangeInteractionText("");
        }

        


        
    }
}