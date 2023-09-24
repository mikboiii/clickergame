using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Sprites;

public class building : MonoBehaviour
{
    Canvas AssociatedCanvas;
    private void Start()
    {
        AssociatedCanvas = GameObject.Find("WoodCanvas").GetComponent<Canvas>();
    }
    private void OnMouseDown()
    {
        if (AssociatedCanvas.isActiveAndEnabled)
        {
            AssociatedCanvas.enabled = false;
        }
        else
        {
            AssociatedCanvas.enabled = true;
        }
    }
}
