using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsulineResever : MonoBehaviour
{
  [Range(1f,100f)]
   public float SpawnRate = 5f;
   public bool canSpawn = false;
   public GameObject InsulinePrefab;
   public Transform spawnPoint;
   private float lastUpdatingTime;
   private Cell_InsulineManager insulineManager;
   private void Start(){
   Spawn();
   lastUpdatingTime = Time.time;
   insulineManager = FindObjectOfType<Cell_InsulineManager>();
   }
   private void Update(){
   	if(Time.time - lastUpdatingTime >= SpawnRate){
   		Spawn();
   		lastUpdatingTime = Time.time;
      }
   }

   public void Spawn(){
   	if(canSpawn){
   	GameObject spawnedInsulineObject =  Instantiate(InsulinePrefab,spawnPoint.position,Quaternion.identity);
   	spawnedInsulineObject.GetComponent<FollowObject>().target = transform;
   	}
   }
     private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.tag == "Insuline Object")
        {
        	OnInsulineEnter();
            Destroy(collider.gameObject,2f);
        }
    }
    private void OnInsulineEnter(){
    	insulineManager.insulineValueInCell += .5f;
    }
}
