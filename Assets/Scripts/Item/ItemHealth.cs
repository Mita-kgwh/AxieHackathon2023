using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : BaseItem
{
    public int Amount;
    LinePlayer linePlayer;

    void Awake()
    {
        linePlayer = FindObjectOfType<LinePlayer>();
    }
    

    protected override void OnDie()
    {
        EffectManager.Instance.Spawn(TYPE_FX.HitGreen, this.transform.position);
        linePlayer.AddHP(Amount);
    }


}
