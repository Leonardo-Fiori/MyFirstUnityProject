using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playButton : MonoBehaviour {

    public void click()
    {
        AudioManager.instance.Play("click");
        SceneManager.LoadScene("Main");
        print("INIZIAMO...");
    }
}
