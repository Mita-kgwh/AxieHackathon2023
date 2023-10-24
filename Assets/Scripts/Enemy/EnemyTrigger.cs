using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour {

    public float health;

    public GameObject hp;

    private float curHealth;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetHp(float ratio)
    {
        this.health = this.health + this.health * ratio;
    }
    public void OnHit(float dame)
    {
        curHealth -= dame;
        if (curHealth <= 0)
        {
            //Quan test:
            EffectManager.Instance.Spawn(TYPE_FX.Collision, this.transform.position);

            ItemManager.Instance.Spawn((ItemType)(Random.Range(0, 4)), transform.position);
            GameController.Instance.AddLevel();
            Destroy(this.gameObject);
        }
        float ratio = curHealth / health;

        Vector3 scale = new Vector3(ratio, 1, 1);

        hp.transform.localScale = scale;
    }
}
