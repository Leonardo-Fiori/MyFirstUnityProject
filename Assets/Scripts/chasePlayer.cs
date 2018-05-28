using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chasePlayer : MonoBehaviour {

    public GameObject player;
    public float movementSpeed;
    private Rigidbody rb;

    private float offsetXChasing = 0f;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        offsetXChasing = Random.Range(-3f, 3f);
	}
	
	// Update is called once per frame
	void Update () {
        if (GameController.paused) return;

        Vector3 playerPos = new Vector3(player.transform.position.x + offsetXChasing, player.transform.position.y, player.transform.position.z);
        Vector3 direction = player.transform.position - transform.position;
        if(direction.magnitude > 1.5f)
        {
            direction.Normalize();
            //print(direction * movementSpeed);
            rb.AddForce(direction * movementSpeed, ForceMode.VelocityChange);
            transform.LookAt(player.transform);
        }
	}
}