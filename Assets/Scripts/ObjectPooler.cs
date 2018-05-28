using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {


    #region Singleton
    public static ObjectPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    // Definisco una Pool

    [System.Serializable]
    public class PoolInfo
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    // Salvo tutte le pool in una lista di pools modificabile da editor

    public List<PoolInfo> pools;

    // Associo una key alla pool giusta nella lista tramite un dizionario

    public Dictionary<string, Queue<GameObject>> poolDictionary;

    // Creo delle code di gameobject grandi quanto pool.size, 
    // di oggetti di tipo pool.prefab, 
    // e le salvo nel dizionario con tag pool.tag

	void Start () {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(PoolInfo pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for(int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectPool);
        }
	}

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            throw new System.Exception("Pool tag inesistente!");
        }

        GameObject objToSpawn = poolDictionary[tag].Dequeue();

        if(objToSpawn == null)
        {
            print("POOLING ERROR: oggetto non trovato! ("+tag+")");
        }

        objToSpawn.SetActive(true);
        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objToSpawn);

        IPooledObject pooledObj = objToSpawn.GetComponent<IPooledObject>();

        if(pooledObj != null)
        {
            pooledObj.onObjectSpawn();
        }

        return objToSpawn;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
