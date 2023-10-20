using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraveyardMusic : MonoBehaviour
{
    private AudioSource source;
    public int startSample = 0;
    public int endSample;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        endSample = 2610719;
        //2609779
        //2610719
        //2611660**

    }

    // Update is called once per frame
    void Update()
    {
        //235520
        //2844672
        Debug.Log(source.timeSamples);
        if (source.timeSamples >= endSample)
        {
            startSample = 217324;
            source.timeSamples = startSample + (source.timeSamples - endSample);
        }
    }
}
