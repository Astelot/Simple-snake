using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    private List<GameObject> snake = new List<GameObject>();
    private GameObject snakePrefab;
    private Transform level;
    private bool growNextMove;

    private void Awake()
    {
        snake.Add(this.gameObject);
        level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<LevelManager>().getLevel().transform;
        snakePrefab = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().getSnakeBody();
        gameObject.name = "Snake";
    }

    public void moveSnake(float moveSpeed, int move, float speedMultiplier)
    {
        Vector3 lastPosition;
        Vector3 tempPosition;

        lastPosition = transform.position;
        iTween.MoveTo(gameObject, transform.position + Vector3.forward, moveSpeed / getSpeedMultiplier(move, 0, speedMultiplier));

        for (int i = 1; i < snake.Count; i++) {

            tempPosition = snake[i].transform.position;
            iTween.MoveTo(snake[i].gameObject, lastPosition, moveSpeed / getSpeedMultiplier(move, i, speedMultiplier));
            lastPosition = tempPosition;
        }

        if (growNextMove)
        {
            growPart(lastPosition);
            growNextMove = false;
        }

    }

    private void growPart(Vector3 atPosition)
    {
        snake.Add(Instantiate(snakePrefab, atPosition, Quaternion.identity));
        snake[snake.Count - 1].transform.localScale = new Vector3(0f, 0f, 0f);
        renameAndParent();

        growPart(snake[snake.Count - 2], snakePrefab.transform.localScale);
        growPart(snake[snake.Count - 1], snakePrefab.transform.localScale/1.3f);
    }

    public void grow() {
        growNextMove = true;
    }

    public void grow(GameObject prefab, Vector3 position, Transform level, bool isTail)
    {
        snake.Add(Instantiate(prefab, position, Quaternion.identity));
        if (isTail)
        {
            snake[snake.Count - 1].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        renameAndParent();
    }

    private void growPart(GameObject part, Vector3 toSize)
    {
        iTween.ScaleTo(part, toSize, 0.8f);
    }

    private void renameAndParent()
    {
        snake[snake.Count - 1].name = "Body";
        snake[snake.Count - 1].transform.SetParent(level);
    }

    private float getSpeedMultiplier(int move, int i, float multiplier) {

        if(move % 2 == 0)
            return i % 2 == 0 ? multiplier : 1;

        else return i % 2 == 0 ? 1 : multiplier;
    }
}