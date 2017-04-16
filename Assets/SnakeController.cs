using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    Vector3 lastHeadPosition;
    Vector3 lastBodyPosition;
    Vector3 rotation = new Vector3(0,0,0);

    public void moveSnake(List<GameObject> snakeList, int rotation)
    {
        Debug.Log("POzivam move snake");
        transform.eulerAngles = new Vector3(0, rotation, 0);

        lastHeadPosition = transform.position;
        transform.position = transform.forward + transform.position;

        for (int i = 1; i < snakeList.Count; i++) {
            Debug.Log("for petlja");
            lastBodyPosition = snakeList[i].transform.position;
            snakeList[i].transform.position = lastHeadPosition;
            lastHeadPosition = lastBodyPosition;
        }
    }
}
