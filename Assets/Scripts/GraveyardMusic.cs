using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardMusic : MonoBehaviour
{
    private AudioSource source;
    public int startSample = 0;
    public int endSample = 2844672;
    //private bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        //startSample = source.timeSamples = 0;
        //endSample = source.timeSamples = 5757952;

    }

    // Update is called once per frame
    void Update()
    {
        //235520
        //2844672
        Debug.Log(source.timeSamples);
        //isPlaying = true;
        if (source.timeSamples >= endSample)
        {
            startSample = 235520;
            source.timeSamples = startSample + (source.timeSamples - endSample);
        }
    }
}
