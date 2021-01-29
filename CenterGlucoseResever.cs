using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterGlucoseResever : MonoBehaviour
{
   public Transform target;
   public float force = 2.5f;
   private Rigidbody2D rb;

   private void Start(){
   	rb = GetComponent<Rigidbody2D>();
   } 
   private void FixedUpdate(){
   	if(target == null)
   	rb.AddForce((- transform.position) / Vector2.Distance(transform.position,Vector2.zero) * force);
   	else 
   	rb.AddForce((target.position - transform.position) / Vector2.Distance(transform.position,target.position) * (force + 1));

   }
}
