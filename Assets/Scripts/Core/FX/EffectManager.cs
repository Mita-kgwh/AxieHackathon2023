using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TYPE_FX
{
    None = 0,
    Vibrating = 1,
    Explosion = 2,
    ExplosionLarge = 3,
    HitGreen = 4,
    HitBlue = 5,
    Collision = 6,
    Blink = 7,
    FadeIn = 8,
    ComboExplosionSmall = 9,
    ComboExplosionLarge = 10,
    FadeOut = 11,
    Shield = 12,
    Slow = 13
}

public class EffectManager : MonoSingleton<EffectManager>
{
    public GameObject[] prefabs;

    private GameObject temp;

    private AudioSource audio;

    public void Spawn(TYPE_FX type, Vector3 location)
    {
        temp = Instantiate(prefabs[(int)type]) as GameObject;
        temp.transform.position = location;
    }

    // dùng spawn mấy cái fx ko cần location
    public void Spawn(TYPE_FX type)
    {
        Instantiate(prefabs[(int)type]);
    }

    public void ApplyEffect(TYPE_FX type, GameObject target)
    {
        Debug.Log((int)type);
        temp = Instantiate(prefabs[(int)type]) as GameObject;
        Debug.Log(temp);
        temp.GetComponent<BaseEffect>().Init(target);
        
    }

    public void ApplyEffect(TYPE_FX type, GameObject target, float _TimeLife)
    {
        temp = Instantiate(prefabs[(int)type]) as GameObject;

        // !!! lưu ý không gọi cartoon FX
        temp.GetComponent<BaseEffect>().Init(target);
        temp.GetComponent<BaseEffect>().timeLife = _TimeLife;
    }

    public TYPE_FX typeTest;
    public Vector3 locationTest;

    public void SpawnTest()
    {
        Spawn(typeTest, locationTest);
    }

    public void Mute()
    {
        for (int i=0;i<prefabs.Length;i++)
        {
            audio = prefabs[i].GetComponent<AudioSource>();
            if (audio!=null)
            {
                audio.mute = true;
            }
        }

    }

    public void DeMute()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            audio = prefabs[i].GetComponent<AudioSource>();
            if (audio != null)
            {
                audio.mute = false;
            }
        }
    }


#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpawnTest();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ApplyEffect(TYPE_FX.Blink, GameObject.Find("Panda"));
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            ApplyEffect(TYPE_FX.FadeIn, GameObject.Find("Panda"));
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ApplyEffect(TYPE_FX.FadeOut, GameObject.Find("Panda"));
        }
    }
#endif
}