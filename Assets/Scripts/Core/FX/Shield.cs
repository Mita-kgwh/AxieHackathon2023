using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : BaseEffect
{
    public override void Init(GameObject _Target)
    {
        this.transform.parent = _Target.transform;
        this.transform.localPosition = Vector3.zero;
    }
    protected override void Update()
    {
        base.Update();
    }

    protected override void End()
    {
       
        base.End();
    }
}
