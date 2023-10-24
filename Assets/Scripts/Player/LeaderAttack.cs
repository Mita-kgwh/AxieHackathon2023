using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderAttack : MonoBehaviour {

    private Animator anim;

    public float timeDelay;

    private float curTime;

    public BaseBody body;

    private bool isAttack;
	// Use this for initialization
	void Start () {
        curTime = timeDelay;
        anim = GetComponent<Animator>();
        anim.SetBool("isStop", true);
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(anim.enabled);

        if(anim.enabled)
        {
            if (curTime < 0)
                anim.SetBool("isStop", true);
            else
                curTime -= Time.deltaTime;
        }

        Quaternion rotate = Quaternion.Euler(0, 0, 0);
        switch (body.dir)
        {
            case Direction.UP:
                rotate = Quaternion.Euler(0, 0, 90);
                break;
            case Direction.DOWN:
                rotate = Quaternion.Euler(0, 0, -90);
                break;
            case Direction.LEFT:
                rotate = Quaternion.Euler(0, 0, 180);
                break;
            case Direction.RIGHT:
                rotate = Quaternion.Euler(0, 0, 0);
                break;
        }

        gameObject.transform.rotation = rotate;
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Enemy")
        {
            curTime = timeDelay;
            target.GetComponent<EnemyTrigger>().OnHit(6);
            anim.SetBool("isStop", false);
        }
    }
}
