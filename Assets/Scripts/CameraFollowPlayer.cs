using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {

    public GameObject player;
    public bool deathcam = false;
    public float freecamSpeed = 0.1f;
    //public GameObject cameraContainer;
    //public Rigidbody cameraContainerRb;

    private float tollerance = 16f;

	// Use this for initialization
	void Start () {
        //cameraContainerRb = cameraContainer.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        float offset = transform.position.x - player.transform.position.x;
    
        float movement;

        if (deathcam)
        {
            transform.Rotate(0, freecamSpeed, 0);
            movement = offset / (tollerance/2);
        }
        else
        {
            movement = offset / tollerance;
        }

        if (Mathf.Abs(movement) <= 0.025f) movement = 0f;

        transform.position = new Vector3(transform.position.x - movement, transform.position.y, transform.position.z);
    }
}
