using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class UIBase : MonoBehaviour
{
    public virtual void OpenUI()
    {
        this.gameObject.SetActive(true);
        OnOpen();
    }
    
    public virtual void CloseUI()
    {
        OnClose();
        this.gameObject.SetActive(false);
    }

    protected virtual void OnOpen()
    {
        
    }

    protected virtual void OnClose()
    {
        
    }
}
