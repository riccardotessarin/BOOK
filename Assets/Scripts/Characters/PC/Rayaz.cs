using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
using Attacks;
using MalusEBonus;
using Photon.Pun;

namespace Characters.PC {
    public class Rayaz : PlayableCharacter {
        [SerializeField] float specialAttackRadius;
        [SerializeField] Character targetEnemy;
        [SerializeField] float fogDamage;
        [SerializeField] float fogDuration;
        [SerializeField] PoisonFog prefabFog;
        

        // Start is called before the first frame update
        protected override void Awaker() {
            base.Awaker();
            type = "rayaz";
            raceType = EnumUtility.CharacterType.Rayaz;
            weaknessSprite = Resources.Load<Sprite>("Images/WaterPowerSprite");
            hp = 50;
            stamina = 50;
            specialAttackRadius = 10;
            speed = 120;
            baseAttackRange = 10;
            targetEnemy = null;
            fogDamage = 2;
            fogDuration = 10;
            elementType = EnumUtility.AttackType.Basilisk;
            elementSprite = Resources.Load<Sprite>("Images/PoisonPowerSprite");
            anim=transform.GetChild(1).GetComponent<Animation>();
        }

        protected override void Starter() {
            base.Starter();
            baseAttackDescription = $"poisonous bullet(Recoil: {baseAttackRecoil * 100 / hp}%)";
            prefabFog = Resources.Load<GameObject>("Prefabs/Attacks/Fog").GetComponent<PoisonFog>();
            anim["punch"].layer=123;
            anim["hpunch"].layer=123;
        }

        /*protected override void BaseAttack(){
            
        }*/
        protected override void Updater() {
            base.Updater();
            if (targetEnemy && targetEnemy.IsDeath) {
                StartCoroutine(PFog());
                targetEnemy = null;
            }
        }

        protected override void FixedUpdater()
        {
            base.FixedUpdater();
            while(controller.IsWalking){
                while(controller.speedToTransmit==controller.RunSpeed){
                    UseStamina(2);
                    anim.Play("run");
                }
                anim.Play("walk");
            }
            if(controller.isJumping){
                anim.Stop();
                anim.Play("jump");
            }
        }

        protected override void SpecialAttack() {
            if (!isAttacking && !targetEnemy) {
                if (currentHp <= specialAttackRecoil) {
                    Debug.Log("cannot do special attack, life too low");
                } else {
                    StartCoroutine(SpecialEffect());
                    UseStamina(staminaConsumed);
                }
            }
        }

        protected override void RyuyukiBond() {
            Bonus bonus = new Bonus(true, MalusManager.Stats.ElementalPower, 1.2f, "ryuyukiBonus");
            Bonus malus = new Bonus(false, MalusManager.Stats.Speed, 0.7f, "ryuyukiMalus");
            malusManager.Add(bonus);
            malusManager.Add(malus);
            Debug.Log(this.ToString() + "ryuyuki bond");
        }

        protected override void GeneeBond() {
            Bonus bonus = new Bonus(true, MalusManager.Stats.Stamina, 1.3f, "geneeBonus");
            Bonus malus = new Bonus(false, MalusManager.Stats.ElementalPower, 0.7f, "geneeMalus");
            malusManager.Add(bonus);
            malusManager.Add(malus);
            Debug.Log(this.ToString() + "genee bond");
        }

        protected override void RayazBond() {
            Bonus malus = new Bonus(false, MalusManager.Stats.Weakness, 0.7f, "rayazMalus");
            malusManager.Add(malus);
            Debug.Log(this.ToString() + "rayazbond");
        }

        protected override void ReverseRyuyukiBond() {
            malusManager.Remove(MalusManager.Stats.ElementalPower, "ryuyukiBonus");
            malusManager.Remove(MalusManager.Stats.Speed, "ryuyukiMalus");
            Debug.Log(this.ToString() + ": reverse ryuyuki bond");
        }

