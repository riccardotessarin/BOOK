using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
using System.Linq;
namespace MalusEBonus{
    public class MalusManager : MonoBehaviour
    {
        //private List
        public enum Stats{
            Hp,
            Stamina,
            BasePower,
            Speed,
            Recoil,
            DamageReduction,
            Weakness,
            ElementalPower
            

        }

        public PlayableCharacter player;
        private List<Bonus> bonusList;
        private Dictionary<Stats,string> statDict = new Dictionary<Stats, string>();
        private Dictionary<Stats,Sprite> imageDict= new Dictionary<Stats, Sprite>();

        void Awake(){
            bonusList=new List<Bonus>();
            statDict[Stats.Hp]="ModifyHpMax";
            statDict[Stats.Stamina]="ModifyStaminaMax";
            statDict[Stats.BasePower]="ModifyBasePower";
            statDict[Stats.Speed]="ModifySpeed";
            statDict[Stats.Recoil]="ModifyRecoil";
            statDict[Stats.DamageReduction]="ModifyDamageReceived"; //only genee
            statDict[Stats.Weakness]="ModifyWeakness";
            statDict[Stats.ElementalPower]="ModifyElementalPower";
            imageDict[Stats.Hp]=Resources.Load<Sprite>("Images/healthSprite");
            imageDict[Stats.Stamina]=Resources.Load<Sprite>("Images/staminaSprite");
            imageDict[Stats.BasePower]=Resources.Load<Sprite>("Images/baseAttackSprite");
            imageDict[Stats.Speed]=Resources.Load<Sprite>("Images/speedSprite");
            imageDict[Stats.Recoil]=Resources.Load<Sprite>("Images/recoilSprite");
            imageDict[Stats.DamageReduction]=Resources.Load<Sprite>("Images/DamageSprite");
        }

        void Start(){
            imageDict[Stats.Weakness]=player.WeaknessSprite;
            imageDict[Stats.ElementalPower]=player.ElementSprite;
        }
        ///<summary>
        ///add a bonus to player
        ///</summary>
        public bool Add(Bonus bonus){
            if(bonusList.Contains(new Bonus(bonus.Stat,bonus.Name))){
                Debug.Log("bonus with the same name is already in the list");
                return false;
            }
            else{
                bonusList.Add(bonus);
                ActivateBonus(bonus);
                player.UIManager.AddBonusImage(imageDict[bonus.Stat],bonus.Positive,bonus.Name,bonus.Stat==Stats.Weakness);
                return true;
            }
        }
        ///<summary>
        ///remove a bonus from the player
        ///</summary>
        public bool Remove( Stats stat,string name){
            int index= bonusList.IndexOf(new Bonus(stat,name));
            if(index!=-1){
                DeActivateBonus(bonusList[index]);
                bonusList.RemoveAt(index);
                player.UIManager.RemoveBonusImage(name);
                return true;
            }
            else{
                Debug.Log("the searched bonus isn't in the list");
                return false;
            }
            
        }

        private void ActivateBonus(Bonus bonus){
            player.SendMessage(statDict[bonus.Stat],bonus.Modifier,SendMessageOptions.DontRequireReceiver);
        }
        private void DeActivateBonus(Bonus bonus){
            player.SendMessage(statDict[bonus.Stat],1/bonus.Modifier,SendMessageOptions.DontRequireReceiver);
        }

    }
}
