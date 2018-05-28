using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inflate : MonoBehaviour, IPooledObject {

    public float factor = 0.01f;
    public float destination = 1f;
    private bool reached = false;

	// Use this for initialization
	public void onObjectSpawn() {
        transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.localScale.x < destination && !reached)
            transform.localScale += Vector3.one * factor;
        else
            reached = true;
	}

    private void OnEnable()
    {
        reached = false;
        transform.localScale = Vector3.zero;
    }
}
