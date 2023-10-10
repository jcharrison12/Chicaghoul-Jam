using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public GameObject character;

    public float walkSpeed = 5.0f;

    Vector2 movement;


    
    public enum MovementPath
    {
        None,
        PlayerControlled,
        CirclePath,
        CirclePathWait,
        ChaseTarget,
        Reset,
    }
    public enum CharacterFacing
    {
        Up,
        Down,
        Left,
        Right,
    }
    CharacterFacing currentDirection = CharacterFacing.Down;
    MovementPath originalMovementPath = MovementPath.CirclePath;
    //Fixed Movement Time
    float currentMovementTime = 0;
    float maxMovementTime = 5f;

    //old update
    /*
          movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");

        if (movement.y != 0)
        {
            movement.x = 0;
        }
        if (movement.x != 0)
        {
            movement.y = 0;
        }
    */

    void Update()
    {


        switch (originalMovementPath)
        {
            case MovementPath.PlayerControlled:
                PlayerMovement();
                break;
            case MovementPath.CirclePath:
                float elapsed = Time.fixedDeltaTime;
                currentMovementTime += elapsed;
                float x = 0;
                float y = 0;
                
                if (currentMovementTime >= maxMovementTime)
                {
                    Debug.Log("change direction");
                    switch (currentDirection)
                    {
                        case CharacterFacing.Up:
                            currentDirection = CharacterFacing.Right;
                            break;
                        case CharacterFacing.Right:
                            currentDirection = CharacterFacing.Down;
                            break;
                        case CharacterFacing.Down:
                            currentDirection = CharacterFacing.Left;
                            break;
                        case CharacterFacing.Left:
                            currentDirection = CharacterFacing.Up;
                            break;
                    }
                    currentMovementTime = 0;
                }
                
                switch (currentDirection)
                {
                    case CharacterFacing.Up:
                        y = -1;
                        break;
                    case CharacterFacing.Right:
                        x = 1;
                        break;
                    case CharacterFacing.Down:
                        y = 1;
                        break;
                    case CharacterFacing.Left:
                        x = -1;
                        break;
                }
                Debug.Log("AI movment " + x + ", " + y);
                movement.x = x;
                movement.y = y;
               // AnimateDirection(x, y);
                break;
        }
        FaceDirection(movement.x, movement.y);
    }



    private void PlayerMovement()
    {
            movement.x = Input.GetAxis("Horizontal");
            movement.y = Input.GetAxis("Vertical");

        Debug.Log("player movment " + movement.x + ", " + movement.y);

        if (movement.y != 0)
        {
            movement.x = 0;
        }
        if (movement.x != 0)
        {
            movement.y = 0;
        }
    }

    private void FaceDirection(float x, float y)
{
    if (x > 0)
    {
        currentDirection = CharacterFacing.Right;
        Debug.Log("right");
    }
    else if (x < 0)
    {
        currentDirection = CharacterFacing.Left;
        Debug.Log("Left");
    }
    else if (y > 0)
    {
        currentDirection = CharacterFacing.Up;
        Debug.Log("Up");
    }
    else if (y < 0)
    {
        currentDirection = CharacterFacing.Down;
        Debug.Log("Down");
    }
    else
    {
        Debug.Log(currentDirection.ToString());
    }
}



void FixedUpdate()
{

    character.transform.Translate(Vector3.right * movement.x * walkSpeed* Time.deltaTime);
    character.transform.Translate(Vector3.up * movement.y * walkSpeed * Time.deltaTime);
}
}
