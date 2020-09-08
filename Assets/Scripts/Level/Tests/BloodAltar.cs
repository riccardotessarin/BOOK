using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Test {
    public class BloodAltar : Trial {
        [SerializeField] protected float lifeToPay = 30;
        [SerializeField] protected float lifePaid = 0;

        protected override void Starter() {
            base.Starter();
            description = "pay life to pass. Paid life: " + lifePaid.ToString();
        }

        protected override void Updater() {
            base.Updater();
            description = "pay life to pass. Paid life: " + lifePaid.ToString();
        }

        public override void StartTrial() {
            start = true;
        }

        protected override void EndTrial() {
            if (lifePaid < lifeToPay)
                return;
            completed = true;
        }

        public void AddLife(float life) {
            lifePaid += life;
        }
    }
}