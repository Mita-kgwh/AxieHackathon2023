using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : MonoBehaviour {

    public LinePlayer linePlayer;

    private BaseBody player;

    protected virtual void Start()
    {
        player = linePlayer.leader;

    }

}
