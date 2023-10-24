using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D col)
    {
        BaseBody leader = GetComponent<BaseBody>();

        if (col.tag == "Wall")
        {
            // Nếu leader đụng vào tường thì chết
            if (leader)
            {
                EffectManager.Instance.Spawn(TYPE_FX.Collision, transform.position);
                leader.linePlayer.OnDie();
            }
        }

  
        if (col.tag == "Enemy")
        {
            // cho effect đụng tường chết vô đây
            EffectManager.Instance.Spawn(TYPE_FX.Explosion, transform.position);
            leader.OnHit(5);
            Destroy(col.gameObject);
        }
    }
}
