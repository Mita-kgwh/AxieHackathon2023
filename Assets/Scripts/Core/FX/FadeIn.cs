using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn: BaseEffect
{
    private Color fadeIndex;
    private SpriteRenderer rdr;
    protected override void Start()
    {
        base.Start();
        rdr = target.GetComponent<SpriteRenderer>();
        fadeIndex = Color.white;
        fadeIndex.a = 0;     
    }
    protected override void Update()
    {
        rdr.color = fadeIndex;
        //fadeIndex.a +=0.5f;
        fadeIndex.a = (1f - timer / timeLife);
        base.Update();
    }

    protected override void End()
    {
        rdr.color = Color.white;
        base.End();
    }
}
