using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowEffect : BaseEffect
{
   
    private LinePlayer player;
    protected override void Start()
    {
        base.Start();
        player = target.GetComponent<LinePlayer>();       
    }
}
