using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForwardSimple : MonoBehaviour {

    public float speed = 0.2f;

	// Use this for initialization
	void Start () {
        speed = GameController.gameSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        speed = GameController.gameSpeed;
        if(!GameController.paused)
            transform.position += Vector3.back * speed;
	}
}
