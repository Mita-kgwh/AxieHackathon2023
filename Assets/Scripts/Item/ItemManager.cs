using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemManager : MonoSingleton<ItemManager>
{
    public GameObject[] prefabs;
    private GameObject temp;

    // Spawn ra tại location đó
    public void Spawn(ItemType type, Vector3 location)
    {
        temp = Instantiate(prefabs[(int)type]) as GameObject;
        temp.transform.position = location;
    }



#if UNITY_EDITOR//--------------------------------------------
    public ItemType typeTest;
    public Vector3 locationTest;

    public void SpawnTest()
    {
        Spawn(typeTest, locationTest);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SpawnTest();
        }
    }
#endif// --------------------------------------------
}