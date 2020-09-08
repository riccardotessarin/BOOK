using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
using Attacks;
using MalusEBonus;

namespace Characters.PC {
    public class Ryuyuki : PlayableCharacter {
        [SerializeField] protected Character lastTarget;
        [SerializeField] protected float specialAttackRadius;
        [SerializeField] protected float speedModifier;
        [SerializeField] protected float maxlastTargetDistance;
        [SerializeField] protected GameObject iceFogPrefab;
        


        // Start is called before the first frame update
        protected override void Awaker() {
            base.Awaker();
            type = "ryuyuki";
            raceType = EnumUtility.CharacterType.Ryuyuki;
            weaknessSprite = Resources.Load<Sprite>("Images/FirePowerSprite");
            hp = 50;
            //Debug.Log("Awaker Ryuyuki");
            stamina = 50;
            specialAttackRadius = 10;
            speed = 120;
            baseAttackRange = 10;
            speedModifier = 0.5f;
            maxlastTargetDistance = 20;
            elementType = EnumUtility.AttackType.Niflheim;
            elementSprite = Resources.Load<Sprite>("Images/IcePowerSprite");
            anim=transform.GetChild(1).GetComponent<Animation>();
            iceFogPrefab=Resources.Load<GameObject>("Prefabs/Attacks/IceFog");
        }

        protected override void Starter() {
            base.Starter();
            //Debug.Log("Starter Ryuyuki");
            baseAttackDescription = $"ice scale(Recoil: {baseAttackRecoil * 100 / hp}%)";
            specialAttackDescription = $"the last scale,that hitted a target, explodes and freeze all characters in a certain area, slowing their speed (Recoil: {specialAttackRecoil * 100 / hp}%; Last target: {lastTarget != null})";
            anim["punch"].layer=123;
            anim["hpunch"].layer=123;
        }

        protected override void Updater() {
            base.Updater();
            specialAttackDescription = $"the last scale,that hitted a target, explodes and freeze all characters in a certain area, slowing their speed (Recoil: {specialAttackRecoil * 100 / hp}%; Last target: {lastTarget != null})";
            if (equippedAttack == Attack.SpecialAttack && PowerMode && isMine)
                UIManager.ChangeDescriptionText(specialAttackDescription);

            if (lastTarget)
                DistanceCheckLastTarget();
        }

        protected override void FixedUpdater()
        {
            if(!isDeath){
                base.FixedUpdater();
                if(controller.IsWalking){
                    Debug.Log("moving");
                    if(controller.speedToTransmit==controller.WalkingSpeed){
                        Debug.Log("walking");
                        anim.Play("walk");
                    }
                    else{
                        anim.Play("idle");
                    }
                }
                else if(!controller.IsWalking){
                    if(controller.speedToTransmit==controller.RunSpeed){
                        Debug.Log("running");
                        UseStamina(2);
                        anim.Play("run");
                    }
                }
                
                else if(controller.isJumping){
                    anim.Play("jump");
                }
                
            }
        }
        protected override void SpecialAttack() {
            if (!isAttacking && lastTarget) {
                if (currentHp <= specialAttackRecoil) {
                    Debug.Log("cannot do special attack, life too low");
                } else {
                    StartCoroutine(SpecialEffect());
                    UseStamina(staminaConsumed);
                    lastTarget = null;
                }
            }
        }

        protected override void RyuyukiBond() {
            Bonus malus = new Bonus(false, MalusManager.Stats.Weakness, 0.7f, "ryuyukiMalus");
            malusManager.Add(malus);
            Debug.Log(this.ToString() + ": ryuyuki bond");
        }

        protected override void GeneeBond() {
            Bonus bonus = new Bonus(true, MalusManager.Stats.Recoil, 0.7f, "geneeBonus");
            Bonus malus = new Bonus(false, MalusManager.Stats.Stamina, 0.7f, "geneeMalus");
            malusManager.Add(bonus);
            malusManager.Add(malus);
            Debug.Log(this.ToString() + "genee bond");
        }

        protected override void RayazBond() {
            Bonus bonus = new Bonus(true, MalusManager.Stats.BasePower, 1.3f, "rayazBonus");
            Bonus malus = new Bonus(false, MalusManager.Stats.Hp, 0.7f, "rayazMalus");
            malusManager.Add(bonus);
            malusManager.Add(malus);
            Debug.Log(this.ToString() + "rayaz bond");
        }

