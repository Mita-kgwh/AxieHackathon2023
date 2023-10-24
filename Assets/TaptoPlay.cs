using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class TaptoPlay : MonoBehaviour, IPointerDownHandler
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        GameController.Instance.StartGame();
        this.gameObject.SetActive(false);
    }
}
