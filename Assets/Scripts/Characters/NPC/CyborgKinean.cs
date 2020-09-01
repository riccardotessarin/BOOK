using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
using Attacks;
using Consumables.Books.Abilities;
using Consumables.Books;
using Consumables.Books.Drops;
using User;

namespace Characters.NPC {
    public class CyborgKinean : NonPlayableCharacters {
        [SerializeField] float specialAttackRadius;
        [SerializeField] float specialPower;
        [SerializeField] float specialDamageMultiplicator;
        [SerializeField] float maxAttackDistance;
        [SerializeField] EnumUtility.AttackType specialAttackAttribute;
        [SerializeField] Damage specialDamage;
        private bool rarityDrop;//True common / false rare



        protected override void Awaker() {
            base.Awaker();
            hp = 50;
            secondType = "cyborg";
            baseAttackRadius = 2.5f;
            detectionRadius = 10;
            specialAttackRadius = 7;
            basePower = 2;
            specialPower = 3;
            speed = 40;
            maxAttackDistance = 10;
            specialAttackAttribute=RandomAttackType();
            typeDrop=specialAttackAttribute;
            SetDropLoot();
        }

        protected override void Starter() {
            base.Starter();
            specialDamage = new Damage(specialPower, specialAttackAttribute);
        }

        protected override void Updater() {
            base.Updater();

            if (DetectionZone()) {
                if (BaseAttackZone())
                    BaseAttack();
                else if (SpecialAttackZone())
                    SpecialAttack();
            }
        }

        protected override void Death() {
            IsDeath = true;
        }


        protected override void OnDrawGizmosSelected() {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, specialAttackRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxAttackDistance);
        }
        ///<summary>
        ///return true if target is in range for a special attack
        ///and make this instance look at it,
        /// return false if this instance doesnt have a target or
        /// the target isn't in the area 
        ///</summary>
        bool SpecialAttackZone() {
            if (target) {
                Collider[] hitcolliders = Physics.OverlapSphere(transform.position, specialAttackRadius, PCLAYERMASK);
                foreach (var collider in hitcolliders) {
                    if (collider.gameObject == target.gameObject) {
                        transform.LookAt(target.transform);
                        return true;
                    }
                }

                return false;
            }

            return false;
        }

        void SpecialAttack() {
            if (!isAttacking) {
                StartCoroutine(SpecialAttackDamage());
            }
        }
        ///<summary>
        ///Coroutine that activate the special attack
        ///</summary>
        IEnumerator SpecialAttackDamage() {
            isAttacking = true;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, transform.position.y / 2, transform.forward, out hit, maxAttackDistance, PCLAYERMASK)) {
                Debug.Log("HIT");
                hit.transform.SendMessage("TakeDamage", specialDamage, SendMessageOptions.DontRequireReceiver);
            } else {
                Debug.Log("MISS");
            }

            yield return new WaitForSeconds(speed / 60f);
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            isAttacking = false;
        }

        private EnumUtility.AttackType RandomAttackType(){
            return (EnumUtility.AttackType)Random.Range(1,6);
        }
        protected override void SetDropLoot(){
            rarityDrop=Random.Range(0.1f,1.0f)<=0.7f;

        }
        ///<summary>
        ///method that return the drop
        ///</summary>
        public BookDrop GetDrop(){
            int i=rarityDrop ? 1:0;
            //TODO
            GameObject ret=new GameObject();
            ret.AddComponent(Inventory.BookDropDictionary[specialAttackAttribute][i]);
            ret.name=ret.GetComponent<BookDrop>().Name;
            return ret.GetComponent<BookDrop>();
        }
    }
}