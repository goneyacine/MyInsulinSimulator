using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public float force = 1.5f;
    private Rigidbody2D rb;

    void Start(){
    	rb = gameObject.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate(){
    rb.AddForce((target.position - transform.position) / Vector2.Distance(transform.position,target.position) * force);
    }
}
