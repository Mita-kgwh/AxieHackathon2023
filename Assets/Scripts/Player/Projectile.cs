using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public float dame;

    [HideInInspector]
    public Vector3 targetPosition;

    public float speed;

    private Vector3 newVector;

    // Use this for initialization
    void Start () {
         newVector = targetPosition - gameObject.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.position += Time.deltaTime * speed * newVector;
    }

    public virtual void InitTarget()
    {
        newVector = targetPosition - gameObject.transform.position;
    }

    public virtual void UpdatePosition()
    {
        gameObject.transform.position += Time.deltaTime * speed * newVector;
    }
}
