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
            Speed
        }

        public PlayableCharacter player;
        private List<Bonus> bonusList;
        private Dictionary<Stats,string> statDict = new Dictionary<Stats, string>();

        void Awake(){
            bonusList=new List<Bonus>();
            statDict[Stats.Hp]="ModifyHpMax";
            statDict[Stats.Stamina]="ModifyStaminaMax";
            statDict[Stats.BasePower]="ModifyBasePower";
            statDict[Stats.Speed]="ModifySpeed";
        }

        public bool Add(Bonus bonus){
            if(bonusList.Contains(new Bonus(bonus.Stat,bonus.Name))){
                Debug.Log("bonus with the same name is already in the list");
                return false;
            }
            else{
                bonusList.Add(bonus);
                ActivateBonus(bonus);
                return true;
            }
        }

        public bool Remove( Stats stat,string name){
            int index= bonusList.IndexOf(new Bonus(stat,name));
            if(index!=-1){
                DeActivateBonus(bonusList[index]);
                bonusList.RemoveAt(index);
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
