using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static bool paused = false;
    public static float score = 0;
    public static int kills = 0;
    public static bool godMode = false;
    private bool timescaling = false;
    public static bool bossActive = false;
    public static int zarawudoCoins = 0;

    public Canvas pauseMenu;
    public Canvas pauseCanvas;

    public static float gameSpeed = 0.2f;

    public Text scoreText;

    public GameObject bossPrefab;

    private static bool restarting = false;
    public GameObject mainLight;
    public GameObject player;
    private PlayerController pc;
    private Light lt;

    public IEnumerator slowdown()
    {
        print("SLOWDOWN!");
        while (gameSpeed > 0.01f)
        {
            print("slowing down " + gameSpeed);
            gameSpeed = gameSpeed * 0.99f;
            yield return null;
        }

        //Invoke("speedup", 1f);
        
        Invoke("startSpeedup", 1f);
        yield return null;
    }

    private void startSpeedup()
    {
        StartCoroutine(speedup());
    }

    private IEnumerator speedup()
    {
        print("SPEEDUP!");
        while (gameSpeed < 0.2f)
        {
            gameSpeed = gameSpeed * 1.1f;
            print("speeding up "+gameSpeed);
            yield return null;
        }

        timescaling = false;
        yield return null;
    }

    private void Start()
    {
        lt = mainLight.GetComponent<Light>();
        pc = player.GetComponent<PlayerController>();
        scoreText.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            print("score " + score);
            print("kills " + kills);
            Instantiate(bossPrefab, new Vector3(0, 0, 20), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }

        if (Input.GetKeyDown(KeyCode.P) && !pc.dead)
        {
            paused = !paused;
            pauseMenu.gameObject.SetActive(paused);
            pauseCanvas.gameObject.SetActive(paused);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && pc.godMode && !pc.dead)
        {
            if (!timescaling)
            {
                AudioManager.instance.Play("hax");
                StartCoroutine(slowdown());
            }
        }

        if (!pc.dead && !paused)
        {
            score += Time.deltaTime;
            scoreText.text = "Score: " + (Mathf.Round(score)).ToString();
        }

        if (restarting)
        {
            lt.intensity -= 0.01f;
        }

        if (lt.intensity <= 0 && restarting)
        {
            bossActive = false;
            restarting = false;
            paused = false;
            zarawudoCoins = 0;
            score = 0;
            kills = 0;
            lt.intensity = 2.5f;
            print(lt.intensity);
            SceneManager.LoadScene("Main");
        }

        if(kills % 10 == 0 && kills >= 10 && bossActive == false)
        {
            Instantiate(bossPrefab, new Vector3(0, 0, 20), Quaternion.identity);
            kills++;
        }
    }

    public static void restart()
    {
        restarting = true;
    }

}
