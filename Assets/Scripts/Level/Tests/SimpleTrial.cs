using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Test{
    public class SimpleTrial : Trial
    {
        protected override void Starter(){
            base.Starter();
            description="move to the next area after interacted with this";
        }
        // Start is called before the first frame update
        public override void StartTrial(){
            start=true;
        }
        protected override void EndTrial(){
            completed=true;
        }
        
    }
}
