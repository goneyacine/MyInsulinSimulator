using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlucoseResever : MonoBehaviour
{
    private Transform spawnPoint;
    public float spawnRate = 3f;
    public GameObject glucosePrefab;
    public GameObject centerGlucoseReseverPrefab;
    public float lastSpawningTime;
    private Cell_InsulineManager insulineManager;
    public Transform centerGlucoseReseverSpawnPoint;
    private void Update(){
      if(Time.time - lastSpawningTime >= spawnRate){
   	   Spawn();
   	   lastSpawningTime = Time.time;
      }
    }
    private void Spawn(){
    insulineManager = FindObjectOfType<Cell_InsulineManager>();
    spawnPoint = transform.GetChild(0).transform;
    GameObject spawnedGlucoseObject =  Instantiate(glucosePrefab,spawnPoint.position,Quaternion.identity);
   	spawnedGlucoseObject.GetComponent<FollowObject>().target = transform;
    }
    private void OnDisable(){
     GameObject spawnedCenterGlucoseResever = Instantiate(centerGlucoseReseverPrefab,centerGlucoseReseverSpawnPoint.position,Quaternion.identity);
     insulineManager.aliveCenterResevers.Add(spawnedCenterGlucoseResever.GetComponent<CenterGlucoseResever>());   	
    }
      private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Glucose Object")
        {
        	OnGlucoseEnter();
            Destroy(collider.gameObject,.1f);
        }
    }
    private void OnGlucoseEnter(){
    	insulineManager.glucoseValueInCell += 2f;
    }
}
