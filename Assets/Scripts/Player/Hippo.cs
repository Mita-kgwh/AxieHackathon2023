using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hippo : PlayerBaseObject
{
    float timeToChange;

	// Use this for initialization
	void Start () {
        timeToChange = Random.Range(4f, 8f);
        Init();
	}
	
	// Update is called once per frame
	void Update () {
		if(timeToChange <=0)
        {
            timeToChange = Random.Range(4f, 8f);
        }
        else
        {
            timeToChange -= Time.deltaTime;
        }
	}

    public override void Init()
    {
        base.Init();
    }
}
