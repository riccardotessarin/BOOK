using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
using Attacks;
using Consumables.Books.Abilities;
using Consumables.Books;
using Consumables.Books.Drops;
using Photon.Pun;
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
        [SerializeField] private Animator animator;
        




        protected override void Awaker() {
            base.Awaker();
            hp = 30;
            secondType = "cyborg";
            baseAttackRadius = 2.5f;
            detectionRadius = 10;
            specialAttackRadius = 7;
            basePower = 2;
            specialPower = 3;
            speed = 100;
            maxAttackDistance = 10;
            specialAttackAttribute=RandomAttackType();
            typeDrop=specialAttackAttribute;
            SetDropLoot();
            animator=GetComponent<Animator>();
            
        }

        protected override void Starter() {
            base.Starter();
            specialDamage = new Damage(specialPower, specialAttackAttribute);
            animator.SetFloat("Speed",currentSpeed/speed);
        }

        protected override void Updater() {
            base.Updater();

            if (DetectionZone()) {
                animator.SetBool("HasTarget",true);
                if (BaseAttackZone()){
                    animator.SetBool("InRange",true);
                    BaseAttack();
                }
                else if (SpecialAttackZone()){
                    animator.SetBool("InRange",true);
                    SpecialAttack();
                }
                else{
                    animator.SetBool("InRange",false);
                }
            }
            else{
                animator.SetBool("HasTarget",false);
            }
        }
        protected override IEnumerator BaseAttackDamage(){
            isAttacking = true;
            animator.SetBool("IsAttacking",isAttacking);
            
            yield return new WaitForSeconds(60f/currentSpeed);
            target.SendMessage("TakeDamage", baseDamage, SendMessageOptions.DontRequireReceiver);
            isAttacking = false;
            animator.SetBool("IsAttacking",isAttacking);
        }

        protected override void Death() {
            base.Death();
            animator.SetTrigger("IsDead");
            
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
            RaycastHit hit;
            animator.SetBool("IsAttacking",isAttacking);
            yield return new WaitForSeconds(120f/currentSpeed);
            if (Physics.SphereCast(transform.position, transform.position.y / 2, transform.forward, out hit, maxAttackDistance, PCLAYERMASK)) {
                Debug.Log("HIT");
                hit.transform.SendMessage("TakeDamage", specialDamage, SendMessageOptions.DontRequireReceiver);
            } else {
                Debug.Log("MISS");
            }
            isAttacking = false;
            animator.SetBool("IsAttacking",isAttacking);
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

        protected override IEnumerator TakingDamage(Damage damage){
            isAttacking=true;
           
            Debug.Log("cyborg damage taken");
            
            
            Debug.Log("Low Damage");
            //animator.Play("anim_warrior_qr_1_Get_Hit_1",-1,0);
            //animator.SetBool("BigHit",false);
            //animator.SetBool("TakingDamage",takingDamage);
            animator.SetTrigger("Hitted");
            yield return new WaitForSeconds(0.1f);
            
            if (damage.DamageRec < currentHp) {
                Debug.Log("Calculating Damage");
                currentHp -= damage.DamageRec;
                Debug.Log(gameObject.ToString() + " took damage");
            } else{
                Debug.Log("Death");
                Death();

            }
            
            isAttacking=false;
        }

        protected override void ModifySpeed(float modifier){
            base.ModifySpeed(modifier);
            var value=currentSpeed/speed;
            animator.SetFloat("Speed",value);
        }
        
#region RPC

        [PunRPC]
        private void RPC_NPCTakeDamage(float damageAmount, short attackType) {
            var attackTypeEnum = (EnumUtility.AttackType) attackType;
            StartCoroutine(TakingDamage(new Damage(damageAmount, attackTypeEnum)));
        }

#endregion
    }
}