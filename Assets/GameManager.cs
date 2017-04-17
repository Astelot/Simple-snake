using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public float rotateTimer = 1f;
    public float moveTimer = 1f;
    public float snakeMoveSpeed = 0.8f;
    public float levelRotationSpeed = 0.8f;
    public float snakeSpeedMultiplier = 2f;

    public GameObject snakeHeadPrefab;
    public GameObject snakeBodyPrefab;
    public GameObject snakeTailPrefab;

    private int moves;
    private int desiredRotation;
    private Boolean hasSnakeMoved;
    private LevelManager levelManager;
    private InputManager inputManager;
    private SnakeController snakeController;

    public GameObject getSnakeHead() {
        return this.snakeHeadPrefab;
    }

    public GameObject getSnakeBody() {
        return this.snakeBodyPrefab;
    }

    public GameObject getSnakeTail() {
        return this.snakeTailPrefab;
    }

	void Start () {
        levelManager = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>();
        inputManager = GameObject.FindGameObjectWithTag("InputManager").GetComponent<InputManager>();
        snakeController = levelManager.getSnake().GetComponent<SnakeController>();
        moves = 0;
        hasSnakeMoved = true;
	}
	
	void Update () {

        if (hasSnakeMoved)
            rotateTimer -= Time.deltaTime;
        else if (!hasSnakeMoved)
            moveTimer -= Time.deltaTime;

        if(rotateTimer <= 0 && hasSnakeMoved) {

            updateLevel();
            inputManager.setLevelRotation(0);
            resetTimers();
            hasSnakeMoved = false;
        }
        else if (!hasSnakeMoved && moveTimer <= 0)
        {
            updateSnake();
            updateCamera();
            resetTimers();
            hasSnakeMoved = true;
            moves++;
        }
    }

    private void updateCamera()
    {
        iTween.MoveTo(Camera.main.gameObject, levelManager.getGridCenter() + Vector3.up * 10f, 0.8f);
    }

    private void updateSnake()  {
        snakeController.moveSnake(0.8f, moves, snakeSpeedMultiplier);
    }

    private void updateLevel()  {

        desiredRotation = inputManager.getLevelRotation();
        moves++;

        if (desiredRotation != 0)
        {
            iTween.RotateAdd(levelManager.getLevel(), Vector3.up * desiredRotation, 0.8f);
        }
    }

    private void resetTimers()  {

        rotateTimer = 1f;
        moveTimer = 1f;
    }
}
