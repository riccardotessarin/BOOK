using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attacks;

namespace Characters.Interfaces {
    public abstract class NonPlayableCharacters : Character {
        [SerializeField] protected float baseAttackRadius;

        public string secondType;

        [SerializeField] protected PlayableCharacter target;

        //[SerializeField] protected bool hasTarget;
        [SerializeField] protected float detectionRadius;


        protected Damage baseDamage;
        [SerializeField] protected EnumUtility.AttackType typeDrop; 


        protected virtual void Awaker() {
            gameObject.layer = 9; //NPC layer
            type = "kinean";
            IsDeath = false;
            target = null;
            isAttacking = false;
            looted=false;
        }

        protected virtual void Starter() {
            currentHp = hp;
            currentSpeed = speed;
            currentBasePower=basePower;
            baseDamage = new Damage(currentBasePower, EnumUtility.AttackType.Neutral);
        }

        void Awake() {
            Awaker();
        }

        // Start is called before the first frame update
        void Start() {
            Starter();
        }

        protected virtual void Updater() { }

        void FixedUpdate() {
            if (Poisoned) {
                StartCoroutine(PoisonDamage());
            }
        }

        // Update is called once per frame
        void Update() {
            if (!IsDeath) {
                Updater();
            } else {
                gameObject.GetComponent<Renderer>().material.color = Color.black;
            }
        }

        protected override void Death() {
            IsDeath = true;
        }

        protected override void BaseAttack() {
            if (!isAttacking)
                StartCoroutine(BaseAttackDamage());
        }

        //method that return true if in range for receiving a base Attack,
        //false otherwise
        protected virtual bool BaseAttackZone() {
            if (target) {
                Collider[] hitcolliders = Physics.OverlapSphere(transform.position, baseAttackRadius, PCLAYERMASK);
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

        //method that return true if at least a PC is in the zone,
        //and set that as a target and makes this NPC instance look at it,
        // Otherwise return false, if there aren't PCs or if it has already a target
        protected virtual bool DetectionZone() {
            Collider[] hitcolliders = Physics.OverlapSphere(transform.position, detectionRadius, PCLAYERMASK);
            if (hitcolliders.Length != 0) {
                if (!target) {
                    target = hitcolliders[0].GetComponent<PlayableCharacter>();
                    transform.LookAt(target.transform);
                    return true;
                } else {
                    foreach (var collider in hitcolliders) {
                        if (collider.gameObject == target.gameObject) {
                            transform.LookAt(target.transform);
                            return true;
                        }
                    }

                    target = null;
                    return false;
                }
            } else
                target = null;

            return false;
        }

        protected override void TakeDamage(Damage damage) {
            if (damage.DamageRec < currentHp) {
                currentHp -= damage.DamageRec;
                Debug.Log(gameObject.ToString() + " took damage");
            } else
                Death();
        }

        protected virtual IEnumerator BaseAttackDamage() {
            isAttacking = true;
            Color baseColor = gameObject.GetComponent<Renderer>().material.color;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
            target.SendMessage("TakeDamage", baseDamage, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(speed / 60f);
            gameObject.GetComponent<Renderer>().material.color = baseColor;
            isAttacking = false;
        }

        protected abstract void SetDropLoot();

        protected virtual void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, baseAttackRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
        
    }
}