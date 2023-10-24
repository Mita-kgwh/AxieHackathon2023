using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemShield : BaseItem
{
    public int Amount;
    LinePlayer linePlayer;

    void Awake()
    {
        linePlayer = FindObjectOfType<LinePlayer>();
    }


    protected override void OnDie()
    {
        EffectManager.Instance.ApplyEffect(TYPE_FX.Shield, target.gameObject);
        linePlayer.SetInvincible(Amount);
    }


}
