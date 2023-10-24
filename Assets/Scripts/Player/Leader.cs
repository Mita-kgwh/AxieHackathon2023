using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : MonoBehaviour {

    public GameObject attack;

    private BaseBody body;

	// Use this for initialization
	void Start () {
        body = GetComponent<BaseBody>();
	}
	
	// Update is called once per frame
	void Update () {
		switch(body.dir)
        {
            case Direction.DOWN:
                attack.transform.position = gameObject.transform.position + Vector3.down * 0.75f;
                break;
            case Direction.UP:
                attack.transform.position = gameObject.transform.position + Vector3.up * 0.75f;
                break;
            case Direction.LEFT:
                attack.transform.position = gameObject.transform.position + Vector3.left * 0.75f;
                break;
            case Direction.RIGHT:
                attack.transform.position = gameObject.transform.position + Vector3.right * 0.75f;
                break;
        }
	}
}
