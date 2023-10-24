using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyringeProjectile: Projectile
{
   
    private bool movable;

    // Use this for initialization
    void Start()
    {
        movable = true;      
        base.InitTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (movable)
            base.UpdatePosition();    
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            movable = false;     
            //Vector3 scale = new Vector3(4f, 4f, 4f);
            //gameObject.transform.localScale = scale;
            target.GetComponent<EnemyTrigger>().OnHit(dame);
        }
    }
}
