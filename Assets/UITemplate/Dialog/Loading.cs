using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Loading : MonoBehaviour {

    public Text txtLoading;
    private string loading = "Loading";
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.UpdateLoading();
	}
    private string dot = "";
    private float time = 0;
    public void UpdateLoading()
    {
        time += Time.deltaTime;
        if (time >= 0.5f)
        {
            time = 0;
            dot += ".";
            if (dot.Length > 3)
            {
                dot = "";
            }
            this.txtLoading.text = this.loading + dot;
        }
    }
}
