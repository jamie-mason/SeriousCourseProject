using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float MovementSpeed = 1f;
    TrasformScaleRelativeToViewport trasformScaleRelativeToViewport;


    private void Start()
    {
        if (MovementSpeed < 0f)
        {
            MovementSpeed = 1f;
        }
        trasformScaleRelativeToViewport = new TrasformScaleRelativeToViewport();

    }
    private void Update()
    {
        if(trasformScaleRelativeToViewport == null)
        {
            trasformScaleRelativeToViewport = new TrasformScaleRelativeToViewport();
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 direction = new Vector2(horizontalInput, verticalInput);
        

        Vector3 newPosition = transform.position + (Vector3)(direction * MovementSpeed * Time.deltaTime);
        newPosition = KeepInViewport(newPosition);

        transform.position = newPosition;

    }
    private Vector3 KeepInViewport(Vector3 position)
    {
        var size = trasformScaleRelativeToViewport.getViewportSize(Camera.main, GetComponent<SpriteRenderer>());
        // Get the camera's viewport boundaries (from 0 to 1)
        Vector3 viewportPos = Camera.main.WorldToViewportPoint(position);

        // Clamp the viewport position to the range of 0 to 1
        viewportPos.x = Mathf.Clamp(viewportPos.x, 0f + size.x/2f, 1f - size.x / 2f);
        viewportPos.y = Mathf.Clamp(viewportPos.y, 0f + size.y / 2f, 1f - size.y / 2f);

        // Convert the clamped viewport position back to world space
        return Camera.main.ViewportToWorldPoint(viewportPos);
    }
}
