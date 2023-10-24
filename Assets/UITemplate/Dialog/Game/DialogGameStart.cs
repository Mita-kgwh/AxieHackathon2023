using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogGameStart : BaseDialog {
    public AudioSource music;

    public void Start()
    {
        music = this.GetComponentInParent<AudioSource>();
        
    }
    public void onClickPlayGame()
    {
        music = this.GetComponentInParent<AudioSource>();
        music.mute = true;
        //this.OnHide();
        //GameController.Instance.StartGame();

        Application.LoadLevel("Main");
    }
    public void onClickSeting()
    {
        DialogManager.Instance.ShowDialog <DialogGameSetting>("Prefabs/UI/GameSetting");
    }
}
