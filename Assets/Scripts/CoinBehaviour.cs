using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : ObstacleBehaviour {

    private float timer = 0f;
    public float rotation = 0f;
    public float deltaH = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        //print(transform.rotation);
        transform.Rotate(new Vector3(0, 0, rotation));
        float h = transform.position.y + (Mathf.Sin(timer) * deltaH);
        transform.position = new Vector3(transform.position.x, h, transform.position.z);
	}
}
