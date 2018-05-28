using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathParticleBehaviour : MonoBehaviour {

    public float multiplier = 2f;
    private float time = 0f;
    private float despawnTime = 155f;

    // Use this for initialization
    void Start () {
        Vector3 force = new Vector3(Random.Range(-1f, 1f), 0, 8f);
        GetComponent<Rigidbody>().AddForce( force * multiplier, ForceMode.VelocityChange);
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= despawnTime * Time.deltaTime)
        {
            transform.localScale = transform.localScale - Vector3.one * 0.009f;
            if (transform.localScale.x < 0.0001)
                Destroy(this.gameObject);
        }
	}
}
