using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepFortest : MonoBehaviour {

    [Range(0, 10)]
    public float stepDur;
    private float nextPause = 0.0f;

	// Use this for initialization
	void Start () {
        Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if(nextPause < Time.time)
        {
            Time.timeScale = 0;
        }
	}

    public void step()
    {
        nextPause = Time.time + stepDur;
        Time.timeScale = 1;
    }
}
