using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Rigidbody2D rb;
    float moveLimiter = 0.7f;

    public Vector2 movement;
    public Vector2 input;


    // Update is called once per frame
    void Update()
    {
        // Input

        input.x =  Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        
    }

    private void FixedUpdate()
    {
        // Movement

        var horizontal = MoveX(input.x);
        var vertical = MoveY(input.y);

        rb.AddForce((horizontal + vertical) * movementSpeed);

    }


    // w a s d
    // 0 1 2 3 

    public Vector2 MoveX(float x)
    {
        if (x == 1)
        {
            // press D
            var gameManager = GameObject.FindGameObjectWithTag("GameController");
            var currentLevelCell = gameManager.GetComponent<GameManager>().levels[gameManager.GetComponent<GameManager>().currentLevel].levelCell[3];
            var cell3 = gameManager.GetComponent<GameManager>().actions[currentLevelCell];
            var action = cell3.GetComponentInChildren<MoveAction>().direction;
            return action;
        }
        else if (x == -1)
        {
            // press A
            var gameManager = GameObject.FindGameObjectWithTag("GameController");
            var currentLevelCell = gameManager.GetComponent<GameManager>().levels[gameManager.GetComponent<GameManager>().currentLevel].levelCell[1];
            var cell1 = gameManager.GetComponent<GameManager>().actions[currentLevelCell];
            var action = cell1.GetComponentInChildren<MoveAction>().direction;
            return action;
        }
        return Vector2.zero;
    }

    public Vector2 MoveY(float y)
    {
        if (y == 1)
        {
            // press W

            //var gameManager = GameObject.FindGameObjectWithTag("GameController");
            //var currentLevelCell = gameManager.GetComponent<GameManager>().levels[gameManager.GetComponent<GameManager>().currentLevel].levelCell[0];
            //var cell0 = gameManager.GetComponent<GameManager>().actions[currentLevelCell];
            //var action = cell0.GetComponentInChildren<MoveAction>().direction;

            var gameManager = GameObject.FindGameObjectWithTag("GameController");
            var currentLevelCell = gameManager.GetComponent<GameManager>().levels[gameManager.GetComponent<GameManager>().currentLevel].levelCell[0];
            var cell0 = gameManager.GetComponent<GameManager>().actions[currentLevelCell];
            var action = cell0.GetComponentInChildren<MoveAction>().direction;

            return action;
        }
        else if (y == -1)
        {
            var gameManager = GameObject.FindGameObjectWithTag("GameController");
            var currentLevelCell = gameManager.GetComponent<GameManager>().levels[gameManager.GetComponent<GameManager>().currentLevel].levelCell[2];
            var cell2 = gameManager.GetComponent<GameManager>().actions[currentLevelCell];
            var action = cell2.GetComponentInChildren<MoveAction>().direction;
            return action;
        }
        return Vector2.zero;
    }

}
