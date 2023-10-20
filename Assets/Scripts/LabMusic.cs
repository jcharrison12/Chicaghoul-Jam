using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LabMusic : MonoBehaviour
{

    private AudioSource source;
    public int startSample = 0;
    public int endSample;
    //5286355
    //5288236**
    //5292940
    //5293881
    //5294822
    //5296703
    //5297644**
    //private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        endSample = 5292940;
        //5290118
        //5292940
        //5293881**
        //startSample = source.timeSamples = 0;
        //endSample = source.timeSamples = 5757952;

    }

    // Update is called once per frame
    void Update()
    {
        //335872
        //5757952
        Debug.Log(source.timeSamples);
        //isPlaying = true;
        if (source.timeSamples >= endSample)
        {
            startSample = 311404;
            //311404
            //312345**
            //313286
            //314227
            source.timeSamples = startSample + (source.timeSamples - endSample);
        }
    }

    void StartLoop()
    {

    }
}
