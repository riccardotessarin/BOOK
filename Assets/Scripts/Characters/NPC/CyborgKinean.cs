using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;

namespace Characters.NPC {
    public class CyborgKinean : NonPlayableCharacters {
        [SerializeField] float specialAttackRadius;
        [SerializeField] float specialPower;
        [SerializeField] float specialDamageMultiplicator;
        [SerializeField] float maxAttackDistance;

        [SerializeField]
        float SpecialDamage {
            get => specialPower * specialDamageMultiplicator;
        }

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
            baseDamageMultiplicator = 2;
            maxAttackDistance = 10;
            specialDamageMultiplicator = 1;
        }

        protected override void Starter() {
            base.Starter();
        }

        protected override void Updater() {
            if (DetectionZone()){
                if (BaseAttackZone())
                    BaseAttack();
                else if (SpecialAttackZone())
                    SpecialAttack();
            }
        }

        protected override void Death() {
            isDeath = true;
        }


        protected override void OnDrawGizmosSelected() {
            base.OnDrawGizmosSelected();
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, specialAttackRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxAttackDistance);
        }

        bool SpecialAttackZone() {
            if (target){
                Collider[] hitcolliders = Physics.OverlapSphere(transform.position, specialAttackRadius, PCLAYERMASK);
                foreach (var collider in hitcolliders){
                    if (collider.gameObject == target.gameObject){
                        transform.LookAt(target.transform);
                        return true;
                    }
                }

                return false;
            }

            return false;
        }

        void SpecialAttack() {
            if (!isAttacking){
                StartCoroutine(SpecialAttackDamage());
            }
        }

        IEnumerator SpecialAttackDamage() {
            isAttacking = true;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, transform.position.y / 2, transform.forward, out hit, maxAttackDistance, PCLAYERMASK)){
                Debug.Log("HIT");
                hit.transform.SendMessage("TakeDamage", SpecialDamage, SendMessageOptions.DontRequireReceiver);
            }
            else{
                Debug.Log("MISS");
            }

            yield return new WaitForSeconds(speed / 60f);
            gameObject.GetComponent<Renderer>().material.color = Color.white;
            isAttacking = false;
        }
    }
}