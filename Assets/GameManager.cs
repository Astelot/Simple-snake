using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject snakeHeadPrefab;
    public GameObject snakeBodyPrefab;
    public float moveTimer = 1f;
    public float timeDecreasePerMove = 0.01f;

    private List<GameObject> snake = new List<GameObject>();
    private int moves;
    private int rotation;

	// Use this for initialization
	void Start () {
        GameObject levelManager = GameObject.FindGameObjectWithTag("LevelManager");
        Vector2 gridSize = levelManager.GetComponent<LevelManager>().gridSize;
        snake.Add(Instantiate(snakeHeadPrefab, new Vector3(Mathf.Round(gridSize.x / 2), 1, Mathf.Round(gridSize.y / 2) - 1), Quaternion.identity));
        //DEBUG
        for (int i = 1; i < 4; i++) {
            snake.Add(Instantiate(snakeBodyPrefab, new Vector3(Mathf.Round(gridSize.x / 2), 1, Mathf.Round(gridSize.y / 2) - 1 - i), Quaternion.identity));
        }
        moves = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if(Input.GetButtonDown("Right"))
        {
            Debug.Log("Stisnuo sam right");
            rotation += 90;
        }

        else if (Input.GetButtonDown("Left"))
        {
            rotation -= 90;
        }

        moveTimer -= Time.deltaTime;
        if(moveTimer < 0) {
            moves++;
            snake[0].GetComponent<SnakeController>().moveSnake(snake, rotation);
            moveTimer = 1f - (moves * timeDecreasePerMove);
        }
	}
}
