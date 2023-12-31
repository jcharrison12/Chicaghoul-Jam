using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Scoring : MonoBehaviour
{
    public List<BodyPart> scores = new List<BodyPart>();
    public JobInstance jobInst;
    //public Jobs job;
    public TextMeshProUGUI boss;

    private AudioSource myAudio;
    public AudioClip youWin;
    public bool audio_enabled = true;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        myAudio = GetComponent<AudioSource>();

        //jobInst = new JobInstance();
        //job = ScriptableObject.CreateInstance<Jobs>();
        //job = jobInst.jobData;
    }

    // Update is called once per frame
    void Update()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        if (activeScene.name == "Interview")
        {
            if (audio_enabled)
            {
                ScorePoints();
                audio_enabled = false;
            }
            
        } 
       
    }
    public IEnumerator Waiting()
    {

        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Interview");
    }
    public void ScorePoints()
    {
        boss = GameObject.Find("Boss Speech").GetComponent<TextMeshProUGUI>();
        //BodyPart.PartOption tempEnum = (BodyPart.PartOption)i;
        int points = 0;
        List<int> pointInt = new List<int>();
        for(int i = 0; i < 5; i ++)
        {
            var tempEnum = (BodyPart.PartOption)i;
            var temp = scores.Find(
            delegate (BodyPart bp)
            {
                return bp.part == tempEnum;
            });
            if (temp != null)
            {
                pointInt.Add(temp.quality);
            }
            else
            {
                pointInt.Add(-1);
            }
        }
        for(int i= 0; i < pointInt.Count; i++)
        {
            if(jobInst.jobData.mins[i] <= pointInt[i])
            {
                points += 1;
            }
        }
        if(points >=0 && points <= 1)
        {
            boss.text = "Sorry, this job was not meant for you. Better luck next time.";
        }
        else if(points >= 2 && points <=3)
        {
            myAudio.PlayOneShot(youWin);
            boss.text = "Welcome to the team. You'll do, I guess.";

        }
        else if(points >= 4)
        {
            myAudio.PlayOneShot(youWin);
            boss.text = "Congratulations! Welcome to the team! You were the best applicant!";
        }
    }
    
  
}
