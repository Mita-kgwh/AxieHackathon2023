using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : BaseItem
{

    public int Amount;
    
	


    protected override void OnDie()
    {
        EffectManager.Instance.Spawn(TYPE_FX.HitBlue, this.transform.position);
        GameController.Instance.AddDiamond(Amount);
    }

    
}
