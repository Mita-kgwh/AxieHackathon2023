using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetection : MonoBehaviour {

    public GameObject projectile;

    public float delayTime;

    public float speed;

    private float currentTime;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            if (currentTime <= 0.0f)
            {
                GameObject proj = Instantiate(projectile,
                  transform.position,
                  Quaternion.identity);

                proj.GetComponent<Projectile>().targetPosition = target.transform.position;

                currentTime = delayTime;
            }
        }
    }
}
