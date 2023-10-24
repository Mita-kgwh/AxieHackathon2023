using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrator : BaseEffect
{

    private GameObject cameraMain;

    private Vector3 originalPost;
    private Vector3 changePost = Vector3.zero;

    public float rangeVertical;
    public float rangeHorizontal;

	protected override void Start ()
    {
        cameraMain = GameObject.FindGameObjectWithTag("MainCamera");
        originalPost = cameraMain.transform.position;
        changePost.z = -10;

        base.Start();
	}
	
	// Update is called once per frame
	protected override void Update ()
    {
        changePost.x = Random.Range(originalPost.x - rangeHorizontal, originalPost.x + rangeHorizontal);
        changePost.y = Random.Range(originalPost.y - rangeVertical, originalPost.y + rangeVertical);

        cameraMain.transform.position = changePost;

        // trừ thời gian ở đây
        base.Update();
	}

    protected override void End()
    {
        cameraMain.transform.position = originalPost;

        base.End();
    }
}
