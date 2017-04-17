using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour {

    private SnakeController snake;

    private void Awake()
    {
        snake = gameObject.GetComponent<SnakeController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Hazard")
        {
            Debug.Log("I hit a wall!");
        }
        else if(other.gameObject.tag == "Food")
        {
            snake.grow();
            Destroy(other.gameObject);
        }
    }
}
