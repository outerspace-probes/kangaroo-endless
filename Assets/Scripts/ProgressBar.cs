using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        rect.localScale = new Vector3(0, 1, 1);
    }
    
    public void SetProgressVal(float val = .5f)
    {
        rect.localScale = new Vector3(val, 1, 1);
    }
}