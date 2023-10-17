using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class JobSelect : MonoBehaviour
{
    public List<Jobs> allJobOptions = new List<Jobs>();
    public List<Jobs> jobsDisplayed = new List<Jobs>();
    public GameObject jobboard;
    public TMP_FontAsset gabarito;
    public int activeJob = 0;
    public GameObject _scoreObject;
    public JobInstance this_job_inst;
    
    //public Jobs selectedJob = new Jobs();
    // Start is called before the first frame update
    void Start()
    {
        this_job_inst = new JobInstance();
        for(int i= 0; i < 3; i++)
        {
            var temp = allJobOptions[Random.Range(0, allJobOptions.Count)];
            allJobOptions.Remove(temp);
            jobsDisplayed.Add(temp);
        }
        PostJobs();
    }

    // Update is called once per frame
    void Update()
    {
        SelectJobs();
        SubmitJob();
    }
    public void PostJobs()
    {
        for (int i = 0; i < jobsDisplayed.Count; i++)
        {
            GameObject jobpost = new GameObject();
            jobpost.AddComponent<TextMeshProUGUI>();
            jobpost.GetComponent<TextMeshProUGUI>().text = jobsDisplayed[i].title + "\n" + "\n" + jobsDisplayed[i].description;
            jobpost.GetComponent<TextMeshProUGUI>().color = Color.black;
            jobpost.GetComponent<TextMeshProUGUI>().faceColor = Color.black;
            jobpost.GetComponent<TextMeshProUGUI>().font = gabarito;
            jobpost.GetComponent<TextMeshProUGUI>().fontSize = 50;
            var opacity = jobpost.GetComponent<TextMeshProUGUI>().color;
            opacity.a = .3f;
            jobpost.GetComponent<TextMeshProUGUI>().color = opacity;
            jobpost.transform.SetParent(jobboard.transform);
        }
        HighlightJobInitial();
    }
    public void HighlightJobInitial()
    {
        if (jobboard.transform.childCount > 0)
        {
            var temp = jobboard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color;
            temp.a = 1f;
            jobboard.transform.GetChild(0).GetComponent<TextMeshProUGUI>().color = temp;
            this_job_inst.jobData = jobsDisplayed[0];
        }

    }

    public void SelectJobs()
    {

        if (Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            if (activeJob > 0)
            {
                var tempa = jobboard.transform.GetChild(activeJob).GetComponent<TextMeshProUGUI>().color;
                tempa.a = .3f;
                jobboard.transform.GetChild(activeJob).GetComponent<TextMeshProUGUI>().color = tempa;
                activeJob -= 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (activeJob < 2)
            {
                var tempa = jobboard.transform.GetChild(activeJob).GetComponent<TextMeshProUGUI>().color;
                tempa.a = .3f;
                jobboard.transform.GetChild(activeJob).GetComponent<TextMeshProUGUI>().color = tempa;
                activeJob += 1;
               
            }
        }
        if (jobboard.transform.childCount > 0)
        {
            var temp = jobboard.transform.GetChild(activeJob).GetComponent<TextMeshProUGUI>().color;
            temp.a = 1f;
            jobboard.transform.GetChild(activeJob).GetComponent<TextMeshProUGUI>().color = temp;
            this_job_inst.jobData = jobsDisplayed[activeJob];
        }
    }
    public void SubmitJob()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            //_scoreObject.GetComponent<Scoring>().jobInst = this_job_inst;
            _scoreObject.GetComponent<Scoring>().jobInst.jobData = this_job_inst.jobData;
            SceneManager.LoadScene("MonsterCustomize");
        }
    }
}
