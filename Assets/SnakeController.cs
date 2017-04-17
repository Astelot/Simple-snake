using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    public void moveSnake(List<GameObject> snakeList, int rotation)
    {
        Vector3 lastPosition;
        Vector3 tempPosition;

        lastPosition = transform.position;
        transform.position = transform.forward + transform.position;

        for (int i = 1; i < snakeList.Count; i++) {

            tempPosition = snakeList[i].transform.position;
            snakeList[i].transform.position = lastPosition;
            lastPosition = tempPosition;
        }
    }
}