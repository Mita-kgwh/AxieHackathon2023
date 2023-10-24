using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Spawn,
    Diamond,
    Heart,
    Shield
}

public class BaseItem : MonoBehaviour
{

    protected ItemType type;

    public virtual void Init(ItemType type)
    {
        this.type = type;
    }

    public float timeLife;

    private float timer;

    private bool isBlink = false;

    protected BaseBody target;

    protected virtual void Start()
    {
        timer = timeLife;
    }


    // làm chớp chớp khi gần hết thời gian
    // destroy khi hết thời gian
    protected virtual void Update()
    {
        timer -= Time.deltaTime;

        if (!isBlink)
        {
            if (timer < (timeLife / 4f))
            {
                isBlink = true;
                EffectManager.Instance.ApplyEffect(TYPE_FX.Blink, this.gameObject, timeLife / 4f);
            }
        }
        else if (timer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    // chạm vào player
    void OnTriggerEnter2D(Collider2D col)
    {
        target = col.GetComponent<BaseBody>();
        if (target == null)
        {
            // không phải player
            return;
        }

        
        OnDie();
        Destroy(this.gameObject);
    }
    

    protected virtual void OnDie()
    {

    }

}