        protected override void ReverseRyuyukiBond() {
            malusManager.Remove(MalusManager.Stats.Weakness, "ryuyukiMalus");
            Debug.Log(this.ToString() + ": reverse ryuyuki bond");
        }

        protected override void ReverseGeneeBond() {
            malusManager.Remove(MalusManager.Stats.Recoil, "geneeBonus");
            malusManager.Remove(MalusManager.Stats.Stamina, "geneeMalus");
            Debug.Log(this.ToString() + ": reverse genee bond");
        }

        protected override void ReverseRayazBond() {
            malusManager.Remove(MalusManager.Stats.BasePower, "rayazBonus");
            malusManager.Remove(MalusManager.Stats.Hp, "rayazMalus");
            Debug.Log(this.ToString() + ": reverse rayaz bond");
        }

        protected override void TakeDamage(Damage damage) {
            if(!isDeath){
                Debug.Log("taking damage1");
                anim.Play("hit");
                float dam = damage.AttackType == weakness ? damage.DamageRec * weaknessMultiplicator : damage.DamageRec;
                if (dam < currentHp) {
                    Debug.Log("taking damage2");
                    currentHp -= dam;
                } else {
                    currentHp = 0;

                    Death();
                }

                if (isMine){
                    uIManager.FillBar(currentHp / hp, "health");
                    UIManager.TakeDamage();
                }
            }
        }

        public override IEnumerator BaseAttackDamage(Vector3 position, Vector3 direction) {
            isAttacking = true;
            baseDamage = new Damage(currentBasePower, EnumUtility.AttackType.Niflheim);
            currentHp -= baseAttackRecoil;
            if (isMine)
                uIManager.FillBar(currentHp / hp, "health");
            RaycastHit hit;
            
            if (Physics.Raycast(position, direction, out hit, baseAttackRange)) {
                Character hitted = hit.collider.GetComponent<Character>();
                if (hitted) {
                    lastTarget = hitted;
                    hitted.SendMessage("TakeDamage", baseDamage, SendMessageOptions.DontRequireReceiver);
                }
            }
            anim.Play("punch");
            yield return new WaitForSeconds(60f/currentSpeed);
            isAttacking = false;
        }

        protected override IEnumerator SpecialEffect() {
            isAttacking = true;
            currentHp -= specialAttackRecoil;
            uIManager.FillBar(currentHp / hp, "health");
            anim.Play("hpunch");
            Collider[] hitcolliders = Physics.OverlapSphere(lastTarget.transform.position, specialAttackRadius);
            foreach (var collider in hitcolliders) {
                Character character = collider.GetComponent<Character>();
                if (character && character.gameObject != this.gameObject) {
                    character.SendMessage("ModifySpeed", speedModifier, SendMessageOptions.DontRequireReceiver);
                }
            }
            GameObject iceFog=Instantiate(iceFogPrefab,lastTarget.transform.position,lastTarget.transform.rotation);

            yield return new WaitForSeconds(120f/currentSpeed);
            lastTarget=null;
            Destroy(iceFog);
            isAttacking = false;
            yield return new WaitForSeconds(10);
            foreach (var collider in hitcolliders) {
                Character character = collider.GetComponent<Character>();
                if (character && character.gameObject != this.gameObject)
                    character.SendMessage("ModifySpeed", 1 / speedModifier, SendMessageOptions.DontRequireReceiver);
            }
        }

        ///<summary>
        ///check if lastTarget(the last target hitted, used for activating special effect )
        ///is in range, otherwise lastTarget becomes null
        ///</summary>
        protected void DistanceCheckLastTarget() {
            float distance = Vector3.Distance(transform.position, lastTarget.transform.position);
            if (distance > maxlastTargetDistance)
                lastTarget = null;
        }

        protected override void OnDrawGizmosSelected() {
            base.OnDrawGizmosSelected();

            Gizmos.color = Color.cyan;
            if (lastTarget)
                Gizmos.DrawWireSphere(lastTarget.transform.position, specialAttackRadius);
        }

        protected override void ModifyWeakness(float modifier) {
            if (modifier < 1) {
                weakness = EnumUtility.AttackType.Inferno;
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