using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionParticleBehaviour : MonoBehaviour
{
    ParticleSystem part;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if (part.isStopped) Destroy(gameObject);
    }

}

