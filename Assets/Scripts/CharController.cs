using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public GameObject target;
    public float walkSpeed = 2.0f; //normal speed of the character
    public float chaseSpeed = 3.0f; //speed increases while chasing
    public MovementPath originalMovementPath = MovementPath.CirclePath; //the neutral walking pattern the character uses
    public int visionRadius = 4; //how far the character can see in front of them
    public int chaseRadius = 2; //how close the enemy needs to be to continue to chase
    public float maxMovementTime = 3f; //how long the enemy will walk before changing direction

    private MovementPath currentMovementPath = MovementPath.CirclePath;
    private SpriteRenderer sprite;

    private Vector2 movement; //public so EnemyWalking.cs can access it

    public Vector2 Movement
	{
		get
		{
            return movement;
		}
	}

    public enum MovementPath
    {
        None,
        PlayerControlled,
        CirclePath,
        ChaseTarget,
    }
    public enum CharacterFacing
    {
        Up,
        Down,
        Left,
        Right,
    }
    CharacterFacing currentDirection = CharacterFacing.Down; //which way the character is facing (used to determine vision cone)

    //Fixed Movement Time
    float currentMovementTime = 0;


    Vector2 targetPosition
	{
        get
		{
            return target.transform.position; 
		}
	}

    Vector2 currentPosition
	{
        get
		{
            return transform.position; 
        }
	}

	public void Start()
	{
        currentMovementPath = originalMovementPath;
        sprite = GetComponent<SpriteRenderer>();
    }

    private void ChangeColor(Color NewColor)
	{
        sprite.color = NewColor;
    }


	void Update()
    {

        switch (currentMovementPath)
        {
            case MovementPath.PlayerControlled:
                PlayerMovement();
                break;
            case MovementPath.ChaseTarget:
                //If the character is already chasing the player, you need to change how the character
                //determines if the enemy you don't want to use WithinSight anymore, trust me.
                //Instead use a function that's based on distance
                if (WithinChase(targetPosition, currentPosition, chaseRadius))
				{
                    MoveToTarget(currentPosition, targetPosition);
                }
                else
				{
                    //stop chasing and switch back to original movement
                    ChangeColor(Color.white);
                    currentMovementPath = originalMovementPath;
                }
                break;
            case MovementPath.CirclePath:
                //Check if the player happens to be within sight of this enemy, then switch to chasing mode             
                if (WithinSight(targetPosition, currentPosition, currentDirection, visionRadius))
                {
                    Debug.Log("Chase!");
                    ChangeColor(Color.red);
                    currentMovementTime = 0;
                    currentMovementPath = MovementPath.ChaseTarget;
                }
                else
                {
                   // float elapsed = Time.fixedDeltaTime;
                    float elapsed = Time.deltaTime;
                    currentMovementTime += elapsed;
                    float x = 0;
                    float y = 0;

                    if (currentMovementTime >= maxMovementTime)
                    {
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
                    movement.x = x;
                    movement.y = y;
                }
                break;
        }
    }

    private void MoveToTarget(Vector2 currentPosition, Vector2 targetPosition)
	{

        float distanceX =
            Vector2.Distance(
                new Vector2(currentPosition.x, 0),
                new Vector2(targetPosition.x, 0));

        float distanceY =
            Vector2.Distance(
                new Vector2(0, currentPosition.y),
                new Vector2(0, targetPosition.y));

        Vector2 motion = Vector2.zero;
        if (distanceX <= 1 && distanceY <= 1)
        {
            //TODO character has caught or is close enough to catching the player
        }
        else
        {
            if (distanceX > 0)
            {
                motion.x = targetPosition.x - currentPosition.x;
                motion.x = Math.Min(1, Math.Max(-1, motion.x)); // Clamp it to -1..1
            }
            else
            {
                motion.x = 0;
            }
            if (distanceY > 0)
            {
                motion.y = targetPosition.y - currentPosition.y;
                motion.y = Math.Min(1, Math.Max(-1, motion.y)); // Ditto for Y
            }
            else
            {
                motion.y = 0;
            }

            movement = motion; //update movement
        }
    }
    private void NextDirection() //TODO call this function when an enemy walks into collision object so they don't just rub up against it for a while
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

       // Debug.Log("player movment " + movement.x + ", " + movement.y);

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
        }
        else if (x < 0)
        {
            currentDirection = CharacterFacing.Left;
        }
        else if (y > 0)
        {
            currentDirection = CharacterFacing.Up;
        }
        else if (y < 0)
        {
            currentDirection = CharacterFacing.Down;
        }
    }

    public bool WithinChase(Vector2 targetPosition, Vector2 chaserPosition, int VisionRadius)
    {
        float distance = Vector2.Distance(
                    chaserPosition,
                    targetPosition);

        return distance < VisionRadius + (VisionRadius * .5f);
    }

    public bool WithinSight(Vector2 targetPosition, Vector2 watcherPosition, CharacterFacing watcherDirection, int VisionRadius)
    {
        float vFrac;
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
        //Change walking speed based on behavior
        float currentSpeed = walkSpeed;
        switch (currentMovementPath)
		{
            case MovementPath.ChaseTarget:
                currentSpeed = chaseSpeed;
                break;
		}

        transform.Translate(Vector3.right * movement.x * currentSpeed * Time.deltaTime);
        transform.Translate(Vector3.up * movement.y * currentSpeed * Time.deltaTime);
    }
}
