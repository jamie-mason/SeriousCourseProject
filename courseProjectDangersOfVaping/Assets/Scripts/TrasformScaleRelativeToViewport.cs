using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class TrasformScaleRelativeToViewport
{
    public float targetViewportWidth = 0.2f; 
    public float targetViewportHeight = 0.2f; 

    private float clamp(float value, float min=0, float max=1)
    {
        if (value < min)
        {
            return min;
        }
        else if(value > max)
        {
            return max;
        }
        else
        {
            return value;
        }
    }


    public TrasformScaleRelativeToViewport(float width, float height) {
        targetViewportWidth = clamp(width);
        targetViewportHeight = clamp(height);
    }
    public TrasformScaleRelativeToViewport()
    {
        targetViewportWidth = 0f;
        targetViewportHeight = 0f;
    }
    public Vector3 getTargetViewportSize(Camera cam, SpriteRenderer sprite)
    {
        Bounds bounds = sprite.bounds;

        Vector3 currentSize = bounds.size;

        // Calculate the target world-space size based on viewport proportions
        float viewportWorldWidth = cam.orthographicSize * 2f * cam.aspect;
        float targetWorldWidth = viewportWorldWidth * targetViewportWidth;
        float targetWorldHeight = targetWorldWidth * (targetViewportHeight / targetViewportWidth);


        // Calculate the scale factor based on the target world size
        Vector3 scale = sprite.gameObject.transform.localScale;
        scale.x *= targetWorldWidth / currentSize.x;
        scale.y *= targetWorldHeight / currentSize.y;
        return scale;
    }
    public Vector3 getTargetViewportSize(Camera cam, SpriteRenderer sprite, float targetViewportWidth, float targetViewportHeight)
    {
        Bounds bounds = sprite.bounds;

        Vector3 currentSize = bounds.size;
        this.targetViewportHeight = clamp(targetViewportHeight);
        this.targetViewportWidth = clamp(targetViewportWidth);
        // Calculate the target world-space size based on viewport proportions
        float viewportWorldWidth = cam.orthographicSize * 2f * cam.aspect;
        float targetWorldWidth = viewportWorldWidth * this.targetViewportWidth;
        float targetWorldHeight = targetWorldWidth * (this.targetViewportHeight / this.targetViewportWidth);

        // Calculate the scale factor based on the target world size
        Vector3 scale = sprite.gameObject.transform.localScale;
        scale.x *= targetWorldWidth / currentSize.x;
        scale.y *= targetWorldHeight / currentSize.y;
        return scale;
    }

    public Vector3 getTargetViewportSize(Camera cam, SpriteRenderer sprite, Vector2 targetSize)
    {
        Bounds bounds = sprite.bounds;

        Vector3 currentSize = bounds.size;
        this.targetViewportHeight = clamp(targetSize.x);
        this.targetViewportWidth = clamp(targetSize.y);
        // Calculate the target world-space size based on viewport proportions
        float viewportWorldWidth = cam.orthographicSize * 2f * cam.aspect;
        float targetWorldWidth = viewportWorldWidth * this.targetViewportWidth;
        float targetWorldHeight = targetWorldWidth * (this.targetViewportHeight / this.targetViewportWidth);

        // Calculate the scale factor based on the target world size
        Vector3 scale = sprite.gameObject.transform.localScale;
        scale.x *= targetWorldWidth / currentSize.x;
        scale.y *= targetWorldHeight / currentSize.y;
        return scale;
    }
    public Vector3 getTargetViewportSize(Camera cam, SpriteRenderer sprite, Vector3 targetSize)
    {
        Bounds bounds = sprite.bounds;

        Vector3 currentSize = bounds.size;
        this.targetViewportHeight = clamp(targetSize.x);
        this.targetViewportWidth = clamp(targetSize.y);
        // Calculate the target world-space size based on viewport proportions
        float viewportWorldWidth = cam.orthographicSize * 2f * cam.aspect;
        float targetWorldWidth = viewportWorldWidth * this.targetViewportWidth;
        float targetWorldHeight = targetWorldWidth * (this.targetViewportHeight / this.targetViewportWidth);

        // Calculate the scale factor based on the target world size
        Vector3 scale = sprite.gameObject.transform.localScale;
        scale.x *= targetWorldWidth / currentSize.x;
        scale.y *= targetWorldHeight / currentSize.y;
        return scale;
    }

    public Vector3 KeepInViewport(Camera cam, SpriteRenderer sprite)
    {
        
            // Get the object's bounds in world space
            Bounds bounds = sprite.bounds;

            // Get the corners of the object's bounds
            Vector3[] worldCorners = new Vector3[8];
            worldCorners[0] = bounds.min;
            worldCorners[1] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
            worldCorners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
            worldCorners[3] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
            worldCorners[4] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
            worldCorners[5] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
            worldCorners[6] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
            worldCorners[7] = bounds.max;

            // Convert the world corners to viewport space
            Vector3[] viewportCorners = new Vector3[8];
            for (int i = 0; i < 8; i++)
            {
                viewportCorners[i] = cam.WorldToViewportPoint(worldCorners[i]);
            }

            // Find the min and max viewport-space coordinates
            Vector3 min = viewportCorners[0];
            Vector3 max = viewportCorners[0];
            foreach (Vector3 corner in viewportCorners)
            {
                min = Vector3.Min(min, corner);
                max = Vector3.Max(max, corner);
            }

            // Check if the object is outside the screen bounds
            Vector3 offset = Vector3.zero;

            // If the object is outside the left or right bounds (viewport space 0 to 1)
            if (min.x < 0)
                offset.x = -min.x;
            else if (max.x > 1)
                offset.x = 1 - max.x;

            // If the object is outside the bottom or top bounds
            if (min.y < 0)
                offset.y = -min.y;
            else if (max.y > 1)
                offset.y = 1 - max.y;

            return offset;        
    }
    public Vector3 getViewportSize(Camera cam, SpriteRenderer sprite)
    {
        Bounds bounds = sprite.bounds;

        Vector3 currentSize = bounds.size;

        Vector3[] worldCorners = new Vector3[8];
        worldCorners[0] = bounds.min;
        worldCorners[1] = new Vector3(bounds.max.x, bounds.min.y, bounds.min.z);
        worldCorners[2] = new Vector3(bounds.min.x, bounds.max.y, bounds.min.z);
        worldCorners[3] = new Vector3(bounds.max.x, bounds.max.y, bounds.min.z);
        worldCorners[4] = new Vector3(bounds.min.x, bounds.min.y, bounds.max.z);
        worldCorners[5] = new Vector3(bounds.max.x, bounds.min.y, bounds.max.z);
        worldCorners[6] = new Vector3(bounds.min.x, bounds.max.y, bounds.max.z);
        worldCorners[7] = bounds.min;

        Vector3[] viewportCorners = new Vector3[8];
        for (int i = 0; i < 8; i++)
        {
            viewportCorners[i] = cam.WorldToViewportPoint(worldCorners[i]);
        }

        Vector3 min = viewportCorners[0];
        Vector3 max = viewportCorners[0];
        foreach (Vector3 corner in viewportCorners)
        {
            min = Vector3.Min(min, corner);
            max = Vector3.Max(max, corner);
        }

        Vector3 viewportSize = max - min;

        return viewportSize;
    }
}
