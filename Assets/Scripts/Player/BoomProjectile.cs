using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomProjectile : Projectile {

    private Animator anim;

    private bool movable;

	// Use this for initialization
	void Start () {
        movable = true;
        anim = GetComponent<Animator>();
        base.InitTarget();
	}
	
	// Update is called once per frame
	void Update () {
        if(movable)
            base.UpdatePosition();
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Exploisive"))
        {
            anim.enabled = false;
            Destroy(gameObject);
        }
    }
      
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Enemy")
        {
            movable = false;
            anim.SetBool("isExploisive", true);
            //Vector3 scale = new Vector3(4f, 4f, 4f);
            //gameObject.transform.localScale = scale;
            target.GetComponent<EnemyTrigger>().OnHit(dame);
        }
    }
}
