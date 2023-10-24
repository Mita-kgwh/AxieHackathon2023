using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIOverlay : MonoBehaviour, IPointerDownHandler
{
    public enum TYPE
    {
        DIALOG = 0,
        MESSAGEBOX = 1,
    }
    private TYPE type;
    public void OnShow(TYPE type)
    {
        this.type = type;
        this.gameObject.SetActive(true);
    }
    public void OnHide()
    {
        this.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (this.type == TYPE.DIALOG)
        {
            DialogManager.Instance.CloseDialog();
        }
        //GameManager.Instance.CloseDialog();
    }
}
