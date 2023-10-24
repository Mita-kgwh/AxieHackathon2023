using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chat : MonoBehaviour {

    public string[] text;
    public TextMesh textMesh;
    public float fixTime;
    public float curTime;

    void Start()
    {   
        textMesh = this.GetComponentInChildren<TextMesh>();
        if (textMesh == null)
            Debug.Log("Text Mesh null!");
        textMesh.text = text[Random.Range(0, text.Length - 1)];
        textMesh.color = Color.red;
        Debug.Log(textMesh.text);
        curTime = 0;
    }

    void Update()
    {
        curTime += Time.deltaTime;
        if (curTime >= fixTime)
        {
            Destroy(this.gameObject);
        }
    }
}
