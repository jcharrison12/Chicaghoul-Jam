using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Walking : MonoBehaviour
{
    public float maxDigTime = 2f;
    public float maxBodyPartShow = 1.5f;
    public float maxStunTime = .5f;


    public List<BodyPart> bpOptions;

    public InventoryPlayer bodyPartManager;
    Animator anim;
    Vector2 movement;

    public enum TerryBehavior
    {
        None,
        Digging,
    }

    private TerryBehavior currentBehavior = TerryBehavior.None;
    private float currentDigTime = 0;
    private float currentStunTime = 0;
    private GameObject dugUpGrave = null;

    private GameObject partSprite;
    private float currentBodyPartShow = 0;
    private bool displayBodyPart = false;

    private AudioSource myAudio;
    public AudioClip digSound, getBodyPart;

    public void Start()
    {
        anim = GetComponent<Animator>();
        bodyPartManager = GetComponent<InventoryPlayer>();

        myAudio = GetComponent<AudioSource>();


        partSprite = GameObject.Find("BodyPartDugUp");  
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
                if(currentStunTime > 0)
				{
                    Debug.Log("stunned...");
                    currentStunTime -= Time.deltaTime;
				}
                else if (Input.GetKey(KeyCode.F))
                {
                    currentBehavior = TerryBehavior.Digging;
                    myAudio.PlayOneShot(digSound);
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
                    StopDigging();
                }
                break;
        }
        //<<<<<<< HEAD


        DisplayBodyPart();
    }

    private void DisplayBodyPart()
	{
        if(displayBodyPart)
        { 
            currentBodyPartShow += Time.deltaTime;
            if(currentBodyPartShow < maxBodyPartShow)
			{

                var opacity = partSprite.GetComponent<SpriteRenderer>().color;
                opacity.a = 1 - (currentBodyPartShow / maxBodyPartShow);
                partSprite.GetComponent<SpriteRenderer>().color = opacity;
            }
		}
	}


    private void StopDigging()
	{
        currentBehavior = TerryBehavior.None;
        currentDigTime = 0;
        anim.StopPlayback();
        anim.Rebind();
    }

    private void TerryDigsUpBodyPart()
    {
        if (dugUpGrave != null)
		{
            Dug dug = dugUpGrave.GetComponent<Dug>();
            if(!dug.dug)
			{
                myAudio.PlayOneShot(getBodyPart);
                dug.dug = true;
                GenerateRandomBodyPart();
            }
        }
		else
		{
            displayBodyPart = false;
		}
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Grave")
            dugUpGrave = col.gameObject;
    }

    void OnTriggerExit2D(Collider2D col)
    {
        dugUpGrave = null;
    }

	void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Debug.Log("HIT BY ENEMY");
            currentStunTime = maxStunTime;
            StopDigging();
            //SOUND - play hit by ghost sound
        }
    }

	private void GenerateRandomBodyPart()
    {
        int randomNumber = Random.Range(0, 5);
        BodyPart.PartOption tempPart = (BodyPart.PartOption)randomNumber;
        Debug.Log("temp part" + tempPart);
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
        AddBodyPart(newBodyPart);
        ShowNewBodyPart(newBodyPart);
    }

    void AddBodyPart(BodyPart newBodyPart)
    {
        Debug.Log("newbodypart " + newBodyPart);
        //BodyPartInstance bp = new BodyPartInstance();
        //bp.bpType = newBodyPart;
        //Debug.Log("bp " + bp);
        bodyPartManager.AddItem(newBodyPart);
    }

    void ShowNewBodyPart(BodyPart newBodyPart)
    {
        partSprite.GetComponent<SpriteRenderer>().sprite = newBodyPart.icon;
        partSprite.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
        var opacity = partSprite.GetComponent<SpriteRenderer>().color;
        opacity.a = 1.0f;
        partSprite.GetComponent<SpriteRenderer>().color = opacity;
        currentBodyPartShow = 0; //reset the timer to show it for 1 second
        displayBodyPart = true;
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
                if (movement.x > 0)
                    anim.Play("Digging-right");
                else
                    anim.Play("Digging-left");
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
