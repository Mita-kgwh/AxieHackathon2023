using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogGameSetting : BaseDialog {

    
    public Toggle Sound;
    public Toggle Music;
    public AudioSource music;
    
    public void Start()
    {
        music = this.GetComponentInParent<AudioSource>();
    }
    public void onChangeSound()
    {
        if (Sound.isOn == true)
        {

        }
    }
    public void onChangeMusic()
    {
        if (Music.isOn == true)
        {
            music.mute = true;
        }
        else { music.mute = false; }
    }
    public void onClickOk()
    {
        this.OnHide();
    }
}
