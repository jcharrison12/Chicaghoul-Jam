using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Walking : MonoBehaviour
{
    public float maxDigTime = 3f;


    public List<BodyPart> inventoryList;
    public List<BodyPart> bpOptions;

    Animator anim;
    Vector2 movement;

    public enum TerryBehavior
    {
        None,
        Digging,
    }

    private TerryBehavior currentBehavior = TerryBehavior.None;
    private float currentDigTime = 0;

    public void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        switch (currentBehavior)
        {
            case TerryBehavior.None:
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
                if (Input.GetKey(KeyCode.F))
                {
                    Debug.Log("key down");
                    currentBehavior = TerryBehavior.Digging;
                }
                break;
            case TerryBehavior.Digging:
                float elapsed = Time.deltaTime;
                currentDigTime += elapsed;
                if (currentDigTime >= maxDigTime)
                {
                    TerryDigsUpBodyPart();
                    currentDigTime = 0;
                }
                if (!Input.GetKey(KeyCode.F))
                {
                    Debug.Log("key up");
                    StopDigging();
                }
                break;
        }
//<<<<<<< HEAD
    }

    private void StopDigging()
	{
        currentBehavior = TerryBehavior.None;
        currentDigTime = 0;
        anim.StopPlayback();
        anim.Rebind();
        anim.SetFloat("Digging-right", 0);
        Debug.Log("STOPPPPPPPPPPPPPPP!!");
    }

    private void TerryDigsUpBodyPart()
    {
        if(dugUpGrave != null)
		{
            DigInteract digInteract = dugUpGrave.GetComponent<DigInteract>();
            if(!digInteract.dugUp)
			{
                digInteract.dugUp = true;
                GenerateRandomBodyPart();
                Debug.Log("BODY PART DUG UP");
            }
        }
    }

    private void GenerateRandomBodyPart()
    {
        int randomNumber = Random.Range(0, 5);
        BodyPart.PartOption tempPart = (BodyPart.PartOption)randomNumber;
        BodyPart newBodyPart = ScriptableObject.CreateInstance<BodyPart>();
        List<BodyPart> results = bpOptions.FindAll(
          delegate (BodyPart bp)
          {
              return bp.part == tempPart;
          }
          );
        Debug.Log("results " + results.Count + " " + results);
        newBodyPart = results[Random.Range(0, results.Count)];
        Debug.Log(newBodyPart);
        inventoryList.Add(newBodyPart);
        ShowBodyPart(newBodyPart);
    }

        //Detect when the A arrow key has been released
        if (Input.GetKeyUp(KeyCode.R))
            Debug.Log("A key was released.");
//>>>>>>> main
    }

    public void FixedUpdate()
    {
        switch(currentBehavior)
		{
            case TerryBehavior.None:
                Debug.Log("Walking");
                anim.SetFloat("horizontal_axis", movement.x);
                anim.SetFloat("vertical_axis", movement.y);
                break;
            case TerryBehavior.Digging:
                Debug.Log("Digging");
                anim.Play("Digging-right");
                break;

		}

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
