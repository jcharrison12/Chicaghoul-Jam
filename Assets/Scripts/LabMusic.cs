using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMusic : MonoBehaviour
{

    private AudioSource source;
    public int startSample;
    public int endSample;
    //private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        //startSample = source.timeSamples = 0;
        endSample = source.timeSamples = 5757952;
        
    }

    // Update is called once per frame
    void Update()
    {
        //335872
        //5757952
        //Debug.Log(source.timeSamples);
        //isPlaying = true;
        if (source.timeSamples >= endSample)
        {
            startSample = source.timeSamples = 335872;
            source.timeSamples = startSample + (source.timeSamples - endSample);
        }
    }

    void StartLoop()
    {

    }
}
