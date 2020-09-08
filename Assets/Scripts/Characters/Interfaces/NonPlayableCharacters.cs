using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Attacks;
using Photon.Pun;

namespace Characters.Interfaces {
    public abstract class NonPlayableCharacters : Character {
        [SerializeField] protected float baseAttackRadius;

        public string secondType;

        [SerializeField] protected PlayableCharacter target;

        //[SerializeField] protected bool hasTarget;
        [SerializeField] protected float detectionRadius;


        protected Damage baseDamage;
        [SerializeField] protected EnumUtility.AttackType typeDrop;

        private PhotonView _photonView;


        protected virtual void Awaker() {
            _photonView = GetComponent<PhotonView>();

            gameObject.layer = 9; //NPC layer
            type = "kinean";
            IsDeath = false;
            target = null;
            isAttacking = false;
            looted = false;
        }

        protected virtual void Starter() {
            hpMax = hp;
            currentHp = hp;
            currentSpeed = speed;
            currentBasePower = basePower;
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
            if (Poisoned && !isDeath) {
                StartCoroutine(PoisonDamage());
            }
        }

        // Update is called once per frame
        void Update() {
            if (!IsDeath) {
                Updater();
            }
        }

        protected override void Death() {
            IsDeath = true;
            StartCoroutine(DeathVanishing());
            GetComponent<CapsuleCollider>().direction = 2;
            GetComponent<CapsuleCollider>().radius = 1.5f;
        }

        protected IEnumerator DeathVanishing() {
            yield return new WaitForSecondsRealtime(120f);
            Destroy(this.gameObject);
        }

        protected override void BaseAttack() {
            if (!isAttacking)
                StartCoroutine(BaseAttackDamage());
        }

        ///<summary>
        ///method that return true if in range for receiving a base Attack,
        ///false otherwise
        ///</summary>
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

        ///<summary>
        ///method that return true if at least a PC is in the zone,
        ///and set that as a target and makes this NPC instance look at it,
        ///Otherwise return false, if there aren't PCs or if it has already a target
        ///</summary>
        protected virtual bool DetectionZone() {
            Collider[] hitcolliders = Physics.OverlapSphere(transform.position, detectionRadius, PCLAYERMASK);
            if (hitcolliders.Length != 0) {
                if (!target) {
                    for (int i = 0; i < hitcolliders.Length; i++) {
                        target = hitcolliders[i].GetComponent<PlayableCharacter>();
                        if (!target.IsDeath) {
                            transform.LookAt(target.transform);
                            return true;
                        }
                    }

                    target = null;
                    return false;
                } else {
                    foreach (var collider in hitcolliders) {
                        if (collider.gameObject == target.gameObject && !target.IsDeath) {
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
            if (!IsDeath) {
                _photonView.RPC("RPC_NPCTakeDamage", RpcTarget.OthersBuffered, damage.DamageRec, damage.AttackType);
            }
        }

        protected virtual IEnumerator TakingDamage(Damage damage) {
            isAttacking = true;
            yield return new WaitForSeconds(0.3f);
            if (damage.DamageRec < currentHp) {
                currentHp -= damage.DamageRec;
                Debug.Log(gameObject.ToString() + " took damage");
            } else
                Death();

            isAttacking = false;
        }

        protected virtual IEnumerator BaseAttackDamage() {
            isAttacking = true;

            target.SendMessage("TakeDamage", baseDamage, SendMessageOptions.DontRequireReceiver);
            yield return new WaitForSeconds(60f / currentSpeed);

            isAttacking = false;
        }

        protected abstract void SetDropLoot();

        protected virtual void OnDrawGizmosSelected() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, baseAttackRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
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