        protected override void ReverseGeneeBond() {
            malusManager.Remove(MalusManager.Stats.Stamina, "geneeBonus");
            malusManager.Remove(MalusManager.Stats.ElementalPower, "geneeMalus");
            Debug.Log(this.ToString() + ": reverse genee bond");
        }

        protected override void ReverseRayazBond() {
            malusManager.Remove(MalusManager.Stats.Weakness, "rayazMalus");
            Debug.Log(this.ToString() + ": reverse rayaz bond");
        }

        protected override void TakeDamage(Damage damage) {
            if(!isDeath){
                anim.Play("hit");
                float dam = damage.AttackType == weakness ? damage.DamageRec * weaknessMultiplicator : damage.DamageRec;
                if (dam < currentHp) {
                    currentHp -= dam;
                } else {
                    currentHp = 0;

                    Death();
                }

                if (isMine)
                    uIManager.FillBar(currentHp / hp, "health");
            }
        }

        public override IEnumerator BaseAttackDamage(Vector3 position, Vector3 direction) {
            isAttacking = true;
            baseDamage = new Damage(currentBasePower, EnumUtility.AttackType.Basilisk);
            currentHp -= baseAttackRecoil;
            if (isMine)
                uIManager.FillBar(currentHp / hp, "health");
            RaycastHit hit;
            
            if (Physics.Raycast(position, direction, out hit, baseAttackRange)) {
                Character hitted = hit.collider.GetComponent<Character>();
                if (hitted) {
                    hitted.SendMessage("TakeDamage", baseDamage, SendMessageOptions.DontRequireReceiver);
                }
            }
            anim.Play("punch");
            yield return new WaitForSeconds(60f/currentSpeed);
            isAttacking = false;
        }

        protected override IEnumerator SpecialEffect() {
            //aggiungere effetto attira mob
            isAttacking = true;
            currentHp -= specialAttackRecoil;
            uIManager.FillBar(currentHp / hp, "health");
            RaycastHit hit;
            
            if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, baseAttackRange)) {
                Character hitted = hit.collider.GetComponent<Character>();
                if (hitted) {
                    targetEnemy = hitted;
                    hitted.Poisoned = true;
                }
            }
            anim.Play("hpunch");
            yield return new WaitForSeconds(120f/currentSpeed);
            isAttacking = false;
        }

        ///<summary>
        ///coroutine that is activated when the enemy hitted by specialAttack die
        ///creates an object PoisonFog and after a certain time destroys it 
        ///</summary>
        protected IEnumerator PFog() {
            //instanziare prefab poisonfog e poi distruggerlo
            var fog = PhotonNetwork.Instantiate("Prefabs/Attacks/Fog", targetEnemy.transform.position, targetEnemy.transform.rotation).GetComponent<PoisonFog>();
            fog.Caster = this;
            fog.Damage = new Damage(fogDamage, EnumUtility.AttackType.Basilisk);
            yield return new WaitForSeconds(fogDuration);
            Destroy(fog.gameObject);
        }

        protected override void ModifyWeakness(float modifier) {
            if (modifier < 1) {
                weakness = EnumUtility.AttackType.Neptunian;
            } else {
                weakness = EnumUtility.AttackType.Nothing;
            }

            Debug.Log(ToString() + "weakness modified");
        }


        protected override void Death(){
            base.Death();
            //StartCoroutine(DeathAnimation());
            anim.Play("death");
        }

        protected override void ModifySpeed(float modifier){
            base.ModifySpeed(modifier);
            var value=currentSpeed/speed;
            anim["idle"].speed=value;
            anim["punch"].speed=value;
            anim["run"].speed=value;
            anim["walk"].speed=value;
            anim["hpunch"].speed=value;
        }

        

        protected override void Revive(){
            base.Revive();
            anim.Stop();
            anim.Play("idle");
        }
    }
}