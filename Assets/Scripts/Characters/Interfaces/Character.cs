using UnityEngine;

namespace Characters.Interfaces {
    public abstract class Character : MonoBehaviour {
        [SerializeField] protected float hp;
        [SerializeField] protected float currentHp;
        public string type;

        [SerializeField] protected float speed;
        [SerializeField] protected int basePower;
        [SerializeField] protected bool isDeath;
        [SerializeField] protected bool isAttacking;


        protected abstract void TakeDamage(float damage);
        protected abstract void Death();
        protected abstract void BaseAttack();

        public override string ToString() {
            return type;
        }
    }
}