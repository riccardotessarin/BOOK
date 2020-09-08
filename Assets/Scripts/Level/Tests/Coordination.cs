using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Test{
    public class Coordination : Trial
    {
        [SerializeField]protected Transform[] spawnPoints=new Transform[4];
        [SerializeField]protected TrialButton[] buttons=new TrialButton[2];
        [SerializeField]protected TrialButton prefab;
        

        // Start is called before the first frame update
        protected override void Starter(){
            description="press buttons spawned in the area";
            base.Starter();
            prefab=Resources.Load<GameObject>("Prefabs/LevelUtility/Button").GetComponent<TrialButton>();
            
            
        }

        // Update is called once per frame
        
        public override void StartTrial(){
            start=true;
            int val=Random.Range(0,4);
            int oldestVal=val;
            buttons[0]=Instantiate(prefab,spawnPoints[val]);
            while(val==oldestVal)
                val=Random.Range(0,4);
            buttons[1]=Instantiate(prefab,spawnPoints[val]);
            
            
        }
        protected override void EndTrial(){
            foreach(var button in buttons){
                if(!button.pressed)
                    return;
            }
            completed=true;
            Debug.Log("Trial Completed");
        }
        
        
    }
}
