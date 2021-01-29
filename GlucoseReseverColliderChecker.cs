using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlucoseReseverColliderChecker : MonoBehaviour
{
	public GameObject targetResever;
    private void OnTriggerEnter2D(Collider2D other){
    	if(other.tag == "Center Glucose Resever"){
    		Destroy(other.gameObject,.01f);
    		targetResever.SetActive(true);
    	}
    }
}
