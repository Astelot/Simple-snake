using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public Vector2 gridSize;
    public GameObject fieldPrefab;
    public GameObject wallPrefab;

    private GameObject[,] levelGrid;
    private GameObject[] levelWalls;
    private GameObject level;

    // Use this for initialization
    void Awake()
    {
        generateLevel();
    }

    void generateLevel()
    {
        level = new GameObject("Level");
        levelGrid = new GameObject[(int)gridSize.x, (int)gridSize.y];
        levelWalls = new GameObject[(int)(gridSize.x * 2 + gridSize.y * 2 + 4)];

        generateLevelWalls();
        generateLevelFields();
    }

    void generateLevelFields()
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
        for (int y = -1; y <= gridSize.y; y++) {
            levelWalls[n++] = Instantiate(wallPrefab, new Vector3(-1, 0, y), Quaternion.identity);
            levelWalls[n++] = Instantiate(wallPrefab, new Vector3(gridSize.x, 0, y), Quaternion.identity);
        }

        for (int x = 0; x < gridSize.x; x++)
        {
            levelWalls[n++] = Instantiate(wallPrefab, new Vector3(x, 0, -1), Quaternion.identity);
            levelWalls[n++] = Instantiate(wallPrefab, new Vector3(x, 0, gridSize.y), Quaternion.identity);
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
        levelGrid[x, y] = Instantiate(fieldPrefab, new Vector3(x, 0, y), Quaternion.identity);
        levelGrid[x, y].transform.SetParent(level.transform);
        levelGrid[x, y].GetComponent<FieldManager>().generateField(Field.Grassland);
    }
}