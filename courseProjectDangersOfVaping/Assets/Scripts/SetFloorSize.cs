using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFloorSize : MonoBehaviour
{
    Camera cam;
    SpriteRenderer spriteRenderer;
    TrasformScaleRelativeToViewport trasformScaleRelativeToViewport; 

    void Start()
    {
        cam = Camera.main;
        spriteRenderer = GetComponent<SpriteRenderer>();
        trasformScaleRelativeToViewport = new TrasformScaleRelativeToViewport(1,1);
    }

    // Update is called once per frame
    void Update()
    {
        if(trasformScaleRelativeToViewport == null)
        {
            trasformScaleRelativeToViewport = new TrasformScaleRelativeToViewport(1, 1);
        }
        // Apply the new scale
        transform.localScale = trasformScaleRelativeToViewport.getTargetViewportSize(cam,spriteRenderer);

    }
}
