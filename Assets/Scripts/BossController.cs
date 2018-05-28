using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    private Rigidbody rb;
    public float inertia = 20f;
    public cameraShaker cs;
    private Animator animator;
    private float attackTimer = 0f;
    public float attackTime;
    public int salute;
    public GameObject player;
    public float speed;
    public float attackTollerance;

	// Use this for initialization
	void Start () {
        GameController.bossActive = true;
        animator = GetComponent<Animator>();
        animator.SetInteger("salute", salute);
        rb = GetComponent<Rigidbody>();
        cs = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<cameraShaker>();
        player = GameObject.FindGameObjectWithTag("Player");

        GameObject[] conigli = GameObject.FindGameObjectsWithTag("Mostro");
        foreach (GameObject coniglio in conigli)
            coniglio.SetActive(false);
	}
	
	void Update () {
        if (GameController.paused) return;

        if (salute <= 0) return;

        // Guarda il giocatore
        transform.LookAt(player.transform);

        // Avanza verso il player
		if(transform.position.z > -2f)
        {
            //print("IL BOSS SI STA AVVICINANDO");
            transform.position += Vector3.back * speed * Time.deltaTime;
        }
        else if (!animator.GetBool("arrivato"))
        {
            //print("IL BOSS E' ARRIVATO");
            animator.SetBool("arrivato", true);
            AudioManager.instance.Play("heiboss");
        }

        // Segui il giocatore oscillando
        if (animator.GetBool("arrivato"))
        {
            float offsetX = (player.transform.position.x - transform.position.x) / inertia;
            Vector3 offset = new Vector3(offsetX,0f,0f);

            transform.position += offset;
        }

        // Setta il trigger di attacco
        float playerX = player.transform.position.x;
        float myX = transform.position.x;
        bool lowerRange = playerX >= myX - attackTollerance;
        bool upperRange = playerX <= myX + attackTollerance;

        if(lowerRange && upperRange)
        {
            attackTimer += Time.deltaTime;
            if(attackTimer >= attackTime * Time.deltaTime)
            {
                //print("BOSS STA PER ATTACCARE IL GIOCATORE!");
                animator.SetTrigger("attacca");
                attackTimer = 0f;
            }
        }
        else
        {
            attackTimer = 0f;
        }

        //print(attackTimer);
	}

    IEnumerator death()
    {
        while (transform.localScale.x >= 0.1f)
        {
            while (GameController.paused) yield return null;
            transform.localScale = transform.localScale * 0.965f;
            print(transform.localScale.x);
            transform.position += Vector3.forward * speed * 0.05f * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }

    // Vieni colpito
    public void damage(int amount)
    {
        transform.localScale *= 1.01f;
        salute -= amount;
        animator.SetInteger("salute", salute);
        print("BOSS COLPITO! " + salute);
        if (salute <= 0)
        {
            GameController.bossActive = false;
            StartCoroutine(death());
            AudioManager.instance.Play("ko");
        }
    }

    public void killPlayer()
    {
        if (GameController.paused) return;

        print("BOSS ATTACCA IL GIOCATORE!");
        float playerX = player.transform.position.x;
        float myX = transform.position.x;
        bool lowerRange = playerX >= myX - attackTollerance/2f;
        bool upperRange = playerX <= myX + attackTollerance/2f;

        if (lowerRange && upperRange)
        {
            print("IL BOSS HA UCCISO IL GIOCATORE!");
            StartCoroutine(cs.shake(0.5f,0.1f));
            AudioManager.instance.Play("ouch");
            AudioManager.instance.Play("ko");
            player.GetComponent<PlayerController>().dead = true;
        }
    }
}
