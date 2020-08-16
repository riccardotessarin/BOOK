using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;

namespace Characters.NPC {
    public class MeltingKinean : NonPlayableCharacters {
        // Start is called before the first frame update
        protected override void Awaker() {
            base.Awaker();
            hp = 20;
            secondType = "melting";
            baseAttackRadius = 2.5f;
            detectionRadius = 10;
            basePower = 2;
            speed = 30;
            baseDamageMultiplicator = 2;
        }

        protected override void Starter() {
            base.Starter();
        }

        protected override void Updater() {
            if (DetectionZone()){
                //Debug.Log(secondType+" "+type+"detecting "+target.type);
                transform.LookAt(target.transform);
                if (BaseAttackZone())
                    BaseAttack();
            }
        }


        // Update is called once per frame
    }
}