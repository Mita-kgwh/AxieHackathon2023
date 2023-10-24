using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaEnemy : MonoBehaviour {

    public float timeToSpawnToxic;

    public ToxicProjectile projectilePrefab;

    private float currentTime;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (currentTime <= 0)
        {
            currentTime = timeToSpawnToxic;
            projectilePrefab.direction = gameObject.GetComponent<AIMovement>().direction;
            if (CameraController.Instance.CheckInCamera(this.gameObject.transform.position))
            {
                GameObject proj = Instantiate(projectilePrefab.gameObject,
                  gameObject.transform.position,
                  Quaternion.identity);
            }
        }
        else
            currentTime -= Time.deltaTime;
	}
}
