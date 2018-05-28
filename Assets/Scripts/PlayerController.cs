using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Rigidbody rb;
    public bool dead = false;
    private bool deadOneTimeExecution = true;

    public float jumpForceMultiplier = 0f;
    public float movementForceMultiplier = 0f;
    public float jumpVelocityTop = 8f;
    public float deathAnimationFactor = 0.1f;
    public GameObject cam;
    public cameraShaker cs;
    public bool godMode = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Coin")
        {
            //GameController.score += 10;
            AudioManager.instance.Play("coin");
            GameController.zarawudoCoins++;
            if (GameController.zarawudoCoins == 10)
            {
                GameController gcInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
                gcInstance.StartCoroutine(gcInstance.slowdown());
                StartCoroutine(cs.shake(1f, 0.1f));
                AudioManager.instance.Play("zawarudo");
                GameController.zarawudoCoins = 0;
            }
            other.gameObject.SetActive(false);
        }
    }

    private bool touchingGround()
    {
        return Physics.OverlapSphere(transform.position - Vector3.down / 2, 1f, 1 << 9).Length > 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.collider.tag;
        if ((tag == "Scenario" || tag == "Ostacolo") && !godMode)
        {
            //Destroy(this.gameObject);
            AudioManager.instance.Play("ouch");
            print("Player is dead! (collided with "+collision.collider);
            dead = true;
        }
    }

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
	}

    private void FixedUpdate()
    {
        if(!GameController.paused)
            transform.position += Vector3.right * Input.GetAxis("Horizontal") * movementForceMultiplier;
    }

    private void restart()
    {
        GameController.restart();
    }

    void Update () {
        if (Input.GetKeyDown(KeyCode.G))
        {
            godMode = !godMode;
            GameController.godMode = godMode;
            if(godMode)
                AudioManager.instance.Play("hax");
        }

        // DEATH
        if (dead)
        {
            if (deadOneTimeExecution)
            {
                StartCoroutine(cs.shake(0.2f, 0.05f));
                GameController.paused = true;
                cam.GetComponent<CameraFollowPlayer>().deathcam = true;
                Invoke("restart", 5f);
                deadOneTimeExecution = false;
            }
        }

        // NOT DEAD
        else
        {
            // JUMP
            if (Input.GetKeyDown(KeyCode.W) && !GameController.paused)
            {
                // Salto
                if (touchingGround() && rb.velocity.y <= 0f)
                {
                    AudioManager.instance.Play("jump");
                    print("Player is jumping");
                    rb.AddForce(Vector3.up * jumpForceMultiplier, ForceMode.VelocityChange);
                }
            }
        }
    }
}
