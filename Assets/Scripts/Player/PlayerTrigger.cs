using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour {

    public float health;

    public GameObject hp;

    private float curHealth;

    public void OnHit(float dame)
    {
        curHealth -= dame;
        if (curHealth <= 0)
        {
            //Quan test:
            EffectManager.Instance.Spawn(TYPE_FX.Collision, this.transform.position);

            ItemManager.Instance.Spawn((ItemType)(Random.Range(0, 2)), transform.position);

            Destroy(this.gameObject);
        }
        float ratio = curHealth / health;

        Vector3 scale = new Vector3(ratio, 1, 1);

        hp.transform.localScale = scale;
    }

}
