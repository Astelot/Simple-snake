using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

    int levelRotation;

    private void Start()
    {
        levelRotation = 0;
    }

    void Update () {

        if (Input.GetButtonDown("Right"))
        {
            levelRotation += 90;
        }

        else if (Input.GetButtonDown("Left"))
        {
            levelRotation -= 90;
        }
    }

    public void setLevelRotation(int levelRotation)
    {
        this.levelRotation = levelRotation;
    }

    public int getLevelRotation() {

        return Mathf.Clamp(levelRotation, -90, 90);
    }
}
