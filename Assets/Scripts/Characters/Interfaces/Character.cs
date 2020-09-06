using System.Collections;
using UnityEngine;
using Attacks;


namespace Characters.Interfaces {
    public abstract class Character : MonoBehaviour {
        [SerializeField] protected float hp;
        [SerializeField] protected float hpMax;
        [SerializeField] protected float currentHp;
        public string type;

        [SerializeField] protected float speed;
        [SerializeField] protected float currentSpeed;
        [SerializeField] protected float basePower;
        [SerializeField] protected float currentBasePower;
        [SerializeField] protected bool isDeath;

        public bool IsDeath {
            get => isDeath;
            protected set => isDeath = value;
        }

        [SerializeField] protected bool isAttacking;
        [SerializeField] bool poisoned;
        [SerializeField] protected bool looted; //false if has a dropLoot / true if it hasn't or it has been taken

        public bool Looted {
            get => looted;
            set => looted = value;
        }

        public const int PCLAYERMASK = 1 << 8; //layer 8
        public const int NPCLAYERMASK = 1 << 9; // layer 9 

        public bool Poisoned {
            get => poisoned;
            set => poisoned = value;
        }


        public struct Damage {
            public Damage(float damage, EnumUtility.AttackType attackType) {
                DamageRec = damage;
                AttackType = attackType;
            }

            public float DamageRec { get; }
            public EnumUtility.AttackType AttackType { get; }
        }

        ///<summary>
        ///Method used to deal damage to the character instance,
        ///it is used by both PC and NPC, it is protected so use with
        ///SendMessage method like this:
        ///hitted.SendMessage("TakeDamage",baseDamage,SendMessageOptions.DontRequireReceiver);
        ///</summary>
        protected abstract void TakeDamage(Damage damage);
        /*Method used when the character hp reach 0,
        (not implemented yet), activate the event linked to the death of the character instance*/

        protected abstract void Death();

        ///<summary>
        ///Method that activate baseAttack Coroutine, 
        ///PCs use this woth a particular input,
        ///NPCs use this when an enemy enter in their damagezone
        ///</summary>
        protected abstract void BaseAttack();

        public override string ToString() {
            return type;
        }

        ///<summary>
        ///Method used for modify speed of character instance,
        ///protected so same as TakeDamage
        ///</summary>
        protected virtual void ModifySpeed(float modifier) {
            currentSpeed *= modifier;
            Debug.Log(ToString() + " speed modified: " + currentSpeed);
        }

        ///<summary>
        ///modify hp max
        ///</summary> 
        protected virtual void ModifyHpMax(float modifier) {
            hp *= modifier;
            if (currentHp > hp)
                currentHp = hp;
            Debug.Log(ToString() + " max hp modified: " + hp);
        }

        protected virtual void ModifyBasePower(float modifier) {
            currentBasePower *= modifier;
            Debug.Log(ToString() + " basePower modified: " + currentBasePower);
        }

        /*Coroutine used for calculating in time damage caused by poisoned status
        */
        protected virtual IEnumerator PoisonDamage() {
            Poisoned = false;

            gameObject.GetComponent<Renderer>().material.color = Color.magenta;
            TakeDamage(new Damage(3, EnumUtility.AttackType.Basilisk));

            yield return new WaitForSecondsRealtime(3);

            Poisoned = true;
        }

        protected virtual void RecoverHP(float hpRecovered) {
            if (currentHp + hpRecovered > hpMax)
                currentHp = hpMax;
            else
                currentHp += hpRecovered;
            Debug.Log("hp recovered. Current hp: " + currentHp);
        }
    }
}