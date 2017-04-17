using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float moveTimer = 1f;
    public float timeDecreasePerMove = 0.01f;

    private int moves;
    private int desiredRotation;
    private LevelManager levelManager;
    private InputManager inputManager;

	void Start () {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        inputManager = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        moves = 0;
	}
	
	void Update () {

        moveTimer -= Time.deltaTime;
        if(moveTimer < 0) {
            updateSnake();
            updateLevel();
            resetTimers();
        }
    }

    private void updateSnake()  {

    }

    private void updateLevel()  {

        desiredRotation = inputManager.getLevelRotation();
        moves++;

        if (desiredRotation != 0)
            iTween.RotateAdd(levelManager.getLevel(), Vector3.up * desiredRotation, 0.8f);
    }

    private void resetTimers()  {

        moveTimer = 1f;
        inputManager.setLevelRotation(0);
    }
}
