using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DestroyPopup : Popup
{
    public void OnCancelButton()
    {
        base.Close();
    }
    public void OnConfirmButton()
    {
        base.Close();
    }
}
