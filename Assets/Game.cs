using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoSingleton<Game> {

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    public int score;
}
