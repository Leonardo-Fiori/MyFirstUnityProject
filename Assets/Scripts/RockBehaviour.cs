using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : ObstacleBehaviour {

	// Use this for initialization
	void Start () {
        Vector3 ang = Quaternion.ToEulerAngles(transform.rotation);
        ang.y += Random.Range(-90f, 90f);
        transform.rotation = Quaternion.Euler(ang);

        transform.localScale = transform.localScale * Random.Range(1, 4);
	}

}
