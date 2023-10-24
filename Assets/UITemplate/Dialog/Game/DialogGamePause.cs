using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogGamePause : BaseDialog {

    public Toggle Sound;
    public Toggle Music;
    public AudioSource music;

    public void onClickResume()
    {
        this.OnHide();
    }
    public void onClickExit()
    {
        DialogManager.Instance.ShowMessageBox("Bạn có muốn thoát không?", MESSAGETYPE.YES_NO, () => this.onExit());
    }
    void onExit()
    {

    }

    public void onChangeSound()
    {
        if (Sound.isOn = true)
        {

        }
    }
    public void onChangeMusic()
    {
        if (Music.isOn = true)
        {
            music.mute = true;
        }
        else { music.Play(); }
    }
}
