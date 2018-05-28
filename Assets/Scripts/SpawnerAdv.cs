using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerAdv : MonoBehaviour
{

    private float timeCounter = 0f;
    public float spawnTime = 1f;
    public string obj;
    public bool instantFirstSpawn = true;
    public float deltaX = 0f;
    public float deltaY = 0f;
    public float deltaTime = 0f;

    // Use this for initialization
    void Start()
    {
        if (instantFirstSpawn)
            timeCounter = spawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.paused)
        {
            if (obj == "coniglio" && GameController.bossActive == true)
            { 
                print("NO SPAWN");
                return;
            }

            float timeVariation = Random.Range(-deltaTime, deltaTime);

            timeCounter += Time.deltaTime;
            if (timeCounter >= (spawnTime + timeVariation) * Time.deltaTime)
            {
                float xVariation = Random.Range(-deltaX, deltaX);
                Vector3 position = new Vector3(transform.position.x + xVariation, transform.position.y, transform.position.z);
                ObjectPooler.Instance.SpawnFromPool(obj, position, Quaternion.Euler(-90f, 0f, 0f));
                timeCounter = 0f;
            }
        }
    }
}
