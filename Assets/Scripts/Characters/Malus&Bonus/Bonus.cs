using System;

namespace MalusEBonus{
    public class Bonus:  IEquatable<Bonus>
    {
        public bool Positive{get;} //if true is a bonus, else a malus
        public MalusManager.Stats Stat{get;}
        public  float Modifier{get;}
        public string Name{get;}
        public Bonus(bool positive,MalusManager.Stats stat, float modifier,string name){
            Positive=positive;
            Stat=stat;
            Modifier=modifier;
            Name=name;
        }
        public Bonus(MalusManager.Stats stats, string name){
            Stat=stats;
            Name=name;
        }

        public bool Equals(Bonus other){
            if(this.Name==other.Name && this.Stat==other.Stat)
                return true;
            else
                return false;
        }


    }
}
