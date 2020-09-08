using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.NPC;
namespace Test{
    public class BloodBath : Trial
    {
        [SerializeField]protected Transform[] spawnPoints=new Transform[3];
        [SerializeField]protected CyborgKinean prefab;
        [SerializeField]protected CyborgKinean[] enemies=new CyborgKinean[3];
        protected override void Starter(){
            base.Starter();
            description="Destroy 3 Cyborg_Kineans that will spawn in this area";
            prefab=Resources.Load<CyborgKinean>("Prefabs/NPC/Cyborg_Kinean").GetComponent<CyborgKinean>();
        }
        
        public override void StartTrial(){
            Debug.Log("Trial Started");
            start=true;
            for(int i=0;i<spawnPoints.Length;i++){
                enemies[i]=Instantiate(prefab,spawnPoints[i]);
            }
        }
        protected override void EndTrial(){
            for(int i=0;i<enemies.Length;i++){
                if(!enemies[i].IsDeath)
                    return;
            }
            completed=true;
            Debug.Log("Trial Completed");
        }
        
    }
}
