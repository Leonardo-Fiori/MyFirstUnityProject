using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackPlayer : MonoBehaviour {

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerTrigger")
        {
            print("Coniglio satanico fugge!");
            anim.SetBool("AttackBool", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerTrigger")
        {
            print("Coniglio satanico usa azione!");
            anim.SetBool("AttackBool", true);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
