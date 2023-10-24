using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//quan ly cac ui trong game
public class DialogManager : MonoSingleton<DialogManager>
{

    void Start()
    {
        GetSceneCurrent();
    }
    void Update()
    {
        GetSceneCurrent();
    }
    private void GetSceneCurrent()
    {
        if (this.scene == null)
        {
            this.scene = FindObjectOfType<BaseScene>();
        }
    }

    public BaseScene scene;//scene hien tai
    //Xử lý các sự kiện show popup, màn hình
    #region Dialog
    public List<BaseDialog> dialogs = new List<BaseDialog>();

    public T ShowDialog<T>(string pathPrefabs, object data = null, Transform transf = null) where T : BaseDialog
    {
        //ShowOverlay(UIOverlay.TYPE.DIALOG);
        T target = (T)FindObjectOfType(typeof(T));
        if (target == null)
        {
            target =
                ((GameObject)Instantiate(Resources.Load(pathPrefabs)))
                    .GetComponent<T>();
            if (target)
            {
                //ShowDialog(target, data);
                target.gameObject.SetActive(true);
                if (transf == null)
                {
                    target.OnShow(this.scene.panelPopup, data);
                }
                else
                {
                    target.OnShow(transf, data);
                }
                if (!this.dialogs.Contains(target))
                    this.dialogs.Add(target); ;
            }
            else
            {
                Debug.LogErrorFormat("type:{0} is incompatible with prefab:{1}", typeof(T).ToString(), pathPrefabs);
            }
        }
        else
        {
            //BaseDialog top = dialogs[dialogs.Count - 1];
            if (transf == null)
            {
                target.OnShow(this.scene.panelPopup, data);
            }
            else
            {
                target.OnShow(transf, data);
            }
        }

        return target;
    }

    private UIOverlay overlay;
    public void ShowOverlay(UIOverlay.TYPE type)
    {
        if (overlay != null)
        {
            overlay.transform.SetParent(this.scene.panelPopup);
            overlay.transform.localScale = Vector3.one;
            overlay.transform.localPosition = Vector3.zero;
            RectTransform rect = (RectTransform)overlay.transform;
            rect.sizeDelta = Vector3.zero;
            overlay.OnShow(type);
        }
        else
        {
            this.overlay = ((GameObject)Instantiate(Resources.Load("Dialog/UIOverlay"))).GetComponent<UIOverlay>();
            this.overlay.transform.SetParent(this.scene.panelPopup);
            this.overlay.transform.localScale = Vector3.one;
            this.overlay.transform.localPosition = Vector3.zero;
            RectTransform rect = (RectTransform)this.overlay.transform;
            rect.sizeDelta = Vector3.zero;
            this.overlay.OnShow(type);
        }
    }
    public void CloseDialog(BaseDialog dialog)
    {
        UIOverlay overlay = FindObjectOfType<UIOverlay>();
        if (overlay != null)
        {
            this.overlay.OnHide();
        }
        dialog.OnHide();
        if (this.dialogs.Contains(dialog))
            this.dialogs.Remove(dialog);
    }
    public void CloseDialog()
    {
        BaseDialog dialog = this.getDialogCurrent();
        UIOverlay overlay = FindObjectOfType<UIOverlay>();
        if (overlay != null)
        {
            this.overlay.OnHide();
        }
        if (dialog != null)
        {
            dialog.OnHide();
            if (this.dialogs.Contains(dialog))
                this.dialogs.Remove(dialog);
        }
    }
    public BaseDialog getDialogCurrent()
    {
        if (this.dialogs.Count > 0)
            return this.dialogs[this.dialogs.Count - 1];
        return null;
    }
    #endregion

    #region MessageBox
    public void ShowMessageBox(string title, string content, MESSAGETYPE type = MESSAGETYPE.OK, MessageBox.CallbackOk callback = null)
    {
        ShowOverlay(UIOverlay.TYPE.MESSAGEBOX);
        GameObject obj = Instantiate(Resources.Load("GUI/MessageBox")) as GameObject;
        obj.SetActive(true);
        obj.transform.SetParent(this.scene.panelPopup);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        MessageBox message = obj.GetComponent<MessageBox>();
        if (message != null)
        {
            message.ShowMessageBox(title, content, type, callback);
        }

    }
    public void ShowMessageBox(string content, MESSAGETYPE type = MESSAGETYPE.OK, MessageBox.CallbackOk callback = null)
    {
        ShowOverlay(UIOverlay.TYPE.MESSAGEBOX);
        GameObject obj = Instantiate(Resources.Load("")) as GameObject;
        obj.SetActive(true);
        obj.transform.SetParent(this.scene.panelPopup);
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        MessageBox message = obj.GetComponent<MessageBox>();
        if (message != null)
        {
            message.transform.SetParent(this.transform);
            message.transform.localScale = Vector3.one;
            message.transform.localPosition = Vector3.zero;
            message.ShowMessageBox(content, type, callback);
        }

    }

    public void CloseMessageBox(MessageBox message)
    {
        UIOverlay overlay = FindObjectOfType<UIOverlay>();
        if (overlay != null)
        {
            overlay.OnHide();
        }
        message.OnHide();
    }
    #endregion

    public void ShowLoading(bool isShow)
    {

    }
    [ContextMenu("show message box")]
    void testShowMessageBox()
    {
        ShowMessageBox("Thong bao", "day la thong bao", MESSAGETYPE.OK);
    }

    [ContextMenu("show dialog")]
    void testShowDialog()
    {
        ShowDialog<BaseDialog>("GUI/PopupDemo");
    }
}