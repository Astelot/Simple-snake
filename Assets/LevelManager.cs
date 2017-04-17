using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public Vector2 gridSize;
    public Vector2[] snakeStartPosition;

    public GameObject fieldPrefab;
    public GameObject wallPrefab;
    public GameObject snakeHeadPrefab;
    public GameObject snakeBodyPrefab;
    public GameObject snakeTailPrefab;

    private GameObject[,] levelGrid;
    private GameObject[] levelWalls;
    private GameObject level;
    private List<GameObject> snake = new List<GameObject>();

    void Awake()
    {
        generateLevel();
    }

    public GameObject getLevel() {
        return this.level;
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

    private void centerCamera() {
        Camera.main.transform.position = new Vector3(0, Mathf.Max(gridSize.x, gridSize.y) + 1, 0);
        Camera.main.orthographicSize = Mathf.Max(gridSize.x, gridSize.y)/1.2f;
    }

    private void generateSnake()
    {
        GameObject snakePrefab;

        for (int i = 0; i < snakeStartPosition.Length; i++) {

            if (i > 0 && i < snakeStartPosition.Length)
                snakePrefab = snakeBodyPrefab;
            else if (i == 0)
                snakePrefab = snakeHeadPrefab;
            else snakePrefab = snakeTailPrefab;

            snake.Add(Instantiate(snakePrefab, new Vector3(snakeStartPosition[i].x, 1, snakeStartPosition[i].y), Quaternion.identity));

            snake[i].name = "Snake";
            snake[i].transform.SetParent(level.transform);
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

        /*for (int y = -1; y <= gridSize.y; y++) {
            levelWalls[n++] = Instantiate(wallPrefab, new Vector3(-1, 0, y), Quaternion.identity);
            levelWalls[n++] = Instantiate(wallPrefab, new Vector3(gridSize.x, 0, y), Quaternion.identity);
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            levelWalls[n++] = Instantiate(wallPrefab, new Vector3(x, 0, -1), Quaternion.identity);
            levelWalls[n++] = Instantiate(wallPrefab, new Vector3(x, 0, gridSize.y), Quaternion.identity);
        }*/

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