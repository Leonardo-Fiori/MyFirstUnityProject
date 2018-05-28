using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireClick : MonoBehaviour {

    public GameObject spark;
    public cameraShaker cameraShaker;

    public int bossDamage;

    public float duration = 0f;
    public float magnitude = 0f;

    private bool timescaling = false;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("Fire1") && !GameController.paused)
        {
            print("FIRE!");

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitData;

            if(Physics.Raycast(ray, out hitData, 1000f))
            {
                print(hitData.collider.tag);
                //print(Quaternion.identity);

                if (hitData.collider.tag == "Mostro")
                {
                    print("MOSTRO");
                    hitData.collider.GetComponentInParent<monsterDeath>().death();
                    GameController.kills++;
                    StartCoroutine(cameraShaker.shake(duration, magnitude));
                    Instantiate(spark, hitData.point + Vector3.up / 2f, Quaternion.identity);
                    AudioManager.instance.Play("quack");
                }

                else if (hitData.collider.tag == "Boss")
                {
                    print("HAI COLPITO IL BOSS!");
                    hitData.collider.GetComponentInParent<BossController>().damage(bossDamage);
                    StartCoroutine(cameraShaker.shake(duration, magnitude));
                    Instantiate(spark, hitData.point + Vector3.up / 2f, Quaternion.identity);
                    AudioManager.instance.Play("quack");
                }

                else
                {
                    //StartCoroutine(cameraShaker.shake(duration / 10f, magnitude / 10f));
                    GameObject part = Instantiate(spark, hitData.point + Vector3.up / 2f, Quaternion.identity);
                    part.transform.localScale *= 0.5f;
                    AudioManager.instance.Play("hit");
                }
            }

            
        }
	}
}
