using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public GameObject character;

    public float walkSpeed = 5.0f;

    Vector2 movement;

    void Update()
    {
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

    }
    void FixedUpdate()
    {

        character.transform.Translate(Vector3.right * movement.x * walkSpeed* Time.deltaTime);
        character.transform.Translate(Vector3.up * movement.y * walkSpeed * Time.deltaTime);
    }
}
