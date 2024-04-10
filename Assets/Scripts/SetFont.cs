using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class SetFont : MonoBehaviour
{
    public TMP_FontAsset fontAsset;
    [Button]
    public void Set()
    {
        TextMeshProUGUI[] obj = FindObjectsOfType<TextMeshProUGUI>();
        foreach (TextMeshProUGUI objs in obj){
            objs.font = fontAsset;
            objs.fontWeight = FontWeight.Regular;
        }
    }
}
