using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBear : MonoBehaviour {

    public float timeToAttack;

    public float frameTime;

    public GameObject attackPrefab;

    public float currentTime;

    private float currentFrameTime;

    private Animator anim;

    private bool isChange;
	// Use this for initialization
	void Start () {
        anim = attackPrefab.GetComponent<Animator>();
        currentTime = 0;
        isChange = false;
	}

    // Update is called once per frame
    void Update()
    {
        if(currentTime<=0)
        {
            currentTime = timeToAttack;
            if (CameraController.Instance.CheckInCamera(gameObject.transform.position))
                attackPrefab.SetActive(true);
        }
        else
        {
            currentTime -= Time.deltaTime;
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !anim.IsInTransition(0))
            {
                attackPrefab.SetActive(false);
            }
        }

       

    }


}
