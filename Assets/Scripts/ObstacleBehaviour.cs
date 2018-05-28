using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : MonoBehaviour {

    // ObstacleBehaviour: SELF DESTRUCT AND PAUSABLE

    public float despawnZ = 4f;
    public float despawnY = -40f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (transform.position.z < despawnZ || transform.position.y < despawnY)
            gameObject.SetActive(false);
	}
}
