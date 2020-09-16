using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
using Attacks;
using MalusEBonus;

namespace Characters.PC {
    public class Genee : PlayableCharacter {
        [SerializeField] bool invicible;
        [SerializeField] float damageReceivedMultiplicator = 1;
        


        // Start is called before the first frame update
        protected override void Awaker() {
            base.Awaker();
            type = "genee";
            raceType = EnumUtility.CharacterType.Genee;
            hp = 80;
            stamina = 50;
            weaknessSprite = Resources.Load<Sprite>("Images/ElectricPowerAttack");
            speed = 100;
            baseAttackRange = 10;
            invicible = false;
            elementType = EnumUtility.AttackType.Neptunian;
            elementSprite = Resources.Load<Sprite>("Images/WaterPowerSprite");
            animator=transform.GetChild(1).GetComponent<Animator>();
        }

        protected override void Starter() {
            base.Starter();
            baseAttackDescription = $"water projectiles(Recoil: {baseAttackRecoil * 100 / hp}%)";
            specialAttackDescription = $"become invicible for a limited time(Recoil: {specialAttackRecoil * 100 / hp}%)";
            animator.SetFloat("Speed",currentSpeed/speed);
        }
        protected override void FixedUpdater()
        {
            base.FixedUpdater();
            animator.SetBool("IsWalking",controller.moveDir!=new Vector2(0,0));
            if(!controller.IsWalking){
                if(controller.speedToTransmit==controller.RunSpeed){
                    UseStamina(0.5f);
                    animator.SetBool("isRunning",true);
                }
                else{
                    animator.SetBool("isRunning",false);
                }
            }
            if(controller.IsJumping){
                animator.SetBool("isJumping",true);
            }
            else{
                animator.SetBool("isJumping",false);
            }
        }
        /*protected override void BaseAttack(){
            
        }*/
        protected override void SpecialAttack() {
            if (!isAttacking) {
                if (currentHp <= specialAttackRecoil) {
                    Debug.Log("cannot do special attack, life too low");
                } else {
                    StartCoroutine(SpecialEffect());
                }
            }
        }

        protected override void RyuyukiBond() {
            Bonus bonus = new Bonus(true, MalusManager.Stats.DamageReduction, 0.7f, "ryuyukiBonus");
            Bonus malus = new Bonus(false, MalusManager.Stats.Speed, 0.7f, "ryuyukiMalus");
            malusManager.Add(bonus);
            malusManager.Add(malus);
            Debug.Log(this.ToString() + "ryuyuki bond");
        }

        protected override void GeneeBond() {
            /*Bonus malus = new Bonus(false, MalusManager.Stats.Weakness, 0.7f, "geneeMalus");

            malusManager.Add(malus);
            Debug.Log(this.ToString() + "genee bond");*/
        }

        protected override void RayazBond() {
            Bonus bonus = new Bonus(true, MalusManager.Stats.Speed, 1.3f, "rayazBonus");
            Bonus malus = new Bonus(false, MalusManager.Stats.Hp, 0.7f, "rayazMalus");
            malusManager.Add(bonus);
            malusManager.Add(malus);
            Debug.Log(this.ToString() + "rayazbond");
        }

        protected override void ReverseRyuyukiBond() {
            malusManager.Remove(MalusManager.Stats.DamageReduction, "ryuyukiBonus");
            malusManager.Remove(MalusManager.Stats.Speed, "ryuyukiMalus");
            Debug.Log(this.ToString() + ": reverse ryuyuki bond");
        }

        protected override void ReverseGeneeBond() {
            /*malusManager.Remove(MalusManager.Stats.Weakness, "geneeMalus");
            Debug.Log(this.ToString() + ": reverse genee bond");*/
        }

        protected override void ReverseRayazBond() {
            malusManager.Remove(MalusManager.Stats.Speed, "rayazBonus");
            malusManager.Remove(MalusManager.Stats.Hp, "rayazMalus");
            Debug.Log(this.ToString() + ": reverse rayaz bond");
        }

        protected override void TakeDamage(Damage damage){
            if(!invicible && !isDeath){
                animator.SetTrigger("hitted");
                float dam= (damage.AttackType==weakness ? damage.DamageRec*weaknessMultiplicator:damage.DamageRec)*damageReceivedMultiplicator;
                if (dam< currentHp){
                    currentHp-=dam;
                    
                }
                else{
                    currentHp=0;
                    
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
            baseDamage = new Damage(currentBasePower, EnumUtility.AttackType.Neptunian);
            currentHp -= baseAttackRecoil;
            if(isMine)
                uIManager.FillBar(currentHp / hp, "health");
            RaycastHit hit;
            if (Physics.Raycast(position, direction, out hit, baseAttackRange)) {
                Character hitted = hit.collider.GetComponent<Character>();
                if (hitted) {
                    hitted.SendMessage("TakeDamage", baseDamage, SendMessageOptions.DontRequireReceiver);
                }
            }
            animator.SetBool("isAttacking",isAttacking);
            yield return new WaitForSeconds(60f/currentSpeed);
            isAttacking = false;
            animator.SetBool("isAttacking",isAttacking);
        }

        ///<summary>
        ///Coroutine that activate the state of invicible and
        ///after a certain time remove this state
        ///</summary>
        protected override IEnumerator SpecialEffect(){
            isAttacking=true;
            currentHp-=specialAttackRecoil;
            if(isMine)
                uIManager.FillBar(currentHp/hp,"health");
            invicible=true;
            animator.SetBool("specialAttack",isAttacking);
            yield return new WaitForSeconds(120f/currentSpeed);
            isAttacking=false;
            animator.SetBool("specialAttack",isAttacking);
            yield return new WaitForSeconds(15);
            invicible = false;
        }

        protected void ModifyDamageReceived(float modifier) {
            damageReceivedMultiplicator *= modifier;
            Debug.Log(ToString() + " damage received modified:" + damageReceivedMultiplicator);
        }

        protected override void ModifyWeakness(float modifier) {
            if (modifier < 1) {
                weakness = EnumUtility.AttackType.Raijin;
            } else {
                weakness = EnumUtility.AttackType.Nothing;
            }

            Debug.Log(ToString() + "weakness modified");
        }

        protected override void Death(){
            base.Death();
            //StartCoroutine(DeathAnimation());
            animator.SetTrigger("isDeath");
        }

        protected override void ModifySpeed(float modifier){
            base.ModifySpeed(modifier);
            var value=currentSpeed/speed;
            animator.SetFloat("Speed",value);
        }

        

        public override void Revive(){
            base.Revive();
            animator.SetTrigger("Revived");
        }
    }
}