using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour {

    public void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Wall")
        {
            Application.Quit();
        }
        else if (Col.gameObject.tag == "Body")
        {
            Application.Quit();
        }
    }
}
