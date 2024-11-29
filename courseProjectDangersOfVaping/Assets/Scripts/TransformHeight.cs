using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformHeight : MonoBehaviour
{
    public RectTransform parentRect;
    void Start()
    {
        // Get the RectTransform of the Canvas

        // Get the RectTransform of the UI element
        RectTransform rectTransform = GetComponent<RectTransform>();

        // Set the height of the UI element to 1/4th of the canvas height
        Debug.Log(parentRect.rect.width);
        float newWidth = parentRect.rect.width / 6f;
        rectTransform.sizeDelta = new Vector2(newWidth, rectTransform.sizeDelta.y);
    }
}
