using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerSimple : MonoBehaviour {

    private float timeCounter = 0f;
    public float spawnTime = 1f;
    public string obj;
    public bool instantFirstSpawn = true;

	// Use this for initialization
	void Start () {
        if (instantFirstSpawn)
            timeCounter = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
        if (!GameController.paused)
        {
            timeCounter += Time.deltaTime;
            if (timeCounter >= spawnTime * Time.deltaTime)
            {
                ObjectPooler.Instance.SpawnFromPool(obj, transform.position, transform.rotation);
                timeCounter = 0f;
            }
        }
	}
}
