using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quitButton : MonoBehaviour {

    public bool useListener = false;

    public void click()
    {
        print("CIAO!");
        Application.Quit();
    }

    private void Start()
    {
        if (useListener)
        {
            Button btn = GetComponent<Button>();
            print("CIAOO! (via listener)");
            btn.onClick.AddListener(Application.Quit);
        }
    }
}
