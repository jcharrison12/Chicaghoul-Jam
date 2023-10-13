using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Walking : MonoBehaviour
{

    Animator anim;
    Vector2 movement;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
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
        
 //Detect when the A arrow key is pressed down
        if (Input.GetKeyDown(KeyCode.R))
            Debug.Log("A key was pressed.");

        //Detect when the A arrow key has been released
        if (Input.GetKeyUp(KeyCode.R))
            Debug.Log("A key was released.");
    }

    public void FixedUpdate()
    {
        anim.SetFloat("horizontal_axis", movement.x);
        anim.SetFloat("vertical_axis", movement.y);
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
