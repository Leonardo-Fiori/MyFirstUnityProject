using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMainTheme : MonoBehaviour {

	void Start () {
        AudioManager.instance.Play("theme");
	}

}
