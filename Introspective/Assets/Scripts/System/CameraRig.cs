using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRig : MonoBehaviour
{
    public Transform viewList;
    public GameObject lastView;

    public void SwitchView(string viewName)
    {
        viewList.Find(viewName).gameObject.active = true;

        if (lastView != null && lastView != viewList.Find(viewName).gameObject)
        {
            lastView.active = false;
        }

        lastView = viewList.Find(viewName).gameObject;
    }
        
}
