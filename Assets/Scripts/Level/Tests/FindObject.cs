using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Test{
    public class FindObject : Trial
    {
        [SerializeField] protected List<Transform> spawnPoints=new List<Transform>();
        [SerializeField] protected TrialObject prefab;
        [SerializeField] protected TrialObject crystal;
        protected override void Starter(){
            base.Starter();
            description="find the crystal";
            //Load prefab
            prefab=Resources.Load<GameObject>("Prefabs/LevelUtility/Crystal").GetComponent<TrialObject>();
        }
        public override void StartTrial(){
            start=true;
            crystal=Instantiate(prefab,spawnPoints[Random.Range(0,spawnPoints.Count)]);
        }
        protected override void EndTrial(){
            if(!crystal.Collected)
                return;
            completed=true;
        }
    }
}
