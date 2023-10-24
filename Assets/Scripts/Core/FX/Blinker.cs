using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinker : BaseEffect
{

    public float timeBlink;

    private float timeBlinkNext;

    private SpriteRenderer spriteTarget;

    protected override void Start()
    {
        base.Start();

        // nếu như không có object được truyền vào
        if(target == null)
        {
            Debug.LogError("Blinker target NULL Exception!");
            Destroy(this.gameObject);
            return;
        }

        spriteTarget = target.GetComponent<SpriteRenderer>();
        if(spriteTarget == null)
        {
            Debug.LogError("Blinker sprite NULL Exception!");
            Destroy(this.gameObject);
            return;
        }

        timeBlinkNext = timeLife - timeBlink;
    }

    protected override void Update()
    {
        if(target == null)
        {
            Destroy(this.gameObject);
            return;
        }

        if(timer <= timeBlinkNext)
        {
            spriteTarget.enabled = !spriteTarget.enabled;
            timeBlinkNext -= timeBlink;
        }


        base.Update();
    }

    protected override void End()
    {
        spriteTarget.enabled = true;

        base.End();
    }

}
