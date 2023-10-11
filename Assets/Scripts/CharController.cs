using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public GameObject character;

    public float walkSpeed = 5.0f;
    public float chaseSpeed = 8.0f; //speed increases while chasing

    Vector2 movement;

    int visionRadius = 10; //how far the character can see in front of them

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
    MovementPath currentMovementPath = MovementPath.CirclePath;
    //Fixed Movement Time
    float currentMovementTime = 0;
    float maxMovementTime = 5f;

    Vector2 playerPosition
	{
        get
		{
            return new Vector2(0, 0); //TODO how do you get the player position?
		}
	}

    Vector2 currentPosition
	{
        get
		{
            return new Vector2(character.transform.position.x, character.transform.position.y);
        }
	}

    void Update()
    {


        switch (currentMovementPath)
        {
            case MovementPath.PlayerControlled:
                PlayerMovement();
                break;
            case MovementPath.ChaseTarget:
                if (WithinSight(playerPosition, currentPosition, currentDirection, visionRadius))
				{
                    //TODO move to target
                }
                else
				{
                    currentMovementPath = originalMovementPath;
                }
                break;
            case MovementPath.CirclePath:
                //Check if the player happens to be within sight of this enemy             
                if (WithinSight(playerPosition, currentPosition, currentDirection, visionRadius))
                {
                    currentMovementTime = 0;
                    currentMovementPath = MovementPath.ChaseTarget;
                }
                else
                {
                    float elapsed = Time.fixedDeltaTime;
                    currentMovementTime += elapsed;
                    float x = 0;
                    float y = 0;

                    if (currentMovementTime >= maxMovementTime)
                    {
                        Debug.Log("change direction");
                        NextDirection();
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
                }
                break;
        }
    }


    private void NextDirection() //TODO call this function when an enemy walks into collision object
    {
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
        FaceDirection(movement.x, movement.y);
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



    public bool WithinSight(Vector2 targetPosition, Vector2 watcherPosition, CharacterFacing watcherDirection, int VisionRadius)
    {
        float vFrac = 0;
        float confFract = 2f;

        if (watcherDirection == CharacterFacing.Down)
        {
            if (targetPosition.y > watcherPosition.y && targetPosition.y < watcherPosition.y + VisionRadius)
            {
                vFrac = (targetPosition.y - watcherPosition.y) / VisionRadius;
                if (targetPosition.x > watcherPosition.x - (VisionRadius * vFrac / confFract) && targetPosition.x < watcherPosition.x + (VisionRadius * vFrac / confFract))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        else if (watcherDirection == CharacterFacing.Up)
        {
            if (targetPosition.y < watcherPosition.y && targetPosition.y > watcherPosition.y - VisionRadius)
            {
                vFrac = (watcherPosition.y - targetPosition.y) / VisionRadius;
                if (targetPosition.x > watcherPosition.x - (VisionRadius * vFrac / confFract) && targetPosition.x < watcherPosition.x + (VisionRadius * vFrac / confFract))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        if (watcherDirection == CharacterFacing.Right)
        {

            if (targetPosition.x > watcherPosition.x && targetPosition.x < watcherPosition.x + VisionRadius)
            {
                vFrac = (targetPosition.x - watcherPosition.x) / VisionRadius;
                if (targetPosition.y > watcherPosition.y - (VisionRadius * vFrac / confFract) && targetPosition.y < watcherPosition.y + (VisionRadius * vFrac / confFract))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }


        if (watcherDirection == CharacterFacing.Left)
        {
            if (targetPosition.x < watcherPosition.x && targetPosition.x > watcherPosition.x - VisionRadius)
            {
                vFrac = (watcherPosition.x - targetPosition.x) / VisionRadius;
                if (targetPosition.y > watcherPosition.y - (VisionRadius * vFrac / confFract) && targetPosition.y < watcherPosition.y + (VisionRadius * vFrac / confFract))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }
        return false;
    }


    void FixedUpdate()
    {

        character.transform.Translate(Vector3.right * movement.x * walkSpeed * Time.deltaTime);
        character.transform.Translate(Vector3.up * movement.y * walkSpeed * Time.deltaTime);
    }
}
