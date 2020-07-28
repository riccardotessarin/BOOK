using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Characters.Interfaces{
    public class NPC : Character
    {
        [SerializeField] protected int baseAttackRange{get;private set;}

        // Start is called before the first frame update
        void Start()
        {
            IsDeath=false;
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        protected override void Death(){
            IsDeath=true;
        }
        protected override int BaseAttack(){return 0;}
    }
}
