using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPushObject : MonoBehaviour {

    Rigidbody rb;
    public float force;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(0, 0, force));
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
