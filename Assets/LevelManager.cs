using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public Vector2 gridSize;
    public Vector2[] snakeStartPosition;

    public GameObject fieldPrefab;
    public GameObject wallPrefab;

    //DEBUG
    public GameObject food;

    private GameObject[,] levelGrid;
    private GameObject[] levelWalls;
    private GameObject level;
    private GameObject snake;
    private GameManager gameManager;
    private SnakeController snakeController;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        generateLevel();

        //DEBUG
        Instantiate(food, Vector3.zero + Vector3.up, Quaternion.identity);
    }

    public GameObject getLevel() {
        return this.level;
    }

    public GameObject getSnake() {
        return this.snake;
    }

    public void generateLevel()
    {
        level = new GameObject("Level");
        levelGrid = new GameObject[(int)gridSize.x, (int)gridSize.y];
        levelWalls = new GameObject[(int)(gridSize.x * 2 + gridSize.y * 2 + 4)];

        generateLevelWalls();
        generateLevelFields();
        generateSnake();
        centerCamera();
    }

    public Vector3 getGridCenter() {

        return (levelGrid[0, 0].transform.position +
            levelGrid[(int)gridSize.x - 1, 0].transform.position +
            levelGrid[0, (int)gridSize.y - 1].transform.position +
            levelGrid[(int)gridSize.x - 1, (int)gridSize.y - 1].transform.position) / 4;
    }

    private void centerCamera() {

        Camera.main.transform.position = getGridCenter() + new Vector3(0, 10, 0);
        Camera.main.orthographicSize = Mathf.Max(gridSize.x, gridSize.y)/1.2f;
    }

    private void generateSnake()
    {
        generateHead();
        generateBody();
    }

    private void generateHead()
    {
        snake = Instantiate(gameManager.getSnakeHead(), new Vector3(snakeStartPosition[0].x, 1, snakeStartPosition[0].y), Quaternion.identity);
        snake.transform.SetParent(level.transform);
        snakeController = GameObject.FindGameObjectWithTag("Snake").GetComponent<SnakeController>();
    }

    private void generateBody()
    {
        GameObject snakePrefab = gameManager.getSnakeBody();
        for (int i = 1; i < snakeStartPosition.Length; i++)
        {
            snakeController.grow(snakePrefab, new Vector3(snakeStartPosition[i].x, 1, snakeStartPosition[i].y), level.transform, i == snakeStartPosition.Length - 1 ? true : false);
        }
    }

    private void generateLevelFields()
    {
        for (int x = 0; x < gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                populateLevel(x, y);
            }
        }
    }

    private void generateLevelWalls() {

        int n = 0;
        for(int y = -1; y <= gridSize.y; y++)
        {
            if (y == -1 || y == gridSize.y) {
                for (int x = -1; x <= gridSize.x; x++)
                {
                    levelWalls[n++] = Instantiate(wallPrefab, new Vector3(x - gridSize.x/2, 0, y - gridSize.y/2), Quaternion.identity);
                }

            }
            else
            {
                levelWalls[n++] = Instantiate(wallPrefab, new Vector3(-1 - gridSize.x / 2, 0, y - gridSize.y / 2), Quaternion.identity);
                levelWalls[n++] = Instantiate(wallPrefab, new Vector3(gridSize.x - gridSize.x / 2, 0, y - gridSize.y / 2), Quaternion.identity);
            }

        }
        populateLevelWalls();
    }

    private void populateLevelWalls() {

        for (int i = 0; i < levelWalls.Length; i++) {
            levelWalls[i].transform.SetParent(level.transform);
            levelWalls[i].name = "Wall";
        }
    }

    private void populateLevel(int x, int y)
    {
        levelGrid[x, y] = Instantiate(fieldPrefab, new Vector3(x - gridSize.x/2, 0, y - gridSize.y/2), Quaternion.identity);
        levelGrid[x, y].transform.SetParent(level.transform);
        levelGrid[x, y].GetComponent<FieldManager>().generateField(Field.Grassland);
    }
}