using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBearAttack : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            target.GetComponent<BaseBody>().OnHit(10);
        }
    }
}
