using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterDeath : MonoBehaviour
{

    MeshRenderer myMesh;
    public GameObject myParticle;

    private void Start()
    {
        myMesh = GetComponent<MeshRenderer>();
    }
    public void death()
    {
        print("I'm dead :(");
        GameObject part = Instantiate(myParticle);
        part.transform.position = transform.position;
        GameController.score += 10f;
        gameObject.SetActive(false);
    }


}
