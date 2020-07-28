using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Interfaces;
namespace Characters.PC{
    public class Ryuyuki : PlayableCharacter
    {

        
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }
        protected override int BaseAttack(){
            return 1;
        }
        protected override void SpecialAttack(){}
        protected override void RyuyukiBond(){}
        protected override void GeneeBond(){}
        protected override void RayazBond(){}
        
    }
